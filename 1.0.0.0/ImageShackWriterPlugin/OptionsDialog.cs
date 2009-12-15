using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace ImageShackWriterPlugin
{
	/// <summary>
	/// Dialog used for gathering user options.
	/// </summary>
	public partial class OptionsDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OptionsDialog"/> class.
		/// </summary>
		/// <remarks>
		/// Sets the version label in the dialog based on the current assembly properties.
		/// </remarks>
		public OptionsDialog()
		{
			InitializeComponent();
			Assembly current = Assembly.GetExecutingAssembly();
			Version version = current.GetName().Version;
			AssemblyProductAttribute product = (AssemblyProductAttribute)(current.GetCustomAttributes(typeof(AssemblyProductAttribute), true)[0]);
			this.versionLabel.Text = String.Format(CultureInfo.CurrentUICulture, Properties.Resources.Label_Version, product.Product, version);
		}

		/// <summary>
		/// Gets or sets the user's registration code.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> containing the value the user has entered
		/// in the registration code text box.
		/// </value>
		public string RegistrationCode
		{
			get
			{
				return this.registrationCode.Text;
			}
			set
			{
				this.registrationCode.Text = value;
			}
		}

		private void imageShackLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.imageshack.us");
		}

		private void registrationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://profile.imageshack.us/prefs/index.php");
		}
	}
}
