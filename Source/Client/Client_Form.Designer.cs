namespace Client
{
    partial class Client_Form
    {
        private System.ComponentModel.IContainer components = null;

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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OutputMessageBox = new System.Windows.Forms.TextBox();
            this.UserTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTitle = new System.Windows.Forms.Label();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.OutputMessageBtn = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OutputMessageBox
            // 
            this.OutputMessageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputMessageBox.Location = new System.Drawing.Point(12, 415);
            this.OutputMessageBox.Name = "OutputMessageBox";
            this.OutputMessageBox.Size = new System.Drawing.Size(488, 23);
            this.OutputMessageBox.TabIndex = 0;
            // 
            // UserTextBox
            // 
            this.UserTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserTextBox.Location = new System.Drawing.Point(120, 12);
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new System.Drawing.Size(177, 23);
            this.UserTextBox.TabIndex = 3;
            // 
            // UserNameTitle
            // 
            this.UserNameTitle.AutoSize = true;
            this.UserNameTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserNameTitle.Location = new System.Drawing.Point(12, 14);
            this.UserNameTitle.Name = "UserNameTitle";
            this.UserNameTitle.Size = new System.Drawing.Size(105, 20);
            this.UserNameTitle.TabIndex = 2;
            this.UserNameTitle.Text = "使用者名稱：";
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(314, 12);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(47, 23);
            this.LoginBtn.TabIndex = 4;
            this.LoginBtn.Text = "登入";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // OutputMessageBtn
            // 
            this.OutputMessageBtn.Location = new System.Drawing.Point(506, 415);
            this.OutputMessageBtn.Name = "OutputMessageBtn";
            this.OutputMessageBtn.Size = new System.Drawing.Size(47, 23);
            this.OutputMessageBtn.TabIndex = 8;
            this.OutputMessageBtn.Text = "輸出";
            this.OutputMessageBtn.UseVisualStyleBackColor = true;
            this.OutputMessageBtn.Click += new System.EventHandler(this.OutputMessageBtn_Click);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(12, 46);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(541, 357);
            this.MessageTextBox.TabIndex = 9;
            this.MessageTextBox.Text = "";
            // 
            // Client_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(565, 450);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.OutputMessageBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.UserNameTitle);
            this.Controls.Add(this.UserTextBox);
            this.Controls.Add(this.OutputMessageBox);
            this.Name = "Client_Form";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox OutputMessageBox;
        private TextBox UserTextBox;
        private Label UserNameTitle;
        private Button LoginBtn;
        private Button OutputMessageBtn;
        private RichTextBox MessageTextBox;
    }
}