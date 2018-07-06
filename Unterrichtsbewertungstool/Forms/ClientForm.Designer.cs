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
            this.tbscore.Location = new System.Drawing.Point(450, 49);
            this.tbscore.Maximum = 25;
            this.tbscore.Name = "tbscore";
            this.tbscore.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbscore.Size = new System.Drawing.Size(45, 220);
            this.tbscore.TabIndex = 0;
            this.tbscore.Scroll += new System.EventHandler(this.Tbscore_Scroll);
            // 
            // lblscore
            // 
            this.lblscore.AutoSize = true;
            this.lblscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblscore.Location = new System.Drawing.Point(460, 30);
            this.lblscore.Name = "lblscore";
            this.lblscore.Size = new System.Drawing.Size(63, 20);
            this.lblscore.TabIndex = 2;
            this.lblscore.Text = "lblscore";
            // 
            // pbdiagram
            // 
            this.pbdiagram.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbdiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbdiagram.Location = new System.Drawing.Point(45, 59);
            this.pbdiagram.Name = "pbdiagram";
            this.pbdiagram.Size = new System.Drawing.Size(400, 200);
            this.pbdiagram.TabIndex = 3;
            this.pbdiagram.TabStop = false;
            // 
            // lbldiatitle
            // 
            this.lbldiatitle.AutoSize = true;
            this.lbldiatitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldiatitle.Location = new System.Drawing.Point(40, 30);
            this.lbldiatitle.Name = "lbldiatitle";
            this.lbldiatitle.Size = new System.Drawing.Size(103, 26);
            this.lbldiatitle.TabIndex = 4;
            this.lbldiatitle.Text = "lbltdiatitle";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(241, 264);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
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
            this.lblzeitspanne.Location = new System.Drawing.Point(152, 266);
            this.lblzeitspanne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblzeitspanne.Name = "lblzeitspanne";
            this.lblzeitspanne.Size = new System.Drawing.Size(68, 13);
            this.lblzeitspanne.TabIndex = 6;
            this.lblzeitspanne.Text = "lblzeitspanne";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 291);
            this.Controls.Add(this.lblzeitspanne);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.lbldiatitle);
            this.Controls.Add(this.pbdiagram);
            this.Controls.Add(this.lblscore);
            this.Controls.Add(this.tbscore);
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