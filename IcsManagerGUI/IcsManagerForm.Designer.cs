namespace IcsManagerGUI
{
    partial class IcsManagerForm
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
            this.cbSharedConnection = new System.Windows.Forms.ComboBox();
            this.cbHomeConnection = new System.Windows.Forms.ComboBox();
            this.ButtonApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStopSharing = new System.Windows.Forms.Button();
            this.sharedDetails = new System.Windows.Forms.TextBox();
            this.homeDetails = new System.Windows.Forms.TextBox();
            this.stopOnClose = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbSharedConnection
            // 
            this.cbSharedConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSharedConnection.FormattingEnabled = true;
            this.cbSharedConnection.Location = new System.Drawing.Point(12, 27);
            this.cbSharedConnection.Margin = new System.Windows.Forms.Padding(2);
            this.cbSharedConnection.Name = "cbSharedConnection";
            this.cbSharedConnection.Size = new System.Drawing.Size(279, 21);
            this.cbSharedConnection.TabIndex = 0;
            this.cbSharedConnection.SelectedIndexChanged += new System.EventHandler(this.cbSharedConnection_SelectedIndexChanged);
            // 
            // cbHomeConnection
            // 
            this.cbHomeConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHomeConnection.FormattingEnabled = true;
            this.cbHomeConnection.Location = new System.Drawing.Point(295, 27);
            this.cbHomeConnection.Margin = new System.Windows.Forms.Padding(2);
            this.cbHomeConnection.Name = "cbHomeConnection";
            this.cbHomeConnection.Size = new System.Drawing.Size(279, 21);
            this.cbHomeConnection.TabIndex = 1;
            this.cbHomeConnection.SelectedIndexChanged += new System.EventHandler(this.cbHomeConnection_SelectedIndexChanged);
            // 
            // ButtonApply
            // 
            this.ButtonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonApply.Location = new System.Drawing.Point(10, 192);
            this.ButtonApply.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonApply.Name = "ButtonApply";
            this.ButtonApply.Size = new System.Drawing.Size(132, 24);
            this.ButtonApply.TabIndex = 2;
            this.ButtonApply.Text = "Start sharing";
            this.ButtonApply.UseVisualStyleBackColor = true;
            this.ButtonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Shared connection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Home connection";
            // 
            // buttonStopSharing
            // 
            this.buttonStopSharing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStopSharing.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonStopSharing.Location = new System.Drawing.Point(146, 192);
            this.buttonStopSharing.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStopSharing.Name = "buttonStopSharing";
            this.buttonStopSharing.Size = new System.Drawing.Size(124, 24);
            this.buttonStopSharing.TabIndex = 3;
            this.buttonStopSharing.Text = "Stop sharing";
            this.buttonStopSharing.UseVisualStyleBackColor = true;
            this.buttonStopSharing.Click += new System.EventHandler(this.buttonStopSharing_Click);
            // 
            // sharedDetails
            // 
            this.sharedDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sharedDetails.Location = new System.Drawing.Point(12, 53);
            this.sharedDetails.Multiline = true;
            this.sharedDetails.Name = "sharedDetails";
            this.sharedDetails.ReadOnly = true;
            this.sharedDetails.Size = new System.Drawing.Size(277, 130);
            this.sharedDetails.TabIndex = 5;
            this.sharedDetails.TabStop = false;
            // 
            // homeDetails
            // 
            this.homeDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.homeDetails.Location = new System.Drawing.Point(295, 53);
            this.homeDetails.Multiline = true;
            this.homeDetails.Name = "homeDetails";
            this.homeDetails.ReadOnly = true;
            this.homeDetails.Size = new System.Drawing.Size(277, 130);
            this.homeDetails.TabIndex = 6;
            this.homeDetails.TabStop = false;
            // 
            // stopOnClose
            // 
            this.stopOnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopOnClose.AutoSize = true;
            this.stopOnClose.Checked = true;
            this.stopOnClose.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stopOnClose.Location = new System.Drawing.Point(12, 221);
            this.stopOnClose.Name = "stopOnClose";
            this.stopOnClose.Size = new System.Drawing.Size(128, 17);
            this.stopOnClose.TabIndex = 8;
            this.stopOnClose.Text = "Stop sharing on close";
            this.stopOnClose.UseVisualStyleBackColor = true;
            // 
            // IcsManagerForm
            // 
            this.AcceptButton = this.ButtonApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 254);
            this.Controls.Add(this.stopOnClose);
            this.Controls.Add(this.homeDetails);
            this.Controls.Add(this.sharedDetails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStopSharing);
            this.Controls.Add(this.ButtonApply);
            this.Controls.Add(this.cbHomeConnection);
            this.Controls.Add(this.cbSharedConnection);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "IcsManagerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Sharing Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IcsManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.FormSharingManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSharedConnection;
        private System.Windows.Forms.ComboBox cbHomeConnection;
        private System.Windows.Forms.Button ButtonApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStopSharing;
        private System.Windows.Forms.TextBox sharedDetails;
        private System.Windows.Forms.TextBox homeDetails;
        private System.Windows.Forms.CheckBox stopOnClose;
    }
}

