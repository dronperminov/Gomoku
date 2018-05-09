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
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();

            boardWidthBox.SelectedItem = Properties.Settings.Default.BoardWidth.ToString();
            boardHeightBox.SelectedItem = Properties.Settings.Default.BoardHeight.ToString();

            selectionColorBtn.BackColor = Properties.Settings.Default.BoardSelectionBackColor;
            aiMoveColorBtn.BackColor = Properties.Settings.Default.AImoveBackColor;
            winLineColorBtn.BackColor = Properties.Settings.Default.winLineColor;

            cellSizeBox.Value = Properties.Settings.Default.CellSize;
        }

        private Color SelectColor(Color startColor) {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = Properties.Settings.Default.BoardSelectionBackColor;

            if (dialog.ShowDialog() != DialogResult.OK)
                return startColor;

            return dialog.Color;
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            Properties.Settings.Default.BoardWidth = int.Parse(boardWidthBox.SelectedItem.ToString());
            Properties.Settings.Default.BoardHeight = int.Parse(boardHeightBox.SelectedItem.ToString());
            Properties.Settings.Default.BoardSelectionBackColor = selectionColorBtn.BackColor;
            Properties.Settings.Default.AImoveBackColor = aiMoveColorBtn.BackColor;
            Properties.Settings.Default.winLineColor = winLineColorBtn.BackColor;
            Properties.Settings.Default.CellSize = (int) cellSizeBox.Value;
            Properties.Settings.Default.Save();

            Close();
        }

        private void backBtn_Click(object sender, EventArgs e) {
            Close();
        }

        private void selectionColorBtn_Click(object sender, EventArgs e) {
            selectionColorBtn.BackColor = SelectColor(Properties.Settings.Default.BoardSelectionBackColor);
        }

        private void aiMoveColorBtn_Click(object sender, EventArgs e) {
            aiMoveColorBtn.BackColor = SelectColor(Properties.Settings.Default.AImoveBackColor);
        }

        private void winLineColorBtn_Click(object sender, EventArgs e) {
            winLineColorBtn.BackColor = SelectColor(Properties.Settings.Default.winLineColor);
        }
    }
}
