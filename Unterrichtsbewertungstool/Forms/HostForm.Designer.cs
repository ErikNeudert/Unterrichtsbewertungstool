﻿namespace Unterrichtsbewertungstool
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
            this.lblHostTitle = new System.Windows.Forms.Label();
            this.btnhost = new System.Windows.Forms.Button();
            this.cbip = new System.Windows.Forms.ComboBox();
            this.tbxTitel = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(117, 148);
            this.tbxPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(289, 26);
            this.tbxPort.TabIndex = 13;
            this.tbxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxPort.TextChanged += new System.EventHandler(this.TbxPort_TextChanged);
            this.tbxPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxPort_KeyDown);
            this.tbxPort.Leave += new System.EventHandler(this.TbxPort_Leave);
            // 
            // lblHostTitle
            // 
            this.lblHostTitle.AutoSize = true;
            this.lblHostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHostTitle.Location = new System.Drawing.Point(18, 14);
            this.lblHostTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHostTitle.Name = "lblHostTitle";
            this.lblHostTitle.Size = new System.Drawing.Size(162, 31);
            this.lblHostTitle.TabIndex = 8;
            this.lblHostTitle.Text = "lblHostTitle";
            // 
            // btnhost
            // 
            this.btnhost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhost.Location = new System.Drawing.Point(12, 188);
            this.btnhost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnhost.Name = "btnhost";
            this.btnhost.Size = new System.Drawing.Size(396, 88);
            this.btnhost.TabIndex = 14;
            this.btnhost.Text = "btnhost";
            this.btnhost.UseVisualStyleBackColor = true;
            this.btnhost.Click += new System.EventHandler(this.Btnhost_Click);
            // 
            // cbip
            // 
            this.cbip.FormattingEnabled = true;
            this.cbip.Location = new System.Drawing.Point(117, 108);
            this.cbip.Name = "cbip";
            this.cbip.Size = new System.Drawing.Size(289, 28);
            this.cbip.TabIndex = 12;
            this.cbip.Text = "cbip";
            this.cbip.SelectedIndexChanged += new System.EventHandler(this.Cbip_SelectedIndexChanged);
            this.cbip.TextChanged += new System.EventHandler(this.Cbip_TextChanged);
            // 
            // tbxTitel
            // 
            this.tbxTitel.Location = new System.Drawing.Point(117, 69);
            this.tbxTitel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxTitel.Name = "tbxTitel";
            this.tbxTitel.Size = new System.Drawing.Size(288, 26);
            this.tbxTitel.TabIndex = 10;
            this.tbxTitel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxTitel.TextChanged += new System.EventHandler(this.TbxTitel_TextChanged);
            this.tbxTitel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbxTitel_KeyDown);
            this.tbxTitel.Leave += new System.EventHandler(this.TbxTitel_Leave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(21, 71);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(58, 20);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "lblTitle";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(21, 109);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(41, 20);
            this.lblIP.TabIndex = 14;
            this.lblIP.Text = "lblIP";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(21, 149);
            this.lblPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(57, 20);
            this.lblPort.TabIndex = 15;
            this.lblPort.Text = "lblPort";
            // 
            // HostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 294);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tbxTitel);
            this.Controls.Add(this.cbip);
            this.Controls.Add(this.tbxPort);
            this.Controls.Add(this.lblHostTitle);
            this.Controls.Add(this.btnhost);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HostForm";
            this.Text = "Hosten";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Label lblHostTitle;
        private System.Windows.Forms.Button btnhost;
        private System.Windows.Forms.ComboBox cbip;
        private System.Windows.Forms.TextBox tbxTitel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPort;
    }
}