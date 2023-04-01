namespace Robit_Game
{
    partial class RobitGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MPBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SouthButton = new System.Windows.Forms.Button();
            this.WestButton = new System.Windows.Forms.Button();
            this.EastButton = new System.Windows.Forms.Button();
            this.NorthButton = new System.Windows.Forms.Button();
            this.InventoryBox = new System.Windows.Forms.ListBox();
            this.LoreBox = new System.Windows.Forms.ListBox();
            this.AHPBar = new System.Windows.Forms.ProgressBar();
            this.SHPBar = new System.Windows.Forms.ProgressBar();
            this.KHPBar = new System.Windows.Forms.ProgressBar();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.AButton = new System.Windows.Forms.Button();
            this.KButton = new System.Windows.Forms.Button();
            this.SButton = new System.Windows.Forms.Button();
            this.E1Button = new System.Windows.Forms.Button();
            this.E2Button = new System.Windows.Forms.Button();
            this.E3Button = new System.Windows.Forms.Button();
            this.E4Button = new System.Windows.Forms.Button();
            this.E1HPBar = new System.Windows.Forms.ProgressBar();
            this.E4HPBar = new System.Windows.Forms.ProgressBar();
            this.E3HPBar = new System.Windows.Forms.ProgressBar();
            this.E2HPBar = new System.Windows.Forms.ProgressBar();
            this.DescriptionBox = new System.Windows.Forms.ListBox();
            this.MPLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MPBar
            // 
            this.MPBar.Location = new System.Drawing.Point(12, 568);
            this.MPBar.Name = "MPBar";
            this.MPBar.Size = new System.Drawing.Size(1214, 53);
            this.MPBar.Step = 1;
            this.MPBar.TabIndex = 0;
            this.MPBar.Value = 100;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1583, 1002);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 118);
            this.button1.TabIndex = 3;
            this.button1.Text = "Items";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1270, 1002);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(288, 118);
            this.button2.TabIndex = 4;
            this.button2.Text = "Badges";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SouthButton
            // 
            this.SouthButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SouthButton.Location = new System.Drawing.Point(1431, 859);
            this.SouthButton.Name = "SouthButton";
            this.SouthButton.Size = new System.Drawing.Size(288, 118);
            this.SouthButton.TabIndex = 5;
            this.SouthButton.Text = "South";
            this.SouthButton.UseVisualStyleBackColor = true;
            this.SouthButton.Click += new System.EventHandler(this.SouthButton_Click);
            // 
            // WestButton
            // 
            this.WestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WestButton.Location = new System.Drawing.Point(1270, 724);
            this.WestButton.Name = "WestButton";
            this.WestButton.Size = new System.Drawing.Size(288, 118);
            this.WestButton.TabIndex = 6;
            this.WestButton.Text = "West";
            this.WestButton.UseVisualStyleBackColor = true;
            this.WestButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // EastButton
            // 
            this.EastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EastButton.Location = new System.Drawing.Point(1583, 724);
            this.EastButton.Name = "EastButton";
            this.EastButton.Size = new System.Drawing.Size(288, 118);
            this.EastButton.TabIndex = 7;
            this.EastButton.Text = "East";
            this.EastButton.UseVisualStyleBackColor = true;
            this.EastButton.Click += new System.EventHandler(this.EastButton_Click);
            // 
            // NorthButton
            // 
            this.NorthButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NorthButton.Location = new System.Drawing.Point(1431, 588);
            this.NorthButton.Name = "NorthButton";
            this.NorthButton.Size = new System.Drawing.Size(288, 118);
            this.NorthButton.TabIndex = 8;
            this.NorthButton.Text = "North";
            this.NorthButton.UseVisualStyleBackColor = true;
            this.NorthButton.Click += new System.EventHandler(this.NorthButton_Click);
            // 
            // InventoryBox
            // 
            this.InventoryBox.FormattingEnabled = true;
            this.InventoryBox.ItemHeight = 20;
            this.InventoryBox.Items.AddRange(new object[] {
            "Item list:",
            "Eggs",
            "Curry Powder",
            "8563 Ducks"});
            this.InventoryBox.Location = new System.Drawing.Point(1270, 12);
            this.InventoryBox.Name = "InventoryBox";
            this.InventoryBox.Size = new System.Drawing.Size(601, 484);
            this.InventoryBox.TabIndex = 9;
            this.InventoryBox.SelectedIndexChanged += new System.EventHandler(this.InventoryBox_SelectedIndexChanged);
            // 
            // LoreBox
            // 
            this.LoreBox.FormattingEnabled = true;
            this.LoreBox.ItemHeight = 20;
            this.LoreBox.Items.AddRange(new object[] {
            "This is a sample",
            "Not a great sample",
            "But this is what it will look like."});
            this.LoreBox.Location = new System.Drawing.Point(12, 636);
            this.LoreBox.Name = "LoreBox";
            this.LoreBox.Size = new System.Drawing.Size(1214, 484);
            this.LoreBox.TabIndex = 10;
            this.LoreBox.SelectedIndexChanged += new System.EventHandler(this.LoreBox_SelectedIndexChanged);
            // 
            // AHPBar
            // 
            this.AHPBar.Location = new System.Drawing.Point(12, 503);
            this.AHPBar.Maximum = 8;
            this.AHPBar.Name = "AHPBar";
            this.AHPBar.Size = new System.Drawing.Size(398, 59);
            this.AHPBar.Step = 1;
            this.AHPBar.TabIndex = 11;
            this.AHPBar.Value = 3;
            this.AHPBar.Click += new System.EventHandler(this.AHPBar_Click);
            // 
            // SHPBar
            // 
            this.SHPBar.Location = new System.Drawing.Point(828, 503);
            this.SHPBar.Name = "SHPBar";
            this.SHPBar.Size = new System.Drawing.Size(398, 59);
            this.SHPBar.Step = 1;
            this.SHPBar.TabIndex = 12;
            this.SHPBar.Value = 100;
            this.SHPBar.Click += new System.EventHandler(this.SHPBar_Click);
            // 
            // KHPBar
            // 
            this.KHPBar.Enabled = false;
            this.KHPBar.Location = new System.Drawing.Point(416, 503);
            this.KHPBar.Name = "KHPBar";
            this.KHPBar.Size = new System.Drawing.Size(406, 59);
            this.KHPBar.Step = 1;
            this.KHPBar.TabIndex = 13;
            this.KHPBar.Value = 100;
            this.KHPBar.Click += new System.EventHandler(this.progressBar4_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.InitialImage = global::Robit_Game.Properties.Resources.evenbricks;
            this.PictureBox.Location = new System.Drawing.Point(12, 125);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(1214, 313);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox.TabIndex = 17;
            this.PictureBox.TabStop = false;
            // 
            // AButton
            // 
            this.AButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AButton.Location = new System.Drawing.Point(12, 444);
            this.AButton.Name = "AButton";
            this.AButton.Size = new System.Drawing.Size(398, 53);
            this.AButton.TabIndex = 18;
            this.AButton.Text = "Abel (3/8)";
            this.AButton.UseVisualStyleBackColor = true;
            this.AButton.Click += new System.EventHandler(this.AButton_Click);
            // 
            // KButton
            // 
            this.KButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KButton.Location = new System.Drawing.Point(416, 444);
            this.KButton.Name = "KButton";
            this.KButton.Size = new System.Drawing.Size(406, 53);
            this.KButton.TabIndex = 19;
            this.KButton.Text = "K-813 (8/8)";
            this.KButton.UseVisualStyleBackColor = true;
            this.KButton.Click += new System.EventHandler(this.KButton_Click);
            // 
            // SButton
            // 
            this.SButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SButton.Location = new System.Drawing.Point(828, 444);
            this.SButton.Name = "SButton";
            this.SButton.Size = new System.Drawing.Size(398, 53);
            this.SButton.TabIndex = 20;
            this.SButton.Text = "Sarge (8/8)";
            this.SButton.UseVisualStyleBackColor = true;
            this.SButton.Click += new System.EventHandler(this.SButton_Click);
            // 
            // E1Button
            // 
            this.E1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.E1Button.Location = new System.Drawing.Point(12, 12);
            this.E1Button.Name = "E1Button";
            this.E1Button.Size = new System.Drawing.Size(292, 53);
            this.E1Button.TabIndex = 22;
            this.E1Button.Text = "Enemy 1 (4/4)";
            this.E1Button.UseVisualStyleBackColor = true;
            this.E1Button.Click += new System.EventHandler(this.E1Button_Click);
            // 
            // E2Button
            // 
            this.E2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.E2Button.Location = new System.Drawing.Point(320, 12);
            this.E2Button.Name = "E2Button";
            this.E2Button.Size = new System.Drawing.Size(292, 53);
            this.E2Button.TabIndex = 23;
            this.E2Button.Text = "Enemy 2 (4/4)";
            this.E2Button.UseVisualStyleBackColor = true;
            this.E2Button.Click += new System.EventHandler(this.E2Button_Click);
            // 
            // E3Button
            // 
            this.E3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.E3Button.Location = new System.Drawing.Point(627, 12);
            this.E3Button.Name = "E3Button";
            this.E3Button.Size = new System.Drawing.Size(292, 53);
            this.E3Button.TabIndex = 24;
            this.E3Button.Text = "Enemy 3 (4/4)";
            this.E3Button.UseVisualStyleBackColor = true;
            this.E3Button.Click += new System.EventHandler(this.E3Button_Click);
            // 
            // E4Button
            // 
            this.E4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.E4Button.Location = new System.Drawing.Point(934, 12);
            this.E4Button.Name = "E4Button";
            this.E4Button.Size = new System.Drawing.Size(292, 53);
            this.E4Button.TabIndex = 25;
            this.E4Button.Text = "Enemy 4 (4/4)";
            this.E4Button.UseVisualStyleBackColor = true;
            this.E4Button.Click += new System.EventHandler(this.E4Button_Click);
            // 
            // E1HPBar
            // 
            this.E1HPBar.Location = new System.Drawing.Point(13, 71);
            this.E1HPBar.Name = "E1HPBar";
            this.E1HPBar.Size = new System.Drawing.Size(291, 48);
            this.E1HPBar.Step = 1;
            this.E1HPBar.TabIndex = 26;
            this.E1HPBar.Value = 100;
            this.E1HPBar.Click += new System.EventHandler(this.progressBar5_Click);
            // 
            // E4HPBar
            // 
            this.E4HPBar.Location = new System.Drawing.Point(934, 71);
            this.E4HPBar.Name = "E4HPBar";
            this.E4HPBar.Size = new System.Drawing.Size(291, 48);
            this.E4HPBar.Step = 1;
            this.E4HPBar.TabIndex = 27;
            this.E4HPBar.Value = 100;
            this.E4HPBar.Click += new System.EventHandler(this.E4HPBar_Click);
            // 
            // E3HPBar
            // 
            this.E3HPBar.Location = new System.Drawing.Point(628, 71);
            this.E3HPBar.Name = "E3HPBar";
            this.E3HPBar.Size = new System.Drawing.Size(291, 48);
            this.E3HPBar.Step = 1;
            this.E3HPBar.TabIndex = 28;
            this.E3HPBar.Value = 100;
            this.E3HPBar.Click += new System.EventHandler(this.E3HPBar_Click);
            // 
            // E2HPBar
            // 
            this.E2HPBar.Location = new System.Drawing.Point(320, 71);
            this.E2HPBar.Name = "E2HPBar";
            this.E2HPBar.Size = new System.Drawing.Size(291, 48);
            this.E2HPBar.Step = 1;
            this.E2HPBar.TabIndex = 29;
            this.E2HPBar.Value = 100;
            this.E2HPBar.Click += new System.EventHandler(this.E2HPBar_Click);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.FormattingEnabled = true;
            this.DescriptionBox.ItemHeight = 20;
            this.DescriptionBox.Items.AddRange(new object[] {
            "Eggs are food you should eat. They are yummy and good for you.",
            "Please consider this!"});
            this.DescriptionBox.Location = new System.Drawing.Point(1270, 503);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(601, 64);
            this.DescriptionBox.TabIndex = 30;
            this.DescriptionBox.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // MPLabel
            // 
            this.MPLabel.AutoSize = true;
            this.MPLabel.BackColor = System.Drawing.Color.Transparent;
            this.MPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MPLabel.Location = new System.Drawing.Point(1232, 573);
            this.MPLabel.Name = "MPLabel";
            this.MPLabel.Size = new System.Drawing.Size(124, 48);
            this.MPLabel.TabIndex = 38;
            this.MPLabel.Text = "10/10";
            this.MPLabel.Click += new System.EventHandler(this.MPLabel_Click);
            // 
            // RobitGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1144);
            this.Controls.Add(this.MPLabel);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.E2HPBar);
            this.Controls.Add(this.E3HPBar);
            this.Controls.Add(this.E4HPBar);
            this.Controls.Add(this.E1HPBar);
            this.Controls.Add(this.E4Button);
            this.Controls.Add(this.E3Button);
            this.Controls.Add(this.E2Button);
            this.Controls.Add(this.E1Button);
            this.Controls.Add(this.SButton);
            this.Controls.Add(this.KButton);
            this.Controls.Add(this.AButton);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.KHPBar);
            this.Controls.Add(this.SHPBar);
            this.Controls.Add(this.AHPBar);
            this.Controls.Add(this.LoreBox);
            this.Controls.Add(this.InventoryBox);
            this.Controls.Add(this.NorthButton);
            this.Controls.Add(this.EastButton);
            this.Controls.Add(this.WestButton);
            this.Controls.Add(this.SouthButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MPBar);
            this.Name = "RobitGame";
            this.Text = "Robit Game";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar MPBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox InventoryBox;
        private System.Windows.Forms.ProgressBar AHPBar;
        private System.Windows.Forms.ProgressBar SHPBar;
        private System.Windows.Forms.ProgressBar KHPBar;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button AButton;
        private System.Windows.Forms.Button KButton;
        private System.Windows.Forms.Button SButton;
        private System.Windows.Forms.Button E1Button;
        private System.Windows.Forms.Button E2Button;
        private System.Windows.Forms.Button E3Button;
        private System.Windows.Forms.Button E4Button;
        private System.Windows.Forms.ProgressBar E1HPBar;
        private System.Windows.Forms.ProgressBar E4HPBar;
        private System.Windows.Forms.ProgressBar E3HPBar;
        private System.Windows.Forms.ProgressBar E2HPBar;
        private System.Windows.Forms.ListBox DescriptionBox;
        private System.Windows.Forms.Label MPLabel;
        public System.Windows.Forms.ListBox LoreBox;
        public System.Windows.Forms.Button SouthButton;
        public System.Windows.Forms.Button WestButton;
        public System.Windows.Forms.Button EastButton;
        public System.Windows.Forms.Button NorthButton;
    }
}

