using System;
using System.Drawing;
using System.Windows.Forms;
using Gomoku.Properties;

namespace Gomoku {
    public partial class MainForm : Form {
        enum GameStatus {
            notGameOver = -1, // не конец игры
            noWinners = 0, // нет победителя (ничья)
            winner = 1 // есть победитель
        }

        const int winCount = 5; // нужно собрать 5 в ряд

        const double maxComplexity = 1; // максимальная сложность игры
        const double minComplexity = 0.04; // стартовое значение сложности
        const double complexityStep = 0.08; // шаг увеличения сложности

        const int levels = (int) ((maxComplexity - minComplexity) / complexityStep);
        const int winsForLevelUp = 7; // победных игр для перехода на новый уровень
        const int lossForLevelDown = 7; // поражений для уменьшения уровня

        static readonly Player player1 = new Player("X", Color.White); // первый игрок: красный крестик
        static readonly Player player2 = new Player("O", Color.Black); // второй игрок: чёрный нолик

        bool isUserFirst = true; // ходит ли человек первым

        Player huPlayer; // человек
        Player aiPlayer; // компьютер

        Grid grid = null;
        Board gameBoard = null; // игровая доска
        GomokuAI ai = null; // компьютерный алгоритм обсчёта
        Move lastMove; // последний ход, сделанный AI

        int wins = 0; // количество побед человека
        int totals = 0; // общее число игр
        int loss = 0; // количество проигрышей

        double complexity;

        void InitGrid() {
            grid = new Grid(Settings.Default.BoardWidth, Settings.Default.BoardHeight, Settings.Default.CellSize, new Point(12, 80), this);
            grid.cellClick += huStep;

            Size gridSize = grid.GetSize();
            Point gridLocation = grid.GetLocation();

            MinimumSize = new Size(gridSize.Width + 300, gridSize.Height + 130);
            Height = MinimumSize.Height;
            Width = MinimumSize.Width;

            gameNameLabel.Location = new Point((Width - gameNameLabel.Width) / 2, gameNameLabel.Location.Y);

            totalLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y);
            winsLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + 25);
            lossLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + 50);

            levelLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + 75);
            complexityLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + 100);

            progressLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + 125);
            winsPerLevelLabel.Location = new Point(gridLocation.X + gridSize.Width + progressLabel.Width + 20, gridLocation.Y + 125);
            lossPerLevelLabel.Location = new Point(gridLocation.X + gridSize.Width + progressLabel.Width + winsPerLevelLabel.Width + 30, gridLocation.Y + 125);

            currMoveLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + 175);
            playerLabel.Location = new Point(gridLocation.X + gridSize.Width + 20 + currMoveLabel.Width, gridLocation.Y + 175);

            rulesHeadlineLabel.Location = new Point(gridLocation.X + gridSize.Width + 20 + (rulesLabel.Width - rulesHeadlineLabel.Width) / 2, gridLocation.Y + gridSize.Height - rulesLabel.Height - 20);
            rulesLabel.Location = new Point(gridLocation.X + gridSize.Width + 20, gridLocation.Y + gridSize.Height - rulesLabel.Height);
        }

        void InitGame() {
            gameBoard = new Board(Settings.Default.BoardHeight, Settings.Default.BoardWidth);
            ai = new GomokuAI(Settings.Default.BoardHeight, Settings.Default.BoardWidth, isUserFirst);

            huPlayer = isUserFirst ? player1 : player2;
            aiPlayer = isUserFirst ? player2 : player1;

            if (!isUserFirst)
                gameBoard.SetStep(Settings.Default.BoardHeight / 2, Settings.Default.BoardWidth / 2, aiPlayer);

            isUserFirst = !isUserFirst;
            complexity = minComplexity + (Settings.Default.level - 1) * complexityStep;

            playerLabel.Text = huPlayer.character;
            playerLabel.ForeColor = huPlayer.color;

            showInfo();

            gameBoard.Draw(grid);
        }

        void huStep(object sender, EventArgs e) {
            Grid.GridCell cell = (Grid.GridCell) sender;

            if (!gameBoard[cell.i, cell.j].isFree())
                return;

            gameBoard.SetStep(cell.i, cell.j, huPlayer);
            ai.RemoveMove(cell.i, cell.j);

            grid[lastMove.i, lastMove.j].button.BackColor = Color.Transparent;

            Game();
        }

        bool isWin(Board gameBoard, Player player) {
            for (int j = 0; j < Settings.Default.BoardWidth; j++) {
                for (int i = 0; i <= Settings.Default.BoardHeight - winCount; i++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i + k, j, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 0, 1, 0);
                        return true;
                    }
                }
            }

            for (int i = 0; i < Settings.Default.BoardHeight; i++) {
                for (int j = 0; j <= Settings.Default.BoardWidth - winCount; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i, j + k, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 0, 0, 1);
                        return true;
                    }
                }
            }

            for (int i = 0; i <= Settings.Default.BoardHeight - winCount; i++) {
                for (int j = 0; j <= Settings.Default.BoardWidth - winCount; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i + k, j + k, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 0, 1, 1);
                        return true;
                    }
                }
            }

            for (int i = Settings.Default.BoardHeight - 1; i >= winCount - 1; i--) {
                for (int j = winCount - 1; j < Settings.Default.BoardWidth; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i - k, j - winCount + 1 + k, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 1 - winCount, -1, 1);
                        return true;
                    }
                }
            }

            return false;
        }

        GameStatus isGameOver(Player player) {
            if (isWin(gameBoard, player))
                return GameStatus.winner;

            return gameBoard.GetLostCells() == 0 ? GameStatus.noWinners : GameStatus.notGameOver;
        }

        void showWinCells(int i, int j, int istep, int jstep, int iscale = 1, int jscale = 1) {
            for (int k = 0; k < winCount; k++) {
                grid[i + istep + k * iscale, j + jstep + k * jscale].button.BackColor = Color.Orange;
                grid[i + istep + k * iscale, j + jstep + k * jscale].button.ForeColor = Color.White;
            }
        }

        void showInfo() {
            totalLabel.Text = "Всего игр: " + totals;
            winsLabel.Text = "Побед: " + wins;
            lossLabel.Text = "Поражений: " + loss;
            levelLabel.Text = "Уровень: " + Settings.Default.level;
            complexityLabel.Text = "Сложность: " + complexity;

            winsPerLevelLabel.Text = Settings.Default.winsPerLevel.ToString();
            lossPerLevelLabel.Text = Settings.Default.lossPerLevel.ToString();
        }

        void GameOver(Player player, GameStatus status) {
            DialogResult result = DialogResult.No;

            totals++;

            if (status != GameStatus.noWinners) {
                if (player == huPlayer) {
                    wins++;
                    Settings.Default.winsPerLevel++;
                }
                else {
                    loss++;
                    Settings.Default.lossPerLevel++;
                }
            }

            if (Settings.Default.winsPerLevel > 0 && Settings.Default.winsPerLevel % winsForLevelUp == 0) {
                Settings.Default.winsPerLevel = 0;
                Settings.Default.lossPerLevel = 0;

                if (Settings.Default.level < levels) {
                    Settings.Default.level++;

                    MessageBox.Show("Поздравляю, Вы одержали победу " + lossForLevelDown + " раз за этот уровень. ", "Уровень был повышен на 1");
                }
                else {
                    MessageBox.Show("Ура! Игра пройдена! Поздравляем!", "Игра окончена");
                }
            }
            else if (Settings.Default.lossPerLevel > 0 && Settings.Default.lossPerLevel % lossForLevelDown == 0) {
                Settings.Default.winsPerLevel = 0;
                Settings.Default.lossPerLevel = 0;

                if (Settings.Default.level > 1) {
                    Settings.Default.level--;

                    MessageBox.Show("К сожалению, Вы потерпели поражение " + lossForLevelDown + " раз за этот уровень. ", "Уровень был понижен на 1");
                }
            }

            Settings.Default.Save();

            showInfo();

            if (status == GameStatus.noWinners) {
                result = MessageBox.Show("Желаете повторить игру?", "Ничья!", MessageBoxButtons.YesNo);
            }
            else if (player == huPlayer) {
                result = MessageBox.Show("Желаете повторить игру?", "Победил ЧЕЛОВЕК!", MessageBoxButtons.YesNo);
            }
            else if (player == aiPlayer) {
                result = MessageBox.Show("Желаете повторить игру?", "Победил КОМПЬЮТЕР!", MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes) {
                InitGame();
            }
            else {
                FormClosing -= MainForm_FormClosing;
                Close();
            }
        }

        void Game() {
            gameBoard.Draw(grid, true);

            GameStatus status;

            if ((status = isGameOver(huPlayer)) != GameStatus.notGameOver) {
                GameOver(huPlayer, status);
                return;
            }

            playerLabel.Text = aiPlayer.character;
            playerLabel.ForeColor = aiPlayer.color;
            playerLabel.Update();

            lastMove = ai.MakeMove(ref gameBoard, aiPlayer, huPlayer, aiPlayer, complexity);
            gameBoard.Draw(grid);

            grid[lastMove.i, lastMove.j].button.BackColor = Settings.Default.AImoveBackColor;
            grid[lastMove.i, lastMove.j].button.Update();
            System.Threading.Thread.Sleep(150);
            grid[lastMove.i, lastMove.j].button.BackColor = Color.Transparent;
            grid[lastMove.i, lastMove.j].button.Update();
            System.Threading.Thread.Sleep(150);
            grid[lastMove.i, lastMove.j].button.BackColor = Settings.Default.AImoveBackColor;
            grid[lastMove.i, lastMove.j].button.Update();

            playerLabel.Text = huPlayer.character;
            playerLabel.ForeColor = huPlayer.color;
            playerLabel.Update();

            if ((status = isGameOver(aiPlayer)) != GameStatus.notGameOver)
                GameOver(aiPlayer, status);
        }

        private void ExitMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите выйти из игры?", "Требуется подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                FormClosing -= MainForm_FormClosing;
                Environment.Exit(0);
            }
        }

        private void RestartMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите начать заново?", "Требуется подтверждение закрытия", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                isUserFirst = !isUserFirst;
                InitGame();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = MessageBox.Show("Вы уверены, что хотите выйти?", "Требуется подтверждение закрытия", MessageBoxButtons.YesNo) != DialogResult.Yes;
        }

        private void resetProgressMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите сбросить прогресс и вернуться на 1 уровень?\nВнимание, игра начнётся заново!", "Требуется подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            totals = 0;
            wins = 0;
            loss = 0;
            
            Settings.Default.level = 1;
            Settings.Default.winsPerLevel = 0;
            Settings.Default.lossPerLevel = 0;
            Settings.Default.Save();

            isUserFirst = true;
            InitGame();
        }

        private void menuExitMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите выйти в меню?", "Требуется подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                FormClosing -= MainForm_FormClosing;
                Close();
            }
        }

        public MainForm() {
            InitializeComponent();
            InitGrid();
            InitGame();
        }
    }
}
