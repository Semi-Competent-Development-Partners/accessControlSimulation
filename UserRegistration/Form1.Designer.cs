namespace UserRegistration {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            labelCardId = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            txtBoxFullName = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtBoxPosition = new TextBox();
            btnClear = new Button();
            btnSubmit = new Button();
            SuspendLayout();
            // 
            // labelCardId
            // 
            labelCardId.AutoSize = true;
            labelCardId.Font = new Font("Segoe UI", 12F);
            labelCardId.Location = new Point(12, 9);
            labelCardId.Name = "labelCardId";
            labelCardId.Size = new Size(134, 21);
            labelCardId.TabIndex = 0;
            labelCardId.Text = "Detected Card ID: ";
            // 
            // txtBoxFullName
            // 
            txtBoxFullName.Font = new Font("Segoe UI", 12F);
            txtBoxFullName.Location = new Point(138, 44);
            txtBoxFullName.Name = "txtBoxFullName";
            txtBoxFullName.Size = new Size(263, 29);
            txtBoxFullName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(81, 21);
            label2.TabIndex = 2;
            label2.Text = "Full name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(12, 92);
            label3.Name = "label3";
            label3.Size = new Size(68, 21);
            label3.TabIndex = 4;
            label3.Text = "Position:";
            // 
            // txtBoxPosition
            // 
            txtBoxPosition.Font = new Font("Segoe UI", 12F);
            txtBoxPosition.Location = new Point(138, 89);
            txtBoxPosition.Name = "txtBoxPosition";
            txtBoxPosition.Size = new Size(263, 29);
            txtBoxPosition.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 12F);
            btnClear.Location = new Point(12, 151);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 32);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += BtnClear_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.Font = new Font("Segoe UI", 12F);
            btnSubmit.Location = new Point(326, 151);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 32);
            btnSubmit.TabIndex = 6;
            btnSubmit.Text = "Register";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += BtnSubmit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 196);
            Controls.Add(btnSubmit);
            Controls.Add(btnClear);
            Controls.Add(label3);
            Controls.Add(txtBoxPosition);
            Controls.Add(label2);
            Controls.Add(txtBoxFullName);
            Controls.Add(labelCardId);
            Name = "Form1";
            Text = "User Management";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCardId;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox txtBoxFullName;
        private Label label2;
        private Label label3;
        private TextBox txtBoxPosition;
        private Button btnClear;
        private Button btnSubmit;
    }
}
