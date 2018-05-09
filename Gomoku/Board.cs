using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku {
    // ход для AI
    struct Move {
        public int i; // строка
        public int j; // столбец
        public int importance; // важность

        public Move(int i, int j, int importance = 0) {
            this.i = i;
            this.j = j;
            this.importance = importance;
        }
    }

    class BoardCell {
        public readonly Image image;

        public BoardCell(Image image = null) {
            this.image = image;
        }

        public bool isFree() { return image == null;  }
    }

    class Board {
        int n, m;
        int lostCells;
        BoardCell[,] board;
        Player player1, player2;

        public Board(int n, int m, Player player1, Player player2) {
            this.n = n;
            this.m = m;

            board = new BoardCell[n, m];
            lostCells = n * m;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    board[i, j] = new BoardCell();

            this.player1 = player1;
            this.player2 = player2;
        }

        public BoardCell this[int i, int j] {
            get {
                return  board[i, j];
            }

            set {
                board[i, j] = value;
            }
        }

        public void SetStep(int i, int j, Player player) {
            board[i, j] = new BoardCell(player.image);

            lostCells--;
        }

        public void Draw(Grid grid, bool update = false) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    grid[i, j].button.Image = board[i, j].image;

                    if (update)
                        grid[i, j].button.Update();
                }
            }
        }

        public bool IsPlayerCell(int i, int j, Player player) {
            return board[i, j].image == player.image;
        }

        public int GetLostCells() {
            return lostCells;
        }
    }
}
