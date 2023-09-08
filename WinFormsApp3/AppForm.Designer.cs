namespace WinFormsApp3
{
    partial class AppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            buttonPlay = new Button();
            pictureBox1 = new PictureBox();
            buttonStop = new Button();
            panelLeft = new Panel();
            panelBottom = new Panel();
            timerListUpdate = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // buttonPlay
            // 
            buttonPlay.Anchor = AnchorStyles.Bottom;
            buttonPlay.Location = new Point(216, 9);
            buttonPlay.Margin = new Padding(4, 2, 4, 2);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(75, 25);
            buttonPlay.TabIndex = 1;
            buttonPlay.Text = "▷";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Location = new Point(186, 11);
            pictureBox1.Margin = new Padding(4, 2, 4, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(602, 438);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // buttonStop
            // 
            buttonStop.Anchor = AnchorStyles.Bottom;
            buttonStop.Location = new Point(299, 9);
            buttonStop.Margin = new Padding(4, 2, 4, 2);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(75, 25);
            buttonStop.TabIndex = 3;
            buttonStop.Text = "II";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // panelLeft
            // 
            panelLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelLeft.AutoScroll = true;
            panelLeft.Location = new Point(12, 12);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(167, 486);
            panelLeft.TabIndex = 4;
            // 
            // panelBottom
            // 
            panelBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.Controls.Add(buttonPlay);
            panelBottom.Controls.Add(buttonStop);
            panelBottom.Location = new Point(186, 454);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(602, 44);
            panelBottom.TabIndex = 5;
            // 
            // timerListUpdate
            // 
            timerListUpdate.Enabled = true;
            timerListUpdate.Interval = 1000;
            timerListUpdate.Tick += timerListUpdate_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(panelBottom);
            Controls.Add(panelLeft);
            Controls.Add(pictureBox1);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 2, 4, 2);
            Name = "Form1";
            Text = "Form1";
            Load += AppForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button buttonPlay;
        private PictureBox pictureBox1;
        private Button buttonStop;
        private Panel panelLeft;
        private Panel panelBottom;
        private System.Windows.Forms.Timer timerListUpdate;
    }
}
