﻿namespace RexSimulatorGui.Controls
{
    partial class RexWidget
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RexWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DoubleBuffered = true;
            this.Name = "RexWidget";
            this.Size = new System.Drawing.Size(800, 600);
            this.Click += new System.EventHandler(this.RexWidget_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RexWidget_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RexWidget_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RexWidget_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RexWidget_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
