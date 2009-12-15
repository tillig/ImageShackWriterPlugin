using System;

namespace ImageShackWriterPlugin
{
	/// <summary>
	/// Data object containing information about a posted image.
	/// </summary>
	public class ImageInfo
	{
		/// <summary>
		/// Gets or sets the width of the image in pixels.
		/// </summary>
		/// <value>
		/// An <see cref="System.Int32"/> containing the posted image's width
		/// in pixels.
		/// </value>
		public int Width { get; set; }

		/// <summary>
		/// Gets or sets the height of the image in pixels.
		/// </summary>
		/// <value>
		/// An <see cref="System.Int32"/> containing the posted image's height
		/// in pixels.
		/// </value>
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the URL to the image.
		/// </summary>
		/// <value>
		/// A <see cref="System.Uri"/> with the direct URL to the image.
		/// </value>
		public Uri Url { get; set; }
	}
}
