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
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetProgressMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelMoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.careerModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certainLevelModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectLevelBox = new System.Windows.Forms.ToolStripComboBox();
            this.friendToFriendModeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.complexityLabel = new System.Windows.Forms.Label();
            this.rulesHeadlineLabel = new System.Windows.Forms.Label();
            this.rulesLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.currMoveLabel = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.winsPerLevelLabel = new System.Windows.Forms.Label();
            this.lossPerLevelLabel = new System.Windows.Forms.Label();
            this.playerPicture = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerPicture)).BeginInit();
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
            this.optionsMenuItem,
            this.changeModeMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(769, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetProgressMenuItem,
            this.cancelMoveMenuItem,
            this.menuExitMenuItem,
            this.ExitMenuItem});
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsMenuItem.Text = "Опции";
            // 
            // resetProgressMenuItem
            // 
            this.resetProgressMenuItem.Name = "resetProgressMenuItem";
            this.resetProgressMenuItem.ShortcutKeyDisplayString = "Ctrl+Alt+R";
            this.resetProgressMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.R)));
            this.resetProgressMenuItem.Size = new System.Drawing.Size(245, 22);
            this.resetProgressMenuItem.Text = "Сбросить прогресс";
            this.resetProgressMenuItem.Click += new System.EventHandler(this.resetProgressMenuItem_Click);
            // 
            // cancelMoveMenuItem
            // 
            this.cancelMoveMenuItem.Name = "cancelMoveMenuItem";
            this.cancelMoveMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.cancelMoveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.cancelMoveMenuItem.Size = new System.Drawing.Size(245, 22);
            this.cancelMoveMenuItem.Text = "Отменить ход";
            this.cancelMoveMenuItem.Click += new System.EventHandler(this.cancelMoveMenuItem_Click);
            // 
            // menuExitMenuItem
            // 
            this.menuExitMenuItem.Name = "menuExitMenuItem";
            this.menuExitMenuItem.ShortcutKeyDisplayString = "Ctrl+M";
            this.menuExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menuExitMenuItem.Size = new System.Drawing.Size(245, 22);
            this.menuExitMenuItem.Text = "Выйти в меню";
            this.menuExitMenuItem.Click += new System.EventHandler(this.menuExitMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeyDisplayString = "Ctrl+Q";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.ExitMenuItem.Size = new System.Drawing.Size(245, 22);
            this.ExitMenuItem.Text = "Выйти";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // changeModeMenuItem
            // 
            this.changeModeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.careerModeMenuItem,
            this.certainLevelModeMenuItem,
            this.friendToFriendModeMenuItem});
            this.changeModeMenuItem.Name = "changeModeMenuItem";
            this.changeModeMenuItem.Size = new System.Drawing.Size(108, 20);
            this.changeModeMenuItem.Text = "Сменить режим";
            // 
            // careerModeMenuItem
            // 
            this.careerModeMenuItem.Name = "careerModeMenuItem";
            this.careerModeMenuItem.Size = new System.Drawing.Size(190, 22);
            this.careerModeMenuItem.Text = "Карьера";
            this.careerModeMenuItem.Click += new System.EventHandler(this.careerModeMenuItem_Click);
            // 
            // certainLevelModeMenuItem
            // 
            this.certainLevelModeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectLevelBox});
            this.certainLevelModeMenuItem.Name = "certainLevelModeMenuItem";
            this.certainLevelModeMenuItem.Size = new System.Drawing.Size(190, 22);
            this.certainLevelModeMenuItem.Text = "Конкретный уровень";
            // 
            // selectLevelBox
            // 
            this.selectLevelBox.BackColor = System.Drawing.Color.White;
            this.selectLevelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectLevelBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.selectLevelBox.Name = "selectLevelBox";
            this.selectLevelBox.Size = new System.Drawing.Size(121, 23);
            this.selectLevelBox.SelectedIndexChanged += new System.EventHandler(this.selectLevelBox_SelectedIndexChanged);
            // 
            // friendToFriendModeMenuItem
            // 
            this.friendToFriendModeMenuItem.Name = "friendToFriendModeMenuItem";
            this.friendToFriendModeMenuItem.Size = new System.Drawing.Size(190, 22);
            this.friendToFriendModeMenuItem.Text = "Друг против друга";
            this.friendToFriendModeMenuItem.Click += new System.EventHandler(this.friendToFriendModeMenuItem_Click);
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
            this.currMoveLabel.Location = new System.Drawing.Point(537, 301);
            this.currMoveLabel.Name = "currMoveLabel";
            this.currMoveLabel.Size = new System.Drawing.Size(163, 23);
            this.currMoveLabel.TabIndex = 11;
            this.currMoveLabel.Text = "Сейчас ходят:";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.progressLabel.ForeColor = System.Drawing.Color.White;
            this.progressLabel.Location = new System.Drawing.Point(537, 254);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(122, 23);
            this.progressLabel.TabIndex = 13;
            this.progressLabel.Text = "Прогресс: ";
            // 
            // winsPerLevelLabel
            // 
            this.winsPerLevelLabel.AutoSize = true;
            this.winsPerLevelLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.winsPerLevelLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.winsPerLevelLabel.ForeColor = System.Drawing.Color.White;
            this.winsPerLevelLabel.Location = new System.Drawing.Point(660, 254);
            this.winsPerLevelLabel.Name = "winsPerLevelLabel";
            this.winsPerLevelLabel.Size = new System.Drawing.Size(24, 23);
            this.winsPerLevelLabel.TabIndex = 14;
            this.winsPerLevelLabel.Text = "0";
            // 
            // lossPerLevelLabel
            // 
            this.lossPerLevelLabel.AutoSize = true;
            this.lossPerLevelLabel.BackColor = System.Drawing.Color.Red;
            this.lossPerLevelLabel.Font = new System.Drawing.Font("Arkhip", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lossPerLevelLabel.ForeColor = System.Drawing.Color.White;
            this.lossPerLevelLabel.Location = new System.Drawing.Point(704, 254);
            this.lossPerLevelLabel.Name = "lossPerLevelLabel";
            this.lossPerLevelLabel.Size = new System.Drawing.Size(24, 23);
            this.lossPerLevelLabel.TabIndex = 15;
            this.lossPerLevelLabel.Text = "0";
            // 
            // playerPicture
            // 
            this.playerPicture.BackColor = System.Drawing.Color.Transparent;
            this.playerPicture.Location = new System.Drawing.Point(708, 297);
            this.playerPicture.Name = "playerPicture";
            this.playerPicture.Size = new System.Drawing.Size(35, 35);
            this.playerPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playerPicture.TabIndex = 16;
            this.playerPicture.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(769, 548);
            this.Controls.Add(this.playerPicture);
            this.Controls.Add(this.lossPerLevelLabel);
            this.Controls.Add(this.winsPerLevelLabel);
            this.Controls.Add(this.progressLabel);
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
            this.Text = "Гомоку";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label winsLabel;
        private System.Windows.Forms.Label lossLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label complexityLabel;
        private System.Windows.Forms.Label rulesHeadlineLabel;
        private System.Windows.Forms.Label rulesLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label currMoveLabel;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label winsPerLevelLabel;
        private System.Windows.Forms.Label lossPerLevelLabel;
        private System.Windows.Forms.ToolStripMenuItem changeModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem careerModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certainLevelModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem friendToFriendModeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetProgressMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelMoveMenuItem;
        private System.Windows.Forms.ToolStripComboBox selectLevelBox;
        private System.Windows.Forms.PictureBox playerPicture;
    }
}

