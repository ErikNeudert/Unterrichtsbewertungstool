namespace Unterrichtsbewertungstool
{
    partial class HostForm
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
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.lblport = new System.Windows.Forms.Label();
            this.btnhost = new System.Windows.Forms.Button();
            this.cbip = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(12, 76);
            this.tbxPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(349, 22);
            this.tbxPort.TabIndex = 11;
            this.tbxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxPort.TextChanged += new System.EventHandler(this.tbxPort_TextChanged);
            this.tbxPort.Enter += new System.EventHandler(this.tbxPort_Enter);
            this.tbxPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxPort_KeyDown);
            this.tbxPort.Leave += new System.EventHandler(this.tbxPort_Leave);
            // 
            // lblport
            // 
            this.lblport.AutoSize = true;
            this.lblport.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblport.Location = new System.Drawing.Point(16, 11);
            this.lblport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblport.Name = "lblport";
            this.lblport.Size = new System.Drawing.Size(95, 31);
            this.lblport.TabIndex = 8;
            this.lblport.Text = "lblport";
            // 
            // btnhost
            // 
            this.btnhost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhost.Location = new System.Drawing.Point(16, 106);
            this.btnhost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnhost.Name = "btnhost";
            this.btnhost.Size = new System.Drawing.Size(347, 70);
            this.btnhost.TabIndex = 7;
            this.btnhost.Text = "btnhost";
            this.btnhost.UseVisualStyleBackColor = true;
            this.btnhost.Click += new System.EventHandler(this.btnhost_Click);
            // 
            // cbip
            // 
            this.cbip.FormattingEnabled = true;
            this.cbip.Location = new System.Drawing.Point(12, 45);
            this.cbip.Name = "cbip";
            this.cbip.Size = new System.Drawing.Size(351, 24);
            this.cbip.TabIndex = 10;
            this.cbip.Text = "cbip";
            this.cbip.SelectedIndexChanged += new System.EventHandler(this.cbip_SelectedIndexChanged);
            this.cbip.TextChanged += new System.EventHandler(this.cbip_TextChanged);
            // 
            // HostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 189);
            this.Controls.Add(this.cbip);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.lblport);
            this.Controls.Add(this.btnhost);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HostForm";
            this.Text = "Hosten";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Label lblport;
        private System.Windows.Forms.Button btnhost;
        private System.Windows.Forms.ComboBox cbip;
    }
}