using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Gomoku {
    class Grid {
        public struct GridCell {
            public PictureBox button;
            public int i;
            public int j;
        }

        public readonly int width; // ширина поля
        public readonly int height; // высота поля
        int cellSize; // размер клетки

        Size size;
        Point location;

        public event CellClick cellClick;
        public delegate void CellClick(object sender, EventArgs eventArgs);

        GridCell[,] values;
        Image selectingImage;

        public Grid(int width, int height, int cellSize, Point location, Form form) {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            this.location = location;

            size = new Size(width * cellSize, height * cellSize);
            values = new GridCell[height, width];

            selectingImage = CreateSelectionImage(2 * cellSize / 3, cellSize / 5);

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    values[i, j].button = new PictureBox();
                    values[i, j].i = i;
                    values[i, j].j = j;

                    values[i, j].button.Location = new Point(location.X + j * (cellSize - 1), location.Y + i * (cellSize - 1));
                    values[i, j].button.Size = new Size(cellSize, cellSize);
                    values[i, j].button.SizeMode = PictureBoxSizeMode.StretchImage;

                    values[i, j].button.Name = i + " " + j;
                    values[i, j].button.DoubleClick += ButtonClick;
                    values[i, j].button.MouseEnter += Button_MouseEnter;
                    values[i, j].button.MouseLeave += Button_MouseLeave;

                    Image image;

                    if (i == 3 && j == 3 || i == height - 4 && j == width - 4 || i == 3 && j == width - 4 || i == height - 4 && j == 3 || i == height / 2 && j == width / 2) {
                        image = Properties.Resources.gomokuCellPoint;
                    }
                    else if (i == 0 && j == 0) {
                        image = Properties.Resources.gomokuCellLeftTop;
                    }
                    else if (i == 0 && j == width - 1) {
                        image = Properties.Resources.gomokuCellRightTop;
                    }
                    else if (i == height - 1 && j == width - 1) {
                        image = Properties.Resources.gomokuCellRightBottom;
                    }
                    else if (i == height - 1 && j == 0) {
                        image = Properties.Resources.gomokuCellLeftBottom;
                    }
                    else if (i == 0) {
                        image = Properties.Resources.gomokuCellTop;
                    }
                    else if (j == 0) {
                        image = Properties.Resources.gomokuCellLeft;
                    }
                    else if (i == height - 1) {
                        image = Properties.Resources.gomokuCellBottom;
                    }
                    else if (j == width - 1) {
                        image = Properties.Resources.gomokuCellRight;
                    }
                    else {
                        image = Properties.Resources.gomokuCell;
                    }

                    values[i, j].button.BackColor = Color.Transparent;
                    values[i, j].button.BackgroundImage = image;
                    values[i, j].button.BackgroundImageLayout = ImageLayout.Stretch;

                    form.Controls.Add(values[i, j].button);
                }
            }

            DrawLabels(form);
        }

        private Image CreateSelectionImage(int size, int radius) {
            Bitmap image = new Bitmap(cellSize, cellSize);

            Graphics g = Graphics.FromImage(image);
            SolidBrush brush = new SolidBrush(Properties.Settings.Default.BoardSelectionBackColor);
            Pen pen = new Pen(Properties.Settings.Default.BoardSelectionBackColor, 2);
            g.FillEllipse(brush, (cellSize - radius) / 2, (cellSize - radius) / 2, radius, radius);

            int x0 = (cellSize - size) / 2;
            int y0 = (cellSize - size) / 2;

            g.DrawLine(pen, x0, y0, x0 + size / 4, y0);
            g.DrawLine(pen, x0, y0, x0, y0 + size / 4);
            g.DrawLine(pen, x0 + size, y0, x0 + 3 * size / 4, y0);
            g.DrawLine(pen, x0 + size, y0, x0 + size, y0 + size / 4);

            g.DrawLine(pen, x0, y0 + size, x0 + size / 4, y0 + size);
            g.DrawLine(pen, x0, y0 + size, x0, y0 + 3 * size / 4);

            g.DrawLine(pen, x0 + size, y0 + size, x0 + 3 * size / 4, y0 + size);
            g.DrawLine(pen, x0 + size, y0 + size, x0 + size, y0 + 3 * size / 4);

            return image;
        }

        void DrawLabels(Form form) {
            for (int i = 0; i < height; i++) {
                Label label = new Label();
                label.Text = (height - i).ToString();
                label.AutoSize = true;
                label.Font = new Font("Arkhip", cellSize / 5);
                label.BackColor = Color.Transparent;
                label.Location = new Point(location.X - label.PreferredWidth + 5, location.Y + i * (cellSize - 1) + cellSize / 3);

                form.Controls.Add(label);
            }

            for (int j = 0; j < width; j++) {
                Label label = new Label();
                label.Text = ((char)('A' + j)).ToString();
                label.AutoSize = true;
                label.Font = new Font("Arkhip", cellSize / 5);
                label.BackColor = Color.Transparent;
                label.Location = new Point(location.X + j * (cellSize - 1) + cellSize / 3, location.Y - label.PreferredHeight + 3);

                form.Controls.Add(label);
            }
        }

        public void Reset() {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    values[i, j].button.BackColor = Color.Transparent;
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e) {
            PictureBox box = (PictureBox)sender;

            if (box.Image == null)
                box.Image = selectingImage;
        }

        private void Button_MouseLeave(object sender, EventArgs e) {
            PictureBox box = (PictureBox)sender;

            if (box.Image == selectingImage)
                box.Image = null;
        }

        void ButtonClick(object sender, EventArgs e) {
            PictureBox btn = (PictureBox)sender;
            string[] indexes = btn.Name.Split(' ');
            int i = int.Parse(indexes[0]);
            int j = int.Parse(indexes[1]);

            cellClick(values[i, j], e);
        }

        public GridCell this[int i, int j] {
            get { return values[i, j]; }
            set { values[i, j] = value; }
        }

        public Size GetSize() {
            return size;
        }

        public Point GetLocation() {
            return location;
        }
    }
}
