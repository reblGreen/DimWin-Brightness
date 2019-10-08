namespace DimWin
{
    partial class Brightness
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Brightness));
            this.OverlayValue = new System.Windows.Forms.Label();
            this.BrightnessValue = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BrightnessSlider = new DimWin.NoFocusSlider();
            this.OverlaySlider = new DimWin.NoFocusSlider();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OverlaySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // OverlayValue
            // 
            this.OverlayValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OverlayValue.ForeColor = System.Drawing.Color.White;
            this.OverlayValue.Location = new System.Drawing.Point(250, 1);
            this.OverlayValue.Name = "OverlayValue";
            this.OverlayValue.Size = new System.Drawing.Size(42, 45);
            this.OverlayValue.TabIndex = 1;
            this.OverlayValue.Text = "100";
            this.OverlayValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BrightnessValue
            // 
            this.BrightnessValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrightnessValue.ForeColor = System.Drawing.Color.White;
            this.BrightnessValue.Location = new System.Drawing.Point(250, 46);
            this.BrightnessValue.Name = "BrightnessValue";
            this.BrightnessValue.Size = new System.Drawing.Size(42, 45);
            this.BrightnessValue.TabIndex = 4;
            this.BrightnessValue.Text = "100";
            this.BrightnessValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DimWin.Properties.Resources.brightness;
            this.pictureBox2.Location = new System.Drawing.Point(10, 58);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 22);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DimWin.Properties.Resources.contrast;
            this.pictureBox1.Location = new System.Drawing.Point(10, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // BrightnessSlider
            // 
            this.BrightnessSlider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BrightnessSlider.Location = new System.Drawing.Point(49, 52);
            this.BrightnessSlider.Maximum = 100;
            this.BrightnessSlider.Minimum = 1;
            this.BrightnessSlider.Name = "BrightnessSlider";
            this.BrightnessSlider.Size = new System.Drawing.Size(200, 45);
            this.BrightnessSlider.TabIndex = 3;
            this.BrightnessSlider.TickFrequency = 5;
            this.BrightnessSlider.Value = 1;
            // 
            // OverlaySlider
            // 
            this.OverlaySlider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OverlaySlider.Location = new System.Drawing.Point(49, 7);
            this.OverlaySlider.Maximum = 100;
            this.OverlaySlider.Minimum = 25;
            this.OverlaySlider.Name = "OverlaySlider";
            this.OverlaySlider.Size = new System.Drawing.Size(200, 45);
            this.OverlaySlider.TabIndex = 0;
            this.OverlaySlider.TickFrequency = 5;
            this.OverlaySlider.Value = 100;
            // 
            // Brightness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(301, 104);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.BrightnessValue);
            this.Controls.Add(this.BrightnessSlider);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OverlayValue);
            this.Controls.Add(this.OverlaySlider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Brightness";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "dimwin";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OverlaySlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NoFocusSlider OverlaySlider;
        private System.Windows.Forms.Label OverlayValue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label BrightnessValue;
        private NoFocusSlider BrightnessSlider;
    }
}

