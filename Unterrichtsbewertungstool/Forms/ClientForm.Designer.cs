namespace Unterrichtsbewertungstool
{
    partial class ClientForm
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
            this.tbscore = new System.Windows.Forms.TrackBar();
            this.lblscore = new System.Windows.Forms.Label();
            this.pbdiagram = new System.Windows.Forms.PictureBox();
            this.lbldiatitle = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblzeitspanne = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbscore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbdiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbscore
            // 
            this.tbscore.Location = new System.Drawing.Point(675, 75);
            this.tbscore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbscore.Maximum = 25;
            this.tbscore.Name = "tbscore";
            this.tbscore.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbscore.Size = new System.Drawing.Size(56, 338);
            this.tbscore.TabIndex = 0;
            this.tbscore.Scroll += new System.EventHandler(this.Tbscore_Scroll);
            // 
            // lblscore
            // 
            this.lblscore.AutoSize = true;
            this.lblscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblscore.Location = new System.Drawing.Point(690, 46);
            this.lblscore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblscore.Name = "lblscore";
            this.lblscore.Size = new System.Drawing.Size(79, 25);
            this.lblscore.TabIndex = 2;
            this.lblscore.Text = "lblscore";
            // 
            // pbdiagram
            // 
            this.pbdiagram.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbdiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbdiagram.Location = new System.Drawing.Point(68, 91);
            this.pbdiagram.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbdiagram.Name = "pbdiagram";
            this.pbdiagram.Size = new System.Drawing.Size(599, 307);
            this.pbdiagram.TabIndex = 3;
            this.pbdiagram.TabStop = false;
            // 
            // lbldiatitle
            // 
            this.lbldiatitle.AutoSize = true;
            this.lbldiatitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldiatitle.Location = new System.Drawing.Point(60, 46);
            this.lbldiatitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldiatitle.Name = "lbldiatitle";
            this.lbldiatitle.Size = new System.Drawing.Size(128, 31);
            this.lbldiatitle.TabIndex = 4;
            this.lbldiatitle.Text = "lbltdiatitle";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(362, 406);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // lblzeitspanne
            // 
            this.lblzeitspanne.AutoSize = true;
            this.lblzeitspanne.Location = new System.Drawing.Point(221, 409);
            this.lblzeitspanne.Name = "lblzeitspanne";
            this.lblzeitspanne.Size = new System.Drawing.Size(139, 20);
            this.lblzeitspanne.TabIndex = 6;
            this.lblzeitspanne.Text = "Zeitspanne (min):";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 448);
            this.Controls.Add(this.lblzeitspanne);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.lbldiatitle);
            this.Controls.Add(this.pbdiagram);
            this.Controls.Add(this.lblscore);
            this.Controls.Add(this.tbscore);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ClientForm";
            this.Text = "Statistik";
            ((System.ComponentModel.ISupportInitialize)(this.tbscore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbdiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbscore;
        private System.Windows.Forms.Label lblscore;
        private System.Windows.Forms.PictureBox pbdiagram;
        private System.Windows.Forms.Label lbldiatitle;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblzeitspanne;
    }
}