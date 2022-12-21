
namespace eConFaire.RevitBuilder.Intro.UI
{
    partial class FormRoof
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
            this.numOverhang = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numTerasaInaltime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numOverhang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTerasaInaltime)).BeginInit();
            this.SuspendLayout();
            // 
            // numOverhang
            // 
            this.numOverhang.Location = new System.Drawing.Point(28, 41);
            this.numOverhang.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numOverhang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOverhang.Name = "numOverhang";
            this.numOverhang.Size = new System.Drawing.Size(120, 20);
            this.numOverhang.TabIndex = 0;
            this.numOverhang.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Roof Overhang";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inaltime terasa";
            // 
            // numTerasaInaltime
            // 
            this.numTerasaInaltime.Location = new System.Drawing.Point(28, 100);
            this.numTerasaInaltime.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numTerasaInaltime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTerasaInaltime.Name = "numTerasaInaltime";
            this.numTerasaInaltime.Size = new System.Drawing.Size(120, 20);
            this.numTerasaInaltime.TabIndex = 4;
            this.numTerasaInaltime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FormRoof
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 200);
            this.Controls.Add(this.numTerasaInaltime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numOverhang);
            this.Name = "FormRoof";
            this.Text = "FormRoof";
            ((System.ComponentModel.ISupportInitialize)(this.numOverhang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTerasaInaltime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NumericUpDown numOverhang;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numTerasaInaltime;
    }
}