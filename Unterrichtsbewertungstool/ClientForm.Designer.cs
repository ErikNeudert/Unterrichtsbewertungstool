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
            ((System.ComponentModel.ISupportInitialize)(this.tbscore)).BeginInit();
            this.SuspendLayout();
            // 
            // tbscore
            // 
            this.tbscore.Location = new System.Drawing.Point(13, 346);
            this.tbscore.Name = "tbscore";
            this.tbscore.Size = new System.Drawing.Size(516, 45);
            this.tbscore.TabIndex = 0;
            this.tbscore.Scroll += new System.EventHandler(this.tbscore_Scroll);
            // 
            // lblscore
            // 
            this.lblscore.AutoSize = true;
            this.lblscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblscore.Location = new System.Drawing.Point(221, 323);
            this.lblscore.Name = "lblscore";
            this.lblscore.Size = new System.Drawing.Size(63, 20);
            this.lblscore.TabIndex = 2;
            this.lblscore.Text = "lblscore";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 403);
            this.Controls.Add(this.lblscore);
            this.Controls.Add(this.tbscore);
            this.Name = "ClientForm";
            this.Text = "Statistik";
            this.Load += new System.EventHandler(this.DiagramForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DiagramForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.tbscore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbscore;
        private System.Windows.Forms.Label lblscore;

    }
}