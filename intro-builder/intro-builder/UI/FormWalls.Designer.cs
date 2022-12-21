
namespace eConFaire.RevitBuilder.Intro
{
    partial class FormWalls
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
            this.btn_createWall = new System.Windows.Forms.Button();
            this.lbl_lengthX = new System.Windows.Forms.Label();
            this.lbl_lengthY = new System.Windows.Forms.Label();
            this.num_LenX = new System.Windows.Forms.NumericUpDown();
            this.num_LenY = new System.Windows.Forms.NumericUpDown();
            this.txt_roomName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numRoomsNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdaugaIncapere = new System.Windows.Forms.Button();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_LenX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_LenY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRoomsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_createWall
            // 
            this.btn_createWall.Location = new System.Drawing.Point(58, 237);
            this.btn_createWall.Name = "btn_createWall";
            this.btn_createWall.Size = new System.Drawing.Size(75, 23);
            this.btn_createWall.TabIndex = 0;
            this.btn_createWall.Text = "Create Wall";
            this.btn_createWall.UseVisualStyleBackColor = true;
            this.btn_createWall.Click += new System.EventHandler(this.btn_createWall_Click);
            // 
            // lbl_lengthX
            // 
            this.lbl_lengthX.AutoSize = true;
            this.lbl_lengthX.Location = new System.Drawing.Point(5, 145);
            this.lbl_lengthX.Name = "lbl_lengthX";
            this.lbl_lengthX.Size = new System.Drawing.Size(47, 13);
            this.lbl_lengthX.TabIndex = 3;
            this.lbl_lengthX.Text = "LengthX";
            // 
            // lbl_lengthY
            // 
            this.lbl_lengthY.AutoSize = true;
            this.lbl_lengthY.Location = new System.Drawing.Point(3, 181);
            this.lbl_lengthY.Name = "lbl_lengthY";
            this.lbl_lengthY.Size = new System.Drawing.Size(47, 13);
            this.lbl_lengthY.TabIndex = 4;
            this.lbl_lengthY.Text = "LengthY";
            // 
            // num_LenX
            // 
            this.num_LenX.Location = new System.Drawing.Point(58, 145);
            this.num_LenX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_LenX.Name = "num_LenX";
            this.num_LenX.Size = new System.Drawing.Size(120, 20);
            this.num_LenX.TabIndex = 5;
            this.num_LenX.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // num_LenY
            // 
            this.num_LenY.Location = new System.Drawing.Point(58, 181);
            this.num_LenY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.num_LenY.Name = "num_LenY";
            this.num_LenY.Size = new System.Drawing.Size(120, 20);
            this.num_LenY.TabIndex = 6;
            this.num_LenY.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // txt_roomName
            // 
            this.txt_roomName.Location = new System.Drawing.Point(58, 102);
            this.txt_roomName.Name = "txt_roomName";
            this.txt_roomName.Size = new System.Drawing.Size(120, 20);
            this.txt_roomName.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numRoomsNumber
            // 
            this.numRoomsNumber.Location = new System.Drawing.Point(99, 55);
            this.numRoomsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRoomsNumber.Name = "numRoomsNumber";
            this.numRoomsNumber.Size = new System.Drawing.Size(79, 20);
            this.numRoomsNumber.TabIndex = 15;
            this.numRoomsNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRoomsNumber.ValueChanged += new System.EventHandler(this.numRoomsNumber_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Numar incaperi";
            // 
            // btnAdaugaIncapere
            // 
            this.btnAdaugaIncapere.Location = new System.Drawing.Point(44, 237);
            this.btnAdaugaIncapere.Name = "btnAdaugaIncapere";
            this.btnAdaugaIncapere.Size = new System.Drawing.Size(104, 23);
            this.btnAdaugaIncapere.TabIndex = 17;
            this.btnAdaugaIncapere.Text = "Adauga Incapere";
            this.btnAdaugaIncapere.UseVisualStyleBackColor = true;
            this.btnAdaugaIncapere.Visible = false;
            this.btnAdaugaIncapere.Click += new System.EventHandler(this.btnAdaugaIncapere_Click);
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Location = new System.Drawing.Point(72, 20);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(0, 13);
            this.lblRoomNumber.TabIndex = 18;
            // 
            // FormWalls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 308);
            this.Controls.Add(this.lblRoomNumber);
            this.Controls.Add(this.btnAdaugaIncapere);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numRoomsNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_roomName);
            this.Controls.Add(this.num_LenY);
            this.Controls.Add(this.num_LenX);
            this.Controls.Add(this.lbl_lengthY);
            this.Controls.Add(this.lbl_lengthX);
            this.Controls.Add(this.btn_createWall);
            this.Name = "FormWalls";
            this.Text = "FormWalls";
            this.Load += new System.EventHandler(this.FormWalls_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_LenX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_LenY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRoomsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_createWall;
        private System.Windows.Forms.Label lbl_lengthX;
        private System.Windows.Forms.Label lbl_lengthY;
        public System.Windows.Forms.NumericUpDown num_LenX;
        public System.Windows.Forms.NumericUpDown num_LenY;
        public System.Windows.Forms.TextBox txt_roomName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numRoomsNumber;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnAdaugaIncapere;
        private System.Windows.Forms.Label lblRoomNumber;
    }
}