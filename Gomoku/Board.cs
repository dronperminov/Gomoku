using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku {
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
        BoardCell[,] board;

        public Board(int n, int m) {
            this.n = n;
            this.m = m;

            board = new BoardCell[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    board[i, j] = new BoardCell("", Color.Black);
        }

        public BoardCell this[int i, int j] {
            get { return board[i, j]; }
            set { board[i, j] = value; }
        }

        public void setPlayer(int i, int j, Player player) {
            board[i, j].value = player.character;
            board[i, j].color = player.color;
        }

        public void Draw(DataGridView grid) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    grid[j, i].Value = board[i, j].value;
                    grid[j, i].Style.ForeColor = board[i, j].color;
                    grid[j, i].Style.SelectionForeColor = board[i, j].color;
                }
            }

            grid.Update();
        }
    }
}
