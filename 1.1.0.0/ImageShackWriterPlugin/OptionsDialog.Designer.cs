namespace ImageShackWriterPlugin
{
	partial class OptionsDialog
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
			this.registrationLink = new System.Windows.Forms.LinkLabel();
			this.registrationCode = new System.Windows.Forms.TextBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.imageShackLink = new System.Windows.Forms.LinkLabel();
			this.versionLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// registrationLink
			// 
			this.registrationLink.LinkArea = new System.Windows.Forms.LinkArea(0, 37);
			this.registrationLink.Location = new System.Drawing.Point(12, 9);
			this.registrationLink.Name = "registrationLink";
			this.registrationLink.Size = new System.Drawing.Size(268, 31);
			this.registrationLink.TabIndex = 0;
			this.registrationLink.TabStop = true;
			this.registrationLink.Text = "Get your ImageShack registration code and enter it here:";
			this.registrationLink.UseCompatibleTextRendering = true;
			this.registrationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registrationLink_LinkClicked);
			// 
			// registrationCode
			// 
			this.registrationCode.Location = new System.Drawing.Point(15, 43);
			this.registrationCode.Name = "registrationCode";
			this.registrationCode.Size = new System.Drawing.Size(265, 20);
			this.registrationCode.TabIndex = 5;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(205, 69);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(124, 69);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 7;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// imageShackLink
			// 
			this.imageShackLink.AutoSize = true;
			this.imageShackLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.imageShackLink.Location = new System.Drawing.Point(12, 108);
			this.imageShackLink.Name = "imageShackLink";
			this.imageShackLink.Size = new System.Drawing.Size(173, 13);
			this.imageShackLink.TabIndex = 8;
			this.imageShackLink.TabStop = true;
			this.imageShackLink.Text = "Free image hosting by ImageShack";
			this.imageShackLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.imageShackLink_LinkClicked);
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(12, 95);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(45, 13);
			this.versionLabel.TabIndex = 9;
			this.versionLabel.Text = "Version:";
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(292, 127);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.imageShackLink);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.registrationCode);
			this.Controls.Add(this.registrationLink);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.Text = "ImageShack Uploader Options";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel registrationLink;
		private System.Windows.Forms.TextBox registrationCode;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.LinkLabel imageShackLink;
		private System.Windows.Forms.Label versionLabel;
	}
}