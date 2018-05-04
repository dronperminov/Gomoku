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
    public partial class StartForm : Form {
        public StartForm() {
            InitializeComponent();
        }

        private void gameBtn_Click(object sender, EventArgs e) {
            MainForm form = new MainForm();
            Hide();
            form.ShowDialog();
            Show();
        }

        private void settingsBtn_Click(object sender, EventArgs e) {
            SettingsForm form = new SettingsForm();
            Hide();
            form.ShowDialog();
            Show();
        }

        private void exitBtn_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
