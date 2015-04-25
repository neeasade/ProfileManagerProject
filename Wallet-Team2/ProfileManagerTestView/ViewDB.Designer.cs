namespace ProfileManagerTestView
{
    partial class ViewDB
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
            this.uxUserBox = new System.Windows.Forms.ListBox();
            this.uxCurUserInfoBox = new System.Windows.Forms.ListBox();
            this.uxCurUserAddrBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.uxOpenDB = new System.Windows.Forms.OpenFileDialog();
            this.uxSaveDB = new System.Windows.Forms.SaveFileDialog();
            this.button5 = new System.Windows.Forms.Button();
            this.uxLoggedIn = new System.Windows.Forms.Label();
            this.uxLoggedOut = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uxUserBox
            // 
            this.uxUserBox.FormattingEnabled = true;
            this.uxUserBox.Items.AddRange(new object[] {
            "asdf",
            "asdf",
            "asdf",
            "asdf"});
            this.uxUserBox.Location = new System.Drawing.Point(12, 30);
            this.uxUserBox.Name = "uxUserBox";
            this.uxUserBox.Size = new System.Drawing.Size(220, 212);
            this.uxUserBox.TabIndex = 0;
            this.uxUserBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // uxCurUserInfoBox
            // 
            this.uxCurUserInfoBox.FormattingEnabled = true;
            this.uxCurUserInfoBox.Location = new System.Drawing.Point(238, 30);
            this.uxCurUserInfoBox.Name = "uxCurUserInfoBox";
            this.uxCurUserInfoBox.Size = new System.Drawing.Size(208, 212);
            this.uxCurUserInfoBox.TabIndex = 1;
            // 
            // uxCurUserAddrBox
            // 
            this.uxCurUserAddrBox.FormattingEnabled = true;
            this.uxCurUserAddrBox.Location = new System.Drawing.Point(452, 30);
            this.uxCurUserAddrBox.Name = "uxCurUserAddrBox";
            this.uxCurUserAddrBox.Size = new System.Drawing.Size(208, 212);
            this.uxCurUserAddrBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Users:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Current User Info:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(449, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Addresses for current User:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add User";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(288, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Edit Addresses";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(591, 302);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Save DB";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(591, 331);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Load DB";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // uxOpenDB
            // 
            this.uxOpenDB.FileName = "openFileDialog1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(288, 302);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Edit User";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // uxLoggedIn
            // 
            this.uxLoggedIn.AutoSize = true;
            this.uxLoggedIn.Location = new System.Drawing.Point(12, 245);
            this.uxLoggedIn.Name = "uxLoggedIn";
            this.uxLoggedIn.Size = new System.Drawing.Size(35, 13);
            this.uxLoggedIn.TabIndex = 11;
            this.uxLoggedIn.Text = "label4";
            // 
            // uxLoggedOut
            // 
            this.uxLoggedOut.AutoSize = true;
            this.uxLoggedOut.Location = new System.Drawing.Point(12, 270);
            this.uxLoggedOut.Name = "uxLoggedOut";
            this.uxLoggedOut.Size = new System.Drawing.Size(35, 13);
            this.uxLoggedOut.TabIndex = 12;
            this.uxLoggedOut.Text = "label5";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(379, 302);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(88, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "Log In/Out";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(379, 331);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(88, 23);
            this.button7.TabIndex = 14;
            this.button7.Text = "User ToString()";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(473, 331);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(112, 23);
            this.button8.TabIndex = 15;
            this.button8.Text = "Lookup Username";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(473, 302);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(112, 23);
            this.button9.TabIndex = 16;
            this.button9.Text = "Recover Password";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(205, 331);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(77, 23);
            this.button10.TabIndex = 17;
            this.button10.Text = "Delete User";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // ViewDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 364);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.uxLoggedOut);
            this.Controls.Add(this.uxLoggedIn);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxCurUserAddrBox);
            this.Controls.Add(this.uxCurUserInfoBox);
            this.Controls.Add(this.uxUserBox);
            this.Name = "ViewDB";
            this.Text = "UserDB Overview";
            this.Load += new System.EventHandler(this.ViewDB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox uxUserBox;
        private System.Windows.Forms.ListBox uxCurUserInfoBox;
        private System.Windows.Forms.ListBox uxCurUserAddrBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog uxOpenDB;
        private System.Windows.Forms.SaveFileDialog uxSaveDB;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label uxLoggedIn;
        private System.Windows.Forms.Label uxLoggedOut;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
    }
}

