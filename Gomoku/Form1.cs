using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gomoku {
    public partial class MainForm : Form {
        const int boardWidth = 19; // ширина игрового поля
        const int boardHeight = 19; // высота игрового поля
        const int cellSize = 32; // размер клетки в пикселях
        const int winCount = 5; // нужно собрать 5 в ряд

        const double complexity = 0.5; // сложность игры (0, 1]

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

        int wins = 0; // количество побед человека
        int totals = 0; // общее число игр
        int loss = 0; // количество проигрышей

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
            grid.Width = boardWidth * cellSize + 3;
            grid.Height = boardHeight * cellSize + 3;

            MinimumSize = new Size(grid.Width + 40, grid.Height + 110);
            Height = MinimumSize.Height;
            Width = MinimumSize.Width;

            winsLabel.Location = new Point(grid.Location.X - 5, grid.Location.Y + grid.Height + 10);
            lossLabel.Location = new Point(grid.Location.X - 5 + grid.Width / 2, grid.Location.Y + grid.Height + 10);

            for (int i = 0; i < boardWidth; i++) {
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                cell.Style.SelectionBackColor = Color.WhiteSmoke;
                cell.Style.SelectionForeColor = Color.Black;
                cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                cell.Style.Font = new Font("Arial", cellSize / 2);

                DataGridViewColumn col = new DataGridViewColumn(cell);
                col.Width = cellSize;
                grid.Columns.Add(col);
            }

            for (int i = 0; i < boardHeight; i++) {
                grid.Rows.Add();
                grid.Rows[i].Height = cellSize;

                for (int j = 0; j < boardWidth; j++) {
                    grid[j, i].Value = "";
                    grid[j, i].Style.BackColor = Color.White;
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

            playerLabel.Text = huPlayer.character;
            playerLabel.ForeColor = huPlayer.color;

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

        void updateStatistic() {
            winsLabel.Text = "Побед: " + wins + " / " + totals;
            lossLabel.Text = "Поражений: " + loss + " / " + totals;
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

            if (status == noWinners) {
                updateStatistic();

                result = MessageBox.Show("Желаете повторить игру?", "Ничья!", MessageBoxButtons.YesNo);
            }
            else if (player == huPlayer) {
                wins++;
                updateStatistic();

                result = MessageBox.Show("Желаете повторить игру?", "Победил ЧЕЛОВЕК!", MessageBoxButtons.YesNo);
            }
            else if (player == aiPlayer) {
                loss++;

                updateStatistic();

                result = MessageBox.Show("Желаете повторить игру?", "Победил КОМПЬЮТЕР!", MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes) {
                InitGame();
            }
            else {
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

            ai.MakeMove(ref gameBoard, aiPlayer, huPlayer, aiPlayer, 1);
            gameBoard.Draw(grid);

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
    }
}
