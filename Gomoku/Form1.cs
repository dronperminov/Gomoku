using System;
using System.Drawing;
using System.Windows.Forms;
using Gomoku.Properties;

namespace Gomoku {
    public partial class MainForm : Form {
        const int boardWidth = 15; // ширина игрового поля
        const int boardHeight = 15; // высота игрового поля
        const int winCount = 5; // нужно собрать 5 в ряд

        const double maxComplexity = 1; // максимальная сложность игры
        const double minComplexity = 0.04; // стартовое значение сложности
        const double complexityStep = 0.08; // шаг увеличения сложности

        const int levels = (int) ((maxComplexity - minComplexity) / complexityStep);
        const int winsForLevelUp = 7; // победных игр для перехода на новый уровень
        const int lossForLevelDown = 7; // поражений для уменьшения уровня

        const int winner = 1; // есть победитель
        const int noWinners = 0; // нет победителя (ничья)
        const int notGameOver = -1; // не конец игры

        static readonly Player player1 = new Player("X", Color.Red); // первый игрок: красный крестик
        static readonly Player player2 = new Player("O", Color.Black); // второй игрок: чёрный нолик

        bool isUserFirst = true; // ходит ли человек первым

        Player huPlayer; // человек
        Player aiPlayer; // компьютер

        Board gameBoard = null; // игровая доска
        GomokuAI ai = null; // компьютерный алгоритм обсчёта
        Move lastMove; // последний ход, сделанный AI

        int wins = 0; // количество побед человека
        int totals = 0; // общее число игр
        int loss = 0; // количество проигрышей

        int winsPerLevel = 0; // выигрышей человека за уровень
        int lossPerLevel = 0; // проигрышей человека за уровень

        double complexity;

        void InitGrid() {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.ColumnHeadersVisible = false;
            grid.RowHeadersVisible = false;

            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.Width = boardWidth * Settings.Default.CellSize + 3;
            grid.Height = boardHeight * Settings.Default.CellSize + 3;

            MinimumSize = new Size(grid.Width + 40, grid.Height + 130);
            Height = MinimumSize.Height;
            Width = MinimumSize.Width;

            winsLabel.Location = new Point(grid.Location.X - 5, grid.Location.Y + grid.Height + 10);
            lossLabel.Location = new Point(grid.Location.X - 5 + grid.Width / 2, grid.Location.Y + grid.Height + 10);

            for (int i = 0; i < boardWidth; i++) {
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                cell.Style.SelectionBackColor = Settings.Default.BoardSelectionBackColor;
                cell.Style.SelectionForeColor = Settings.Default.BoardSelectionForeColor;
                cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                cell.Style.Font = new Font("Arial", Settings.Default.CellSize / 2);

                DataGridViewColumn col = new DataGridViewColumn(cell);
                col.Width = Settings.Default.CellSize;
                grid.Columns.Add(col);
            }

            for (int i = 0; i < boardHeight; i++) {
                grid.Rows.Add();
                grid.Rows[i].Height = Settings.Default.CellSize;

                for (int j = 0; j < boardWidth; j++) {
                    grid[j, i].Value = "";
                    grid[j, i].Style.BackColor = Settings.Default.BoardBackColor;
                }
            }
        }

        void InitGame() {
            gameBoard = new Board(boardHeight, boardWidth);
            ai = new GomokuAI(boardHeight, boardWidth, isUserFirst);

            huPlayer = isUserFirst ? player1 : player2;
            aiPlayer = isUserFirst ? player2 : player1;

            if (!isUserFirst)
                gameBoard.SetStep(boardHeight / 2, boardWidth / 2, aiPlayer);

            isUserFirst = !isUserFirst;
            complexity = minComplexity + (Settings.Default.level - 1) * complexityStep;

            playerLabel.Text = huPlayer.character;
            playerLabel.ForeColor = huPlayer.color;

            levelLabel.Text = "Уровень: " + Settings.Default.level + " / " + levels;
            complexityLabel.Text = "Сложность: " + (int)(complexity * 100) + "%";

            gameBoard.Draw(grid);
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (!gameBoard[e.RowIndex, e.ColumnIndex].isFree())
                return;

            gameBoard.SetStep(e.RowIndex, e.ColumnIndex, huPlayer);

            ai.RemoveMove(e.RowIndex, e.ColumnIndex);

            Game();
        }

        bool isWin(Board gameBoard, Player player) {
            for (int j = 0; j < boardWidth; j++) {
                for (int i = 0; i <= boardHeight - winCount; i++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i + k, j, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 0, 1, 0);
                        return true;
                    }
                }
            }

            for (int i = 0; i < boardHeight; i++) {
                for (int j = 0; j <= boardWidth - winCount; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i, j + k, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 0, 0, 1);
                        return true;
                    }
                }
            }

            for (int i = 0; i <= boardHeight - winCount; i++) {
                for (int j = 0; j <= boardWidth - winCount; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard.IsPlayerCell(i + k, j + k, player))
                        k++;

                    if (k == winCount) {
                        showWinCells(i, j, 0, 0, 1, 1);
                        return true;
                    }
                }
            }

            for (int i = boardHeight - 1; i >= winCount - 1; i--) {
                for (int j = winCount - 1; j < boardWidth; j++) {
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

        int isGameOver(Player player) {
            if (isWin(gameBoard, player))
                return winner;

            return gameBoard.GetLostCells() == 0 ? noWinners : notGameOver;
        }

        void showWinCells(int i, int j, int istep, int jstep, int iscale = 1, int jscale = 1) {
            grid.ClearSelection();

            for (int k = 0; k < winCount; k++) {
                grid[j + jstep + k * jscale, i + istep + k * iscale].Style.BackColor = Color.Orange;
                grid[j + jstep + k * jscale, i + istep + k * iscale].Style.ForeColor = Color.White;
            }
        }

        void GameOver(Player player, int status) {
            DialogResult result = DialogResult.No;

            totals++;

            if (status != noWinners) {
                if (player == huPlayer) {
                    wins++;
                    winsPerLevel++;
                }
                else {
                    loss++;
                    lossPerLevel++;
                }
            }

            if (winsPerLevel > 0 && winsPerLevel % winsForLevelUp == 0) {
                winsPerLevel = 0;
                lossPerLevel = 0;

                if (Settings.Default.level < levels) {
                    Settings.Default.level++;
                    Settings.Default.Save();

                    MessageBox.Show("Поздравляю, Вы одержали победу " + lossForLevelDown + " раз за этот уровень. ", "Уровень был повышен на 1");
                }
                else {
                    MessageBox.Show("Ура! Игра пройдена! Поздравляем!", "Игра окончена");
                }
            }
            else if (lossPerLevel > 0 && lossPerLevel % lossForLevelDown == 0) {
                winsPerLevel = 0;
                lossPerLevel = 0;

                if (Settings.Default.level > 1) {
                    Settings.Default.level--;
                    Settings.Default.Save();

                    MessageBox.Show("К сожалению, Вы потерпели поражение " + lossForLevelDown + " раз за этот уровень. ", "Уровень был понижен на 1");
                }
            }

            winsLabel.Text = "Побед: " + wins + " / " + totals;
            lossLabel.Text = "Поражений: " + loss + " / " + totals;
            levelLabel.Text = "Уровень: " + Settings.Default.level + " / " + levels;
            complexityLabel.Text = "Сложность: " + (int)(complexity * 100) + "%";

            if (status == noWinners) {
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
            gameBoard.Draw(grid);
            int status;

            if ((status = isGameOver(huPlayer)) != notGameOver) {
                GameOver(huPlayer, status);
                return;
            }

            grid[lastMove.j, lastMove.i].Style.BackColor = Settings.Default.BoardBackColor;

            lastMove = ai.MakeMove(ref gameBoard, aiPlayer, huPlayer, aiPlayer, complexity);
            gameBoard.Draw(grid);

            grid[lastMove.j, lastMove.i].Style.BackColor = Settings.Default.AImoveBackColor;
            
            playerLabel.Text = huPlayer.character;
            playerLabel.ForeColor = huPlayer.color;

            if ((status = isGameOver(aiPlayer)) != notGameOver)
                GameOver(aiPlayer, status);
        }

        public MainForm() {
            InitializeComponent();
            InitGrid();
            InitGame();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Требуется подтверждение закрытия", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                FormClosing -= MainForm_FormClosing;
                Close();
            }
        }

        private void RestartMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите начать заново?", "Требуется подтверждение закрытия", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                isUserFirst = !isUserFirst;
                InitGame();
            }
            else {
                FormClosing -= MainForm_FormClosing;
                Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = MessageBox.Show("Вы уверены, что хотите выйти?", "Требуется подтверждение закрытия", MessageBoxButtons.YesNo) != DialogResult.Yes;
        }

        private void resetProgressMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите сбросить прогресс и вернуться на 1 уровень?\nВнимание, игра начнётся заново!", "Требуется подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            winsPerLevel = 0;
            lossPerLevel = 0;
            totals = 0;
            wins = 0;
            loss = 0;

            Settings.Default.level = 1;
            Settings.Default.Save();

            isUserFirst = !isUserFirst;
            InitGame();
        }
    }
}
