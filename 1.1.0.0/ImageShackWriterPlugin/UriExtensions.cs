using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageShackWriterPlugin
{
	/// <summary>
	/// Extension methods for <see cref="System.Uri"/>.
	/// </summary>
	public static class UriExtensions
	{
		/// <summary>
		/// Regular expression that matches the host name of the URL.
		/// </summary>
		private const string HostRegex = @"(img[0-9]+)\.imageshack\.us";

		/// <summary>
		/// Converts a standard image link URL into one that works with the ImageShack
		/// load-balanced URL. Avoids broken links when individual servers go down.
		/// </summary>
		/// <param name="originalUri">The original <see cref="System.Uri"/> returned from ImageShack after uploading.</param>
		/// <returns>
		/// An updated URL that can be used to get the raw image in a load-balanced format.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="originalUri" /> is <see langword="null" />.
		/// </exception>
		public static Uri ToLoadBalancedImageUri(this Uri originalUri)
		{
			// Converts an original URL like this...
			// http://img546.imageshack.us/img546/9079/50880488.gif
			// into the imageshack.us direct image like this...
			// http://imageshack.us/a/img546/9079/50880488.gif
			if (originalUri == null)
			{
				throw new ArgumentNullException("originalUri");
			}

			// Verify the host is "img123.imageshack.us" format.
			var match = Regex.Match(originalUri.Host, HostRegex);
			if (!match.Success)
			{
				return originalUri;
			}

			// Verify the host number ("img123") matches the path that follows ("/img123/")
			// just in case they switch up the scheme on us.
			var hostNumber = match.Groups[1].Captures[0].Value;
			if (!originalUri.AbsolutePath.StartsWith("/" + hostNumber + "/"))
			{
				return originalUri;
			}

			// Build up the URI by switching the host and adding that /a/ part to the path.
			var newUri = new UriBuilder(originalUri);
			newUri.Host = "imageshack.us";
			newUri.Path = "/a" + originalUri.AbsolutePath;
			return newUri.Uri;
		}
	}
}
