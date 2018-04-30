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

    struct BoardCell {
        public string value;
        public Color color;

        public BoardCell(string value, Color color) {
            this.value = value;
            this.color = color;
        }

        public bool isFree() { return value == "";  }
    }

    class Board {
        int n, m;
        int lostCells;
        BoardCell[,] board;

        public Board(int n, int m) {
            this.n = n;
            this.m = m;

            board = new BoardCell[n, m];
            lostCells = n * m;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    board[i, j] = new BoardCell("", Color.Black);
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
            board[i, j].value = player.character;
            board[i, j].color = player.color;

            lostCells--;
        }

        public void Draw(DataGridView grid) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    grid[j, i].Value = board[i, j].value;
                    grid[j, i].Style.ForeColor = board[i, j].color;
                    grid[j, i].Style.SelectionForeColor = board[i, j].color;
                    grid[j, i].Style.BackColor = Properties.Settings.Default.BoardBackColor;
                    grid[j, i].Style.Font = new Font("Arial", Properties.Settings.Default.CellSize / 2);
                    grid[j, i].ReadOnly = board[i, j].value != "";
                }
            }

            grid.Update();
        }

        public bool IsPlayerCell(int i, int j, Player player) {
            return board[i, j].value == player.character && board[i, j].color == player.color;
        }

        public int GetLostCells() {
            return lostCells;
        }
    }
}
