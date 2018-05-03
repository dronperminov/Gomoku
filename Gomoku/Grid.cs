using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Gomoku {
    class Grid {
        public struct GridCell {
            public Button button;
            public int i;
            public int j;
        }

        public readonly int width; // ширина поля
        public readonly int height; // высота поля
        int cellSize; // размер клетки

        public readonly Size size;
        public readonly Point location;

        public event CellClick cellClick;
        public delegate void CellClick(object sender, EventArgs eventArgs);

        GridCell[,] values;

        public Grid(int width, int height, int cellSize, Point location, Form form) {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            this.location = location;

            size = new Size(width * cellSize, height * cellSize);
            values = new GridCell[height, width];

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    values[i, j].button = new Button();
                    values[i, j].i = i;
                    values[i, j].j = j;

                    values[i, j].button.Location = new Point(location.X + j * cellSize, location.Y + i * cellSize);
                    values[i, j].button.Size = new Size(cellSize, cellSize);

                    values[i, j].button.Font = new Font("Arial", cellSize / 2);

                    values[i, j].button.BackColor = Color.Transparent;

                    values[i, j].button.FlatStyle = FlatStyle.Flat;

                    values[i, j].button.FlatAppearance.BorderSize = 1;
                    values[i, j].button.FlatAppearance.BorderColor = Color.FromArgb(200, 255, 255, 255);
                    values[i, j].button.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.BoardSelectionBackColor;
                    values[i, j].button.FlatAppearance.CheckedBackColor = Color.Red;

                    values[i, j].button.Name = i + " " + j;
                    values[i, j].button.Click += ButtonClick;

                    form.Controls.Add(values[i, j].button);
                }
            }
        }

        void ButtonClick(object sender, EventArgs e) {
            Button btn = (Button)sender;
            string[] indexes = btn.Name.Split(' ');
            int i = int.Parse(indexes[0]);
            int j = int.Parse(indexes[1]);

            cellClick(values[i, j], e);
        }

        public GridCell this[int i, int j] {
            get { return values[i, j]; }
            set { values[i, j] = value; }
        }
    }
}
