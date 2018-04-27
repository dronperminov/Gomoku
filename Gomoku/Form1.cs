using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku {
    public partial class MainForm : Form {
        const int boardWidth = 15; // ширина игрового поля
        const int boardHeight = 15; // высота игрового поля
        const int cellSize = 32; // размер клетки в пикселях
        const int winCount = 5; // нужно собрать 5 в ряд

        static readonly Player player1 = new Player("X", Color.Red); // первый игрок: красный крестик
        static readonly Player player2 = new Player("O", Color.Black); // второй игрок: чёрный нолик
        Player currentPlayer = player1;

        const int notGameOver = -1;
        const int noWinners = 0;
        const int winnerFirst = 1;
        const int winnerSecond = 2;

        Board gameBoard = null;

        void InitGrid() {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToOrderColumns = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.ColumnHeadersVisible = false;
            grid.RowHeadersVisible = false;

            grid.ReadOnly = true;
            grid.Width = boardWidth * cellSize + 3;
            grid.Height = boardHeight * cellSize + 3;

            MinimumSize = new Size(grid.Width + 40, grid.Height + 85);
            Height = MinimumSize.Height;
            Width = MinimumSize.Width;

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
                }
            }
        }

        void InitGame() {
            gameBoard = new Board(boardHeight, boardWidth);
            gameBoard.Draw(grid);

            playerLabel.Text = player1.character;
            playerLabel.ForeColor = player1.color;
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (!gameBoard[e.RowIndex, e.ColumnIndex].isFree())
                return;

            gameBoard.setPlayer(e.RowIndex, e.ColumnIndex, currentPlayer);

            Game();
        }

        int isGameOver() {
            for (int j = 0; j < boardWidth; j++) {
                for (int i = 0; i <= boardHeight - winCount; i++) {
                    int k = 0;

                    while (k < winCount && gameBoard[i + k, j].value == currentPlayer.character && gameBoard[i + k, j].color == currentPlayer.color)
                        k++;

                    if (k == winCount)
                        return currentPlayer == player1 ? winnerFirst : winnerSecond;
                }
            }

            for (int i = 0; i < boardHeight; i++) {
                for (int j = 0; j <= boardWidth - winCount; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard[i, j + k].value == currentPlayer.character && gameBoard[i, j + k].color == currentPlayer.color)
                        k++;

                    if (k == winCount)
                        return currentPlayer == player1 ? winnerFirst : winnerSecond;
                }
            }

            for (int i = 0; i <= boardHeight - winCount; i++) {
                for (int j = 0; j <= boardWidth - winCount; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard[i + k, j + k].value == currentPlayer.character && gameBoard[i + k, j + k].color == currentPlayer.color)
                        k++;

                    if (k == winCount)
                        return currentPlayer == player1 ? winnerFirst : winnerSecond;
                }
            }

            for (int i = boardHeight - 1; i >= winCount - 1; i--) {
                for (int j = winCount - 1; j < boardWidth; j++) {
                    int k = 0;

                    while (k < winCount && gameBoard[i - k, j - winCount + 1 + k].value == currentPlayer.character && gameBoard[i - k, j - winCount + 1 + k].color == currentPlayer.color)
                        k++;

                    if (k == winCount)
                        return currentPlayer == player1 ? winnerFirst : winnerSecond;
                }
            }

            int count = 0;

            for (int i = 0; i < boardHeight; i++)
                for (int j = 0; j < boardWidth; j++)
                    if (gameBoard[i, j].isFree())
                        count++;

            return count == 0 ? noWinners : notGameOver;
        }

        void GameOver(int status) {
            DialogResult result = DialogResult.No;

            if (status == noWinners) {
                result = MessageBox.Show("Желаете повторить игру?", "Ничья!", MessageBoxButtons.YesNo);
            }
            else if (status == winnerFirst) {
                result = MessageBox.Show("Желаете повторить игру?", "Победил первый игрок!", MessageBoxButtons.YesNo);
            }
            else if (status == winnerSecond) {
                result = MessageBox.Show("Желаете повторить игру?", "Победил второй игрок!", MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes)
                InitGame();
        }

        void Game() {
            gameBoard.Draw(grid);

            int status = isGameOver();
                
            if (status == notGameOver) {
                if (currentPlayer == player1) {
                    currentPlayer = player2;
                }
                else {
                    currentPlayer = player1;
                }

                playerLabel.Text = currentPlayer.character;
                playerLabel.ForeColor = currentPlayer.color;
            }
            else {
                GameOver(status);
            }
        }

        public MainForm() {
            InitializeComponent();
            InitGrid();
            InitGame();
        }

    }
}
