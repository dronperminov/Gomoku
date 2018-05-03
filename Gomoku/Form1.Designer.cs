namespace Gomoku {
    partial class MainForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.winsLabel = new System.Windows.Forms.Label();
            this.lossLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.RestartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetProgressMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.complexityLabel = new System.Windows.Forms.Label();
            this.rulesHeadlineLabel = new System.Windows.Forms.Label();
            this.rulesLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.currMoveLabel = new System.Windows.Forms.Label();
            this.playerLabel = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // winsLabel
            // 
            this.winsLabel.AutoSize = true;
            this.winsLabel.BackColor = System.Drawing.Color.Transparent;
            this.winsLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.winsLabel.ForeColor = System.Drawing.Color.White;
            this.winsLabel.Location = new System.Drawing.Point(537, 121);
            this.winsLabel.Name = "winsLabel";
            this.winsLabel.Size = new System.Drawing.Size(102, 23);
            this.winsLabel.TabIndex = 2;
            this.winsLabel.Text = "Побед: 0";
            // 
            // lossLabel
            // 
            this.lossLabel.AutoSize = true;
            this.lossLabel.BackColor = System.Drawing.Color.Transparent;
            this.lossLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lossLabel.ForeColor = System.Drawing.Color.White;
            this.lossLabel.Location = new System.Drawing.Point(537, 151);
            this.lossLabel.Name = "lossLabel";
            this.lossLabel.Size = new System.Drawing.Size(162, 23);
            this.lossLabel.TabIndex = 3;
            this.lossLabel.Text = "Поражений: 0";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestartMenuItem,
            this.resetProgressMenuItem,
            this.ExitMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(769, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // RestartMenuItem
            // 
            this.RestartMenuItem.Name = "RestartMenuItem";
            this.RestartMenuItem.ShortcutKeyDisplayString = "ctrl+R";
            this.RestartMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.RestartMenuItem.Size = new System.Drawing.Size(144, 20);
            this.RestartMenuItem.Text = "Начать партию заново";
            this.RestartMenuItem.Click += new System.EventHandler(this.RestartMenuItem_Click);
            // 
            // resetProgressMenuItem
            // 
            this.resetProgressMenuItem.Name = "resetProgressMenuItem";
            this.resetProgressMenuItem.Size = new System.Drawing.Size(126, 20);
            this.resetProgressMenuItem.Text = "Сбросить прогресс";
            this.resetProgressMenuItem.Click += new System.EventHandler(this.resetProgressMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeyDisplayString = "ctrl+Q";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.ExitMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ExitMenuItem.Text = "Выход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.gameNameLabel.Font = new System.Drawing.Font("Arkhip", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gameNameLabel.ForeColor = System.Drawing.Color.White;
            this.gameNameLabel.Location = new System.Drawing.Point(292, 30);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(164, 38);
            this.gameNameLabel.TabIndex = 5;
            this.gameNameLabel.Text = "Gomoku";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.BackColor = System.Drawing.Color.Transparent;
            this.levelLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.levelLabel.ForeColor = System.Drawing.Color.White;
            this.levelLabel.Location = new System.Drawing.Point(537, 187);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(121, 23);
            this.levelLabel.TabIndex = 6;
            this.levelLabel.Text = "Уровень: 1";
            // 
            // complexityLabel
            // 
            this.complexityLabel.AutoSize = true;
            this.complexityLabel.BackColor = System.Drawing.Color.Transparent;
            this.complexityLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.complexityLabel.ForeColor = System.Drawing.Color.White;
            this.complexityLabel.Location = new System.Drawing.Point(537, 218);
            this.complexityLabel.Name = "complexityLabel";
            this.complexityLabel.Size = new System.Drawing.Size(191, 23);
            this.complexityLabel.TabIndex = 7;
            this.complexityLabel.Text = "Сложность: 0.04";
            // 
            // rulesHeadlineLabel
            // 
            this.rulesHeadlineLabel.AutoSize = true;
            this.rulesHeadlineLabel.BackColor = System.Drawing.Color.Transparent;
            this.rulesHeadlineLabel.Font = new System.Drawing.Font("Arkhip", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rulesHeadlineLabel.ForeColor = System.Drawing.Color.White;
            this.rulesHeadlineLabel.Location = new System.Drawing.Point(546, 412);
            this.rulesHeadlineLabel.Name = "rulesHeadlineLabel";
            this.rulesHeadlineLabel.Size = new System.Drawing.Size(211, 19);
            this.rulesHeadlineLabel.TabIndex = 8;
            this.rulesHeadlineLabel.Text = "Правила и цель игры:";
            // 
            // rulesLabel
            // 
            this.rulesLabel.AutoSize = true;
            this.rulesLabel.BackColor = System.Drawing.Color.Transparent;
            this.rulesLabel.Font = new System.Drawing.Font("Ubuntu", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rulesLabel.ForeColor = System.Drawing.Color.White;
            this.rulesLabel.Location = new System.Drawing.Point(520, 435);
            this.rulesLabel.Name = "rulesLabel";
            this.rulesLabel.Size = new System.Drawing.Size(249, 108);
            this.rulesLabel.TabIndex = 9;
            this.rulesLabel.Text = resources.GetString("rulesLabel.Text");
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.totalLabel.ForeColor = System.Drawing.Color.White;
            this.totalLabel.Location = new System.Drawing.Point(537, 91);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(140, 23);
            this.totalLabel.TabIndex = 10;
            this.totalLabel.Text = "Всего игр: 0";
            // 
            // currMoveLabel
            // 
            this.currMoveLabel.AutoSize = true;
            this.currMoveLabel.BackColor = System.Drawing.Color.Transparent;
            this.currMoveLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currMoveLabel.ForeColor = System.Drawing.Color.White;
            this.currMoveLabel.Location = new System.Drawing.Point(537, 253);
            this.currMoveLabel.Name = "currMoveLabel";
            this.currMoveLabel.Size = new System.Drawing.Size(163, 23);
            this.currMoveLabel.TabIndex = 11;
            this.currMoveLabel.Text = "Сейчас ходят:";
            // 
            // playerLabel
            // 
            this.playerLabel.AutoSize = true;
            this.playerLabel.BackColor = System.Drawing.Color.Transparent;
            this.playerLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerLabel.ForeColor = System.Drawing.Color.White;
            this.playerLabel.Location = new System.Drawing.Point(700, 253);
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(38, 23);
            this.playerLabel.TabIndex = 12;
            this.playerLabel.Text = "hu";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(769, 548);
            this.Controls.Add(this.playerLabel);
            this.Controls.Add(this.currMoveLabel);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.rulesLabel);
            this.Controls.Add(this.rulesHeadlineLabel);
            this.Controls.Add(this.complexityLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.lossLabel);
            this.Controls.Add(this.winsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gomoku";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label winsLabel;
        private System.Windows.Forms.Label lossLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RestartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetProgressMenuItem;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label complexityLabel;
        private System.Windows.Forms.Label rulesHeadlineLabel;
        private System.Windows.Forms.Label rulesLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label currMoveLabel;
        private System.Windows.Forms.Label playerLabel;
    }
}

