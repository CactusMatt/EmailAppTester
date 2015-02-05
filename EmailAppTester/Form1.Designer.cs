namespace EmailAppTester
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StartButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.hasAttachments = new System.Windows.Forms.CheckBox();
            this.addAttachmentButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.filesAttachedListview = new System.Windows.Forms.ListView();
            this.progressLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.toValue = new System.Windows.Forms.Label();
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromValue = new System.Windows.Forms.Label();
            this.subLabel = new System.Windows.Forms.Label();
            this.displayedSubject = new System.Windows.Forms.Label();
            this.fileSentLabel = new System.Windows.Forms.Label();
            this.displayedSentFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(34, 484);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(371, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // StartButton
            // 
            this.StartButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StartButton.BackgroundImage")));
            this.StartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(145, 205);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(247, 169);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start Composition";
            this.StartButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.StartButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Location = new System.Drawing.Point(165, 122);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(251, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Location = new System.Drawing.Point(171, 145);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(150, 13);
            this.subjectLabel.TabIndex = 3;
            this.subjectLabel.Text = "Enter text for email subject line";
            this.subjectLabel.Visible = false;
            // 
            // hasAttachments
            // 
            this.hasAttachments.AutoSize = true;
            this.hasAttachments.BackColor = System.Drawing.SystemColors.ControlLight;
            this.hasAttachments.Location = new System.Drawing.Point(29, 525);
            this.hasAttachments.Name = "hasAttachments";
            this.hasAttachments.Size = new System.Drawing.Size(234, 17);
            this.hasAttachments.TabIndex = 4;
            this.hasAttachments.Text = "Check here if this message has attachments\r\n";
            this.hasAttachments.UseVisualStyleBackColor = false;
            this.hasAttachments.Visible = false;
            this.hasAttachments.CheckedChanged += new System.EventHandler(this.hasAttachments_CheckedChanged);
            // 
            // addAttachmentButton
            // 
            this.addAttachmentButton.Location = new System.Drawing.Point(72, 456);
            this.addAttachmentButton.Name = "addAttachmentButton";
            this.addAttachmentButton.Size = new System.Drawing.Size(99, 23);
            this.addAttachmentButton.TabIndex = 5;
            this.addAttachmentButton.Text = "Add Attachment";
            this.addAttachmentButton.UseVisualStyleBackColor = true;
            this.addAttachmentButton.Visible = false;
            this.addAttachmentButton.Click += new System.EventHandler(this.addAttachmentButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(350, 455);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Send Email!";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Visible = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // filesAttachedListview
            // 
            this.filesAttachedListview.Location = new System.Drawing.Point(80, 380);
            this.filesAttachedListview.Name = "filesAttachedListview";
            this.filesAttachedListview.RightToLeftLayout = true;
            this.filesAttachedListview.Size = new System.Drawing.Size(353, 69);
            this.filesAttachedListview.TabIndex = 7;
            this.filesAttachedListview.UseCompatibleStateImageBehavior = false;
            this.filesAttachedListview.View = System.Windows.Forms.View.SmallIcon;
            this.filesAttachedListview.Visible = false;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(104, 506);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(116, 13);
            this.progressLabel.TabIndex = 8;
            this.progressLabel.Text = "X of Y Sends Complete";
            this.progressLabel.Visible = false;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.toLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.toLabel.Location = new System.Drawing.Point(29, 24);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(44, 28);
            this.toLabel.TabIndex = 9;
            this.toLabel.Text = "To:";
            this.toLabel.Visible = false;
            // 
            // toValue
            // 
            this.toValue.AutoSize = true;
            this.toValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toValue.Location = new System.Drawing.Point(156, 34);
            this.toValue.Name = "toValue";
            this.toValue.Size = new System.Drawing.Size(45, 16);
            this.toValue.TabIndex = 10;
            this.toValue.Text = "label2";
            this.toValue.Visible = false;
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.fromLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.fromLabel.Location = new System.Drawing.Point(29, 65);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(71, 28);
            this.fromLabel.TabIndex = 11;
            this.fromLabel.Text = "From:";
            this.fromLabel.Visible = false;
            // 
            // fromValue
            // 
            this.fromValue.AutoSize = true;
            this.fromValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromValue.Location = new System.Drawing.Point(156, 77);
            this.fromValue.Name = "fromValue";
            this.fromValue.Size = new System.Drawing.Size(45, 16);
            this.fromValue.TabIndex = 12;
            this.fromValue.Text = "label1";
            this.fromValue.Visible = false;
            // 
            // subLabel
            // 
            this.subLabel.AutoSize = true;
            this.subLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.subLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.subLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.subLabel.Location = new System.Drawing.Point(29, 116);
            this.subLabel.Name = "subLabel";
            this.subLabel.Size = new System.Drawing.Size(93, 28);
            this.subLabel.TabIndex = 13;
            this.subLabel.Text = "Subject:";
            this.subLabel.Visible = false;
            // 
            // displayedSubject
            // 
            this.displayedSubject.AutoSize = true;
            this.displayedSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayedSubject.Location = new System.Drawing.Point(156, 122);
            this.displayedSubject.Name = "displayedSubject";
            this.displayedSubject.Size = new System.Drawing.Size(408, 17);
            this.displayedSubject.TabIndex = 14;
            this.displayedSubject.Text = "Default Subject can be as long as I wish it to be, and even more";
            this.displayedSubject.Visible = false;
            // 
            // fileSentLabel
            // 
            this.fileSentLabel.AutoSize = true;
            this.fileSentLabel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.fileSentLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fileSentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileSentLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.fileSentLabel.Location = new System.Drawing.Point(31, 178);
            this.fileSentLabel.Name = "fileSentLabel";
            this.fileSentLabel.Size = new System.Drawing.Size(124, 26);
            this.fileSentLabel.TabIndex = 15;
            this.fileSentLabel.Text = "Sending File:";
            this.fileSentLabel.Visible = false;
            // 
            // displayedSentFile
            // 
            this.displayedSentFile.AutoSize = true;
            this.displayedSentFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.displayedSentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayedSentFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.displayedSentFile.Location = new System.Drawing.Point(159, 178);
            this.displayedSentFile.Name = "displayedSentFile";
            this.displayedSentFile.Size = new System.Drawing.Size(47, 18);
            this.displayedSentFile.TabIndex = 16;
            this.displayedSentFile.Text = "label1";
            this.displayedSentFile.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(744, 554);
            this.Controls.Add(this.displayedSentFile);
            this.Controls.Add(this.fileSentLabel);
            this.Controls.Add(this.displayedSubject);
            this.Controls.Add(this.subLabel);
            this.Controls.Add(this.fromValue);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.toValue);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.filesAttachedListview);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.addAttachmentButton);
            this.Controls.Add(this.hasAttachments);
            this.Controls.Add(this.subjectLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Email Blaster";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label subjectLabel;
        internal System.Windows.Forms.CheckBox hasAttachments;
        private System.Windows.Forms.Button addAttachmentButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ListView filesAttachedListview;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Label toValue;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.Label fromValue;
        private System.Windows.Forms.Label subLabel;
        private System.Windows.Forms.Label displayedSubject;
        private System.Windows.Forms.Label fileSentLabel;
        private System.Windows.Forms.Label displayedSentFile;
                
    }
}

