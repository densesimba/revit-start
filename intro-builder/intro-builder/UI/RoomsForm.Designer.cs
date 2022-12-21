
namespace eConFaire.RevitBuilder.Intro.UI
{
    partial class RoomsForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.numRoomsNumber = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numRoomsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Numar incaperi";
            // 
            // numRoomsNumber
            // 
            this.numRoomsNumber.Location = new System.Drawing.Point(96, 18);
            this.numRoomsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRoomsNumber.Name = "numRoomsNumber";
            this.numRoomsNumber.Size = new System.Drawing.Size(79, 20);
            this.numRoomsNumber.TabIndex = 14;
            this.numRoomsNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRoomsNumber.ValueChanged += new System.EventHandler(this.numRoomsNumber_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 46);
            this.button1.TabIndex = 16;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RoomsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 347);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numRoomsNumber);
            this.Name = "RoomsForm";
            this.Text = "RoomsForm";
            ((System.ComponentModel.ISupportInitialize)(this.numRoomsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numRoomsNumber;
        private System.Windows.Forms.Button button1;
    }
}