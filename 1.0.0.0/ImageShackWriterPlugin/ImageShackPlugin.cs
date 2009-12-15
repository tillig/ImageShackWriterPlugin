using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace ImageShackWriterPlugin
{
	/// <summary>
	/// Plugin for Windows Live Writer that uploads images to ImageShack.
	/// </summary>
	[InsertableContentSource("ImageShack Upload")]
	[WriterPlugin("C4BAAC77-16C0-4117-80D5-9AFE122392DA",
		"ImageShack Upload",
		Description = "Uploads an image to ImageShack and inserts the image into a blog entry.",
		ImagePath = "Resources.Icon.png",
		HasEditableOptions = true)]
	public class ImageShackPlugin : ContentSource
	{
		/// <summary>
		/// Key unde which the user's account registration code is stored.
		/// </summary>
		private const string OptionsKeyRegistrationCode = "RegistrationCode";

		/// <summary>
		/// Gets or sets the user's ImageShack registration code.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the user's ImageShack registration
		/// code so images can be posted on their behalf.
		/// </value>
		public string RegistrationCode
		{
			get
			{
				return this.Options.GetString(OptionsKeyRegistrationCode, null);
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					this.Options.Remove(OptionsKeyRegistrationCode);
				}
				this.Options.SetString(OptionsKeyRegistrationCode, value);
			}
		}

		/// <summary>
		/// Starts the image upload process and inserts the image HTML into the entry on success.
		/// </summary>
		/// <param name="dialogOwner">The dialog owner.</param>
		/// <param name="content">The content to insert into the blog entry.</param>
		/// <returns>
		/// <see cref="System.Windows.Forms.DialogResult.OK"/> if the image was
		/// successfully uploaded and the content was generated;
		/// <see cref="System.Windows.Forms.DialogResult.Cancel"/> in all other
		/// non-exception cases.
		/// </returns>
		public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content)
		{
			if (String.IsNullOrEmpty(this.RegistrationCode))
			{
				MessageBox.Show(dialogOwner, Properties.Resources.Dialog_NotConfiguredMessage, Properties.Resources.Dialog_NotConfiguredTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return DialogResult.Cancel;
			}
			OpenFileDialog dialog = this.CreateOpenFileDialog();
			if (DialogResult.OK != dialog.ShowDialog(dialogOwner))
			{
				return DialogResult.Cancel;
			}
			Uploader uploader = new Uploader(this.RegistrationCode);
			ImageInfo imageInfo = uploader.Upload(new FileInfo(dialog.FileName));
			content = String.Format(CultureInfo.InvariantCulture, "<img src=\"{0}\" width=\"{1}\" height=\"{2}\" />", imageInfo.Url.AbsoluteUri, imageInfo.Width, imageInfo.Height);
			return DialogResult.OK;
		}

		/// <summary>
		/// Creates the "open file" dialog.
		/// </summary>
		/// <returns>
		/// An <see cref="System.Windows.Forms.OpenFileDialog"/> with all of
		/// the parameters set to open image files for ImageShack hosting.
		/// </returns>
		public OpenFileDialog CreateOpenFileDialog()
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = Properties.Resources.Dialog_FileSelectTitle;
			dialog.Filter = Properties.Resources.Dialog_FileSelectFilter;
			return dialog;
		}

		/// <summary>
		/// Displays the plugin options dialog.
		/// </summary>
		/// <param name="dialogOwner">The dialog owner.</param>
		public override void EditOptions(IWin32Window dialogOwner)
		{
			OptionsDialog dialog = new OptionsDialog();
			dialog.RegistrationCode = this.RegistrationCode;
			if (DialogResult.OK != dialog.ShowDialog(dialogOwner))
			{
				return;
			}
			this.RegistrationCode = dialog.RegistrationCode;
		}
	}
}
