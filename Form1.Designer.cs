namespace Robit_Game
{
    partial class Form1
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MPBar
            // 
            this.MPBar.Location = new System.Drawing.Point(12, 568);
            this.MPBar.Name = "MPBar";
            this.MPBar.Size = new System.Drawing.Size(1214, 53);
            this.MPBar.TabIndex = 0;
            this.MPBar.Value = 50;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1583, 1002);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(288, 118);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1270, 1002);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(288, 118);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1431, 859);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(288, 118);
            this.button3.TabIndex = 5;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1270, 724);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(288, 118);
            this.button4.TabIndex = 6;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1583, 724);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(288, 118);
            this.button5.TabIndex = 7;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1431, 588);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(288, 118);
            this.button6.TabIndex = 8;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // InventoryBox
            // 
            this.InventoryBox.FormattingEnabled = true;
            this.InventoryBox.ItemHeight = 20;
            this.InventoryBox.Location = new System.Drawing.Point(1270, 12);
            this.InventoryBox.Name = "InventoryBox";
            this.InventoryBox.Size = new System.Drawing.Size(601, 484);
            this.InventoryBox.TabIndex = 9;
            // 
            // LoreBox
            // 
            this.LoreBox.FormattingEnabled = true;
            this.LoreBox.ItemHeight = 20;
            this.LoreBox.Location = new System.Drawing.Point(12, 636);
            this.LoreBox.Name = "LoreBox";
            this.LoreBox.Size = new System.Drawing.Size(1214, 484);
            this.LoreBox.TabIndex = 10;
            // 
            // AHPBar
            // 
            this.AHPBar.Location = new System.Drawing.Point(12, 503);
            this.AHPBar.Name = "AHPBar";
            this.AHPBar.Size = new System.Drawing.Size(398, 59);
            this.AHPBar.TabIndex = 11;
            this.AHPBar.Value = 50;
            // 
            // SHPBar
            // 
            this.SHPBar.Location = new System.Drawing.Point(828, 503);
            this.SHPBar.Name = "SHPBar";
            this.SHPBar.Size = new System.Drawing.Size(398, 59);
            this.SHPBar.TabIndex = 12;
            this.SHPBar.Value = 50;
            // 
            // KHPBar
            // 
            this.KHPBar.Enabled = false;
            this.KHPBar.Location = new System.Drawing.Point(416, 503);
            this.KHPBar.Name = "KHPBar";
            this.KHPBar.Size = new System.Drawing.Size(406, 59);
            this.KHPBar.TabIndex = 13;
            this.KHPBar.Value = 50;
            this.KHPBar.Click += new System.EventHandler(this.progressBar4_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 125);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(1214, 313);
            this.PictureBox.TabIndex = 17;
            this.PictureBox.TabStop = false;
            // 
            // AButton
            // 
            this.AButton.Location = new System.Drawing.Point(12, 444);
            this.AButton.Name = "AButton";
            this.AButton.Size = new System.Drawing.Size(398, 53);
            this.AButton.TabIndex = 18;
            this.AButton.Text = "Abel";
            this.AButton.UseVisualStyleBackColor = true;
            // 
            // KButton
            // 
            this.KButton.Location = new System.Drawing.Point(416, 444);
            this.KButton.Name = "KButton";
            this.KButton.Size = new System.Drawing.Size(406, 53);
            this.KButton.TabIndex = 19;
            this.KButton.Text = "K-813";
            this.KButton.UseVisualStyleBackColor = true;
            // 
            // SButton
            // 
            this.SButton.Location = new System.Drawing.Point(828, 444);
            this.SButton.Name = "SButton";
            this.SButton.Size = new System.Drawing.Size(398, 53);
            this.SButton.TabIndex = 20;
            this.SButton.Text = "Sarge";
            this.SButton.UseVisualStyleBackColor = true;
            // 
            // E1Button
            // 
            this.E1Button.Location = new System.Drawing.Point(12, 12);
            this.E1Button.Name = "E1Button";
            this.E1Button.Size = new System.Drawing.Size(292, 53);
            this.E1Button.TabIndex = 22;
            this.E1Button.Text = "Enemy 1";
            this.E1Button.UseVisualStyleBackColor = true;
            // 
            // E2Button
            // 
            this.E2Button.Location = new System.Drawing.Point(320, 12);
            this.E2Button.Name = "E2Button";
            this.E2Button.Size = new System.Drawing.Size(292, 53);
            this.E2Button.TabIndex = 23;
            this.E2Button.Text = "Enemy 2";
            this.E2Button.UseVisualStyleBackColor = true;
            // 
            // E3Button
            // 
            this.E3Button.Location = new System.Drawing.Point(627, 12);
            this.E3Button.Name = "E3Button";
            this.E3Button.Size = new System.Drawing.Size(292, 53);
            this.E3Button.TabIndex = 24;
            this.E3Button.Text = "Enemy 3";
            this.E3Button.UseVisualStyleBackColor = true;
            // 
            // E4Button
            // 
            this.E4Button.Location = new System.Drawing.Point(934, 12);
            this.E4Button.Name = "E4Button";
            this.E4Button.Size = new System.Drawing.Size(292, 53);
            this.E4Button.TabIndex = 25;
            this.E4Button.Text = "Enemy 4";
            this.E4Button.UseVisualStyleBackColor = true;
            // 
            // E1HPBar
            // 
            this.E1HPBar.Location = new System.Drawing.Point(13, 71);
            this.E1HPBar.Name = "E1HPBar";
            this.E1HPBar.Size = new System.Drawing.Size(291, 48);
            this.E1HPBar.TabIndex = 26;
            this.E1HPBar.Click += new System.EventHandler(this.progressBar5_Click);
            // 
            // E4HPBar
            // 
            this.E4HPBar.Location = new System.Drawing.Point(934, 71);
            this.E4HPBar.Name = "E4HPBar";
            this.E4HPBar.Size = new System.Drawing.Size(291, 48);
            this.E4HPBar.TabIndex = 27;
            // 
            // E3HPBar
            // 
            this.E3HPBar.Location = new System.Drawing.Point(628, 71);
            this.E3HPBar.Name = "E3HPBar";
            this.E3HPBar.Size = new System.Drawing.Size(291, 48);
            this.E3HPBar.TabIndex = 28;
            // 
            // E2HPBar
            // 
            this.E2HPBar.Location = new System.Drawing.Point(320, 71);
            this.E2HPBar.Name = "E2HPBar";
            this.E2HPBar.Size = new System.Drawing.Size(291, 48);
            this.E2HPBar.TabIndex = 29;
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.FormattingEnabled = true;
            this.DescriptionBox.ItemHeight = 20;
            this.DescriptionBox.Location = new System.Drawing.Point(1270, 503);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(601, 64);
            this.DescriptionBox.TabIndex = 30;
            this.DescriptionBox.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1144);
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
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MPBar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar MPBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox InventoryBox;
        private System.Windows.Forms.ListBox LoreBox;
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
    }
}

