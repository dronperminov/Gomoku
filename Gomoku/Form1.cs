using System;
using System.Drawing;
using System.Windows.Forms;
using Gomoku.Properties;

namespace Gomoku {
    public partial class MainForm : Form {
        public enum GameStatus {
            notGameOver = -1, // не конец игры
            noWinners = 0, // нет победителя (ничья)
            winner = 1 // есть победитель
        }

        // режимы игры
        public enum GameMode {
            career = 0, // карьера - постепенное увеличение уровня
            certainLevel = 1, // игра на конкретном уровне
            friendWithFriend = 2 // человек против человека
        }

        // расположение победной комбинации
        public enum WinGameType {
            horizontal, // горизонтальная линия
            vertical, // вертикальная линия
            mainDiagonal, // главная диагональ
            sideDiagonal // побочная диагональ
        }

        const int winCount = 5; // нужно собрать 5 в ряд

        const double maxComplexity = 1; // максимальная сложность игры
        const double minComplexity = 0.04; // стартовое значение сложности
        const double complexityStep = 0.08; // шаг увеличения сложности

        const int levels = (int) ((maxComplexity - minComplexity) / complexityStep) + 1;
        const int winsForLevelUp = 7; // победных игр для перехода на новый уровень
        const int lossForLevelDown = 7; // поражений для уменьшения уровня

        readonly Player player1 = new Player(Resources.gomokuCellBlack); // первый игрок: красный крестик
        readonly Player player2 = new Player(Resources.gomokuCellWhite); // второй игрок: чёрный нолик

        bool isUserFirst = true; // ходит ли человек первым

        Player huPlayer; // человек
        Player aiPlayer; // компьютер
        Player player; // игрок (для игры друг против друга

        Grid grid = null;
        Board gameBoard = null; // игровая доска
        GameMode gameMode; // текущий режим игры
        GomokuAI ai = null; // компьютерный алгоритм обсчёта
        Move lastAIMove; // последний ход, сделанный AI
        Move lastHuMove; // последний ход, сделанный человеком

        int wins = 0; // количество побед человека
        int totals = 0; // общее число игр
        int loss = 0; // количество проигрышей

        int wins1 = 0; // количество побед первого игрока
        int wins2 = 0; // количество побед второго игрока

        int level; // уровень для игры в режиме certainLevel
        double complexity; // сложность для AI (вероятность пропуска некоторых шаблонов)

        int canceledMoves = 0; // количество отменённых ходов

        void InitGrid() {
            grid = new Grid(Settings.Default.BoardWidth, Settings.Default.BoardHeight, Settings.Default.CellSize, new Point(12, 80), this);
            grid.cellClick += huStep;

            Size gridSize = grid.GetSize();
            Point gridLocation = grid.GetLocation();

            MinimumSize = new Size(gridSize.Width + 300, gridSize.Height + 110);
            Height = MinimumSize.Height;
            Width = MinimumSize.Width;

            rulesLabel.Text = "1.Выбери любую клетку мышкой\n" +
                "2.Нажми дважды, чтобы сделать ход\n" +
                "3.Собери " + winCount + " в ряд раньше соперника\n" +
                "4.Повысь уровень, одержав " + winsForLevelUp + " побед\n" +
                "5.Не понизь уровень, проиграв " + lossForLevelDown + " раз\n" +
                "6.Пройди все " + levels + " уровней сложности";

            gameNameLabel.Location = new Point((Width - gameNameLabel.Width) / 2, gameNameLabel.Location.Y);

            totalLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y);
            winsLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + 25);
            lossLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + 50);

            levelLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + 75);
            complexityLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + 100);

            progressLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + 125);
            winsPerLevelLabel.Location = new Point(gridLocation.X + gridSize.Width + progressLabel.Width + 10, gridLocation.Y + 125);
            lossPerLevelLabel.Location = new Point(gridLocation.X + gridSize.Width + progressLabel.Width + winsPerLevelLabel.Width + 20, gridLocation.Y + 125);

            currMoveLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + 175);
            playerPicture.Location = new Point(gridLocation.X + gridSize.Width + 10 + currMoveLabel.Width, gridLocation.Y + 170);

            rulesHeadlineLabel.Location = new Point(gridLocation.X + gridSize.Width + 10 + (rulesLabel.Width - rulesHeadlineLabel.Width) / 2, gridLocation.Y + gridSize.Height - rulesLabel.Height - 40);
            rulesLabel.Location = new Point(gridLocation.X + gridSize.Width + 10, gridLocation.Y + gridSize.Height - rulesLabel.Height - 20);

            for (int i = 1; i <= levels; i++)
                selectLevelBox.Items.Add("Уровень " + i);

            gameMode = (GameMode) Settings.Default.lastGameMode;
        }

        void InitGame(GameMode mode) {
            gameBoard = new Board(Settings.Default.BoardHeight, Settings.Default.BoardWidth, player1, player2);
            grid.Reset();

            resetProgressMenuItem.Visible = (mode == GameMode.career);
            cancelMoveMenuItem.Visible = (mode == GameMode.career || mode == GameMode.certainLevel);
            cancelMoveMenuItem.Enabled = false;
            progressLabel.Visible = (mode == GameMode.career);
            levelLabel.Visible = (mode == GameMode.career || mode == GameMode.certainLevel);
            complexityLabel.Visible = (mode == GameMode.career || mode == GameMode.certainLevel);
            winsPerLevelLabel.Visible = (mode == GameMode.career);
            lossPerLevelLabel.Visible = (mode == GameMode.career);

            selectLevelBox.SelectedIndexChanged -= selectLevelBox_SelectedIndexChanged;
            selectLevelBox.SelectedIndex = Settings.Default.lastSelectedLevel - 1;
            selectLevelBox.SelectedIndexChanged += selectLevelBox_SelectedIndexChanged;

            careerModeMenuItem.Enabled = mode != GameMode.career;
            certainLevelModeMenuItem.Enabled = mode != GameMode.certainLevel;
            friendToFriendModeMenuItem.Enabled = mode != GameMode.friendWithFriend;

            if (mode == GameMode.career || mode == GameMode.certainLevel) {
                ai = new GomokuAI(Settings.Default.BoardHeight, Settings.Default.BoardWidth, isUserFirst);

                huPlayer = isUserFirst ? player1 : player2;
                aiPlayer = isUserFirst ? player2 : player1;

                if (!isUserFirst)
                    gameBoard.SetStep(Settings.Default.BoardHeight / 2, Settings.Default.BoardWidth / 2, aiPlayer);

                if (mode == GameMode.career) {
                    complexity = minComplexity + (Settings.Default.level - 1) * complexityStep;
                }
                else {
                    level = Settings.Default.lastSelectedLevel;
                    complexity = minComplexity + (level - 1) * complexityStep;
                }
                playerPicture.Image = huPlayer.image;
            }
            else {
                player = isUserFirst ? player1 : player2;

                playerPicture.Image = player.image;
            }

            isUserFirst = !isUserFirst;

            showInfo(mode);

            gameBoard.Draw(grid);
        }

        void huStep(object sender, EventArgs e) {
            Grid.GridCell cell = (Grid.GridCell) sender;

            if (!gameBoard[cell.i, cell.j].isFree())
                return;

            if (gameMode == GameMode.career || gameMode == GameMode.certainLevel) {
                lastHuMove = new Move(cell.i, cell.j);
                gameBoard.SetStep(cell.i, cell.j, huPlayer);
                ai.RemoveMove(cell.i, cell.j);

                grid[lastAIMove.i, lastAIMove.j].button.BackColor = Color.Transparent;

                cancelMoveMenuItem.Enabled = canceledMoves < Settings.Default.maxCancelMoves;
            }
            else {
                gameBoard.SetStep(cell.i, cell.j, player);
            }

            Game(gameMode);
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

        public Image addLine(Image src, int size, WinGameType type) {
            Bitmap image = new Bitmap(src);
            Pen pen = new Pen(Settings.Default.winLineColor, size);
            Graphics g = Graphics.FromImage(image);

            switch (type) {
                case WinGameType.horizontal:
                    g.DrawLine(pen, 0, Settings.Default.CellSize / 2, Settings.Default.CellSize, Settings.Default.CellSize / 2);
                    break;

                case WinGameType.vertical:
                    g.DrawLine(pen, Settings.Default.CellSize / 2, 0, Settings.Default.CellSize / 2, Settings.Default.CellSize);
                    break;

                case WinGameType.mainDiagonal:
                    g.DrawLine(pen, 0, 0, Settings.Default.CellSize, Settings.Default.CellSize);
                    break;

                case WinGameType.sideDiagonal:
                    g.DrawLine(pen, 0, Settings.Default.CellSize, Settings.Default.CellSize, 0);
                    break;
            }

            return image;
        }

        void showWinCells(int i, int j, int istep, int jstep, int iscale = 1, int jscale = 1) {
            WinGameType type;

            if (iscale == 0) {
                type = WinGameType.horizontal;
            }
            else if (jscale == 0) {
                type = WinGameType.vertical;
            }
            else if (iscale == 1 && jscale == 1) {
                type = WinGameType.mainDiagonal;
            }
            else {
                type = WinGameType.sideDiagonal;
            }

            for (int k = 0; k < winCount; k++) {
                grid[i + istep + k * iscale, j + jstep + k * jscale].button.Image = addLine(grid[i + istep + k * iscale, j + jstep + k * jscale].button.Image, 6, type);
            }
        }

        void showInfo(GameMode mode) {
            totalLabel.Text = "Всего игр: " + totals;

            if (mode == GameMode.career || mode == GameMode.certainLevel) {                
                winsLabel.Text = "Побед: " + wins;
                lossLabel.Text = "Поражений: " + loss;
                levelLabel.Text = "Уровень: " + (mode == GameMode.career ? Settings.Default.level : level);
                complexityLabel.Text = "Сложность: " + complexity;

                if (mode == GameMode.career) {
                    winsPerLevelLabel.Text = Settings.Default.winsPerLevel.ToString();
                    lossPerLevelLabel.Text = Settings.Default.lossPerLevel.ToString();
                }
            }
            else {
                winsLabel.Text = "Игрок1: " + wins1;
                lossLabel.Text = "Игрок2: " + wins2;
            }
        }

        void GameOver(Player player, GameStatus status, GameMode mode) {
            DialogResult result = DialogResult.No;

            totals++;

            if (mode == GameMode.career || mode == GameMode.certainLevel) {
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

                if (mode == GameMode.career) {
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
                }
            }
            else {
                if (status != GameStatus.noWinners) {
                    if (player == player1) {
                        wins1++;
                    }
                    else {
                        wins2++;
                    }
                }
            }

            showInfo(mode);

            if (status == GameStatus.noWinners) {
                result = MessageBox.Show("Желаете повторить игру?", "Ничья!", MessageBoxButtons.YesNo);
            }
            else {
                if (mode == GameMode.career || mode == GameMode.certainLevel) {
                    if (player == huPlayer) {
                        result = MessageBox.Show("Желаете повторить игру?", "Победил ЧЕЛОВЕК!", MessageBoxButtons.YesNo);
                    }
                    else if (player == aiPlayer) {
                        result = MessageBox.Show("Желаете повторить игру?", "Победил КОМПЬЮТЕР!", MessageBoxButtons.YesNo);
                    }
                }
                else {
                    if (player == player1) {
                        result = MessageBox.Show("Желаете повторить игру?", "Победил первый игрок!", MessageBoxButtons.YesNo);
                    }
                    else if (player == player2) {
                        result = MessageBox.Show("Желаете повторить игру?", "Победил второй игрок!", MessageBoxButtons.YesNo);
                    }
                }
            }

            if (result == DialogResult.Yes) {
                InitGame(mode);
            }
            else {
                FormClosing -= MainForm_FormClosing;
                Close();
            }
        }

        void Game(GameMode mode) {
            gameBoard.Draw(grid, true);

            GameStatus status;

            if (mode == GameMode.career || mode == GameMode.certainLevel) {
                if ((status = isGameOver(huPlayer)) != GameStatus.notGameOver) {
                    GameOver(huPlayer, status, mode);
                    return;
                }

                playerPicture.Image = aiPlayer.image;
                playerPicture.Update();

                lastAIMove = ai.MakeMove(ref gameBoard, aiPlayer, huPlayer, aiPlayer, complexity);
                gameBoard.Draw(grid);

                if ((status = isGameOver(aiPlayer)) != GameStatus.notGameOver) {
                    GameOver(aiPlayer, status, mode);
                    return;
                }

                grid[lastAIMove.i, lastAIMove.j].button.BackColor = Settings.Default.AImoveBackColor;
                grid[lastAIMove.i, lastAIMove.j].button.Update();
                System.Threading.Thread.Sleep(150);
                grid[lastAIMove.i, lastAIMove.j].button.BackColor = Color.Transparent;
                grid[lastAIMove.i, lastAIMove.j].button.Update();
                System.Threading.Thread.Sleep(150);
                grid[lastAIMove.i, lastAIMove.j].button.BackColor = Settings.Default.AImoveBackColor;
                grid[lastAIMove.i, lastAIMove.j].button.Update();

                playerPicture.Image = huPlayer.image;
                playerPicture.Update();
            }
            else {
                if ((status = isGameOver(player)) != GameStatus.notGameOver) {
                    GameOver(player, status, mode);
                    return;
                }

                player = player == player1 ? player2 : player1;

                playerPicture.Image = player.image;
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите выйти из игры?", "Требуется подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                FormClosing -= MainForm_FormClosing;
                Environment.Exit(0);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = MessageBox.Show("Вы уверены, что хотите выйти?", "Требуется подтверждение закрытия", MessageBoxButtons.YesNo) != DialogResult.Yes;
        }

        private void resetProgressMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите сбросить прогресс и вернуться на 1 уровень?\nВнимание, игра начнётся заново!", "Требуется подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            totals = 0;
            wins = 0;
            loss = 0;
            
            Settings.Default.level = 1;
            Settings.Default.winsPerLevel = 0;
            Settings.Default.lossPerLevel = 0;
            Settings.Default.Save();

            isUserFirst = true;
            InitGame(gameMode);
        }

        private void menuExitMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы уверены, что хотите выйти в меню?", "Требуется подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                FormClosing -= MainForm_FormClosing;
                Close();
            }
        }

        private void friendToFriendModeMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы действительно хотите сменить режим игры?\nТекущая игра будет закрыта.", "Подтвердите смену режима игры", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            totals = 0;

            gameMode = GameMode.friendWithFriend;
            Settings.Default.lastGameMode = (int)gameMode;
            Settings.Default.Save();

            isUserFirst = true;
            InitGame(gameMode);
        }

        private void careerModeMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы действительно хотите сменить режим игры?\nТекущая игра будет закрыта.", "Подтвердите смену режима игры", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            totals = 0;

            gameMode = GameMode.career;
            Settings.Default.lastGameMode = (int)gameMode;
            Settings.Default.Save();

            isUserFirst = true;
            InitGame(gameMode);
        }

        private void selectLevelBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (MessageBox.Show("Вы действительно хотите сменить режим игры?\nТекущая игра будет закрыта.", "Подтвердите смену режима игры", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            changeModeMenuItem.HideDropDown();

            Settings.Default.lastSelectedLevel = selectLevelBox.SelectedIndex + 1;

            totals = 0;
            level = Settings.Default.lastSelectedLevel;

            gameMode = GameMode.certainLevel;
            Settings.Default.lastGameMode = (int)gameMode;
            Settings.Default.Save();

            isUserFirst = true;
            InitGame(gameMode);
        }

        private void cancelMoveMenuItem_Click(object sender, EventArgs e) {
            ai.AddMove(lastAIMove.i, lastAIMove.j);
            gameBoard[lastAIMove.i, lastAIMove.j] = new BoardCell();
            gameBoard[lastHuMove.i, lastHuMove.j] = new BoardCell();

            cancelMoveMenuItem.Enabled = false;
            canceledMoves++;

            gameBoard.Draw(grid);
        }

        public MainForm() {
            InitializeComponent();
            InitGrid();
            InitGame(gameMode);
        }
    }
}
