namespace Gomoku {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.boardWidthLabel = new System.Windows.Forms.Label();
            this.boardWidthBox = new System.Windows.Forms.ComboBox();
            this.boardHeightBox = new System.Windows.Forms.ComboBox();
            this.boardHeightLabel = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectionColorBtn = new System.Windows.Forms.Button();
            this.aiMoveColorBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cellSizeBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.cellSizeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boardWidthLabel
            // 
            this.boardWidthLabel.AutoSize = true;
            this.boardWidthLabel.Font = new System.Drawing.Font("Arkhip", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boardWidthLabel.Location = new System.Drawing.Point(11, 22);
            this.boardWidthLabel.Name = "boardWidthLabel";
            this.boardWidthLabel.Size = new System.Drawing.Size(136, 19);
            this.boardWidthLabel.TabIndex = 0;
            this.boardWidthLabel.Text = "Ширина поля";
            // 
            // boardWidthBox
            // 
            this.boardWidthBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boardWidthBox.Font = new System.Drawing.Font("Arkhip", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boardWidthBox.FormattingEnabled = true;
            this.boardWidthBox.Items.AddRange(new object[] {
            "15",
            "19"});
            this.boardWidthBox.Location = new System.Drawing.Point(154, 20);
            this.boardWidthBox.Name = "boardWidthBox";
            this.boardWidthBox.Size = new System.Drawing.Size(53, 25);
            this.boardWidthBox.TabIndex = 1;
            // 
            // boardHeightBox
            // 
            this.boardHeightBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boardHeightBox.Font = new System.Drawing.Font("Arkhip", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boardHeightBox.FormattingEnabled = true;
            this.boardHeightBox.Items.AddRange(new object[] {
            "15",
            "19"});
            this.boardHeightBox.Location = new System.Drawing.Point(154, 57);
            this.boardHeightBox.Name = "boardHeightBox";
            this.boardHeightBox.Size = new System.Drawing.Size(53, 25);
            this.boardHeightBox.TabIndex = 3;
            // 
            // boardHeightLabel
            // 
            this.boardHeightLabel.AutoSize = true;
            this.boardHeightLabel.Font = new System.Drawing.Font("Arkhip", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boardHeightLabel.Location = new System.Drawing.Point(11, 59);
            this.boardHeightLabel.Name = "boardHeightLabel";
            this.boardHeightLabel.Size = new System.Drawing.Size(130, 19);
            this.boardHeightLabel.TabIndex = 2;
            this.boardHeightLabel.Text = "Высота поля";
            // 
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Arkhip", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveBtn.Location = new System.Drawing.Point(221, 202);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(111, 36);
            this.saveBtn.TabIndex = 4;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Arkhip", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backBtn.Location = new System.Drawing.Point(338, 202);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(111, 36);
            this.backBtn.TabIndex = 5;
            this.backBtn.Text = "Вернуться";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arkhip", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Цвет выделения";
            // 
            // selectionColorBtn
            // 
            this.selectionColorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectionColorBtn.Location = new System.Drawing.Point(182, 123);
            this.selectionColorBtn.Name = "selectionColorBtn";
            this.selectionColorBtn.Size = new System.Drawing.Size(25, 25);
            this.selectionColorBtn.TabIndex = 7;
            this.selectionColorBtn.UseVisualStyleBackColor = true;
            this.selectionColorBtn.Click += new System.EventHandler(this.selectionColorBtn_Click);
            // 
            // aiMoveColorBtn
            // 
            this.aiMoveColorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aiMoveColorBtn.Location = new System.Drawing.Point(182, 157);
            this.aiMoveColorBtn.Name = "aiMoveColorBtn";
            this.aiMoveColorBtn.Size = new System.Drawing.Size(25, 25);
            this.aiMoveColorBtn.TabIndex = 9;
            this.aiMoveColorBtn.UseVisualStyleBackColor = true;
            this.aiMoveColorBtn.Click += new System.EventHandler(this.aiMoveColorBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arkhip", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(11, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Цвет хода AI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arkhip", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(11, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "Размер поля";
            // 
            // cellSizeBox
            // 
            this.cellSizeBox.Font = new System.Drawing.Font("Arkhip", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cellSizeBox.Location = new System.Drawing.Point(154, 91);
            this.cellSizeBox.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.cellSizeBox.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.cellSizeBox.Name = "cellSizeBox";
            this.cellSizeBox.Size = new System.Drawing.Size(53, 23);
            this.cellSizeBox.TabIndex = 11;
            this.cellSizeBox.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 250);
            this.Controls.Add(this.cellSizeBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.aiMoveColorBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectionColorBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.boardHeightBox);
            this.Controls.Add(this.boardHeightLabel);
            this.Controls.Add(this.boardWidthBox);
            this.Controls.Add(this.boardWidthLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.cellSizeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label boardWidthLabel;
        private System.Windows.Forms.ComboBox boardWidthBox;
        private System.Windows.Forms.ComboBox boardHeightBox;
        private System.Windows.Forms.Label boardHeightLabel;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectionColorBtn;
        private System.Windows.Forms.Button aiMoveColorBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown cellSizeBox;
    }
}