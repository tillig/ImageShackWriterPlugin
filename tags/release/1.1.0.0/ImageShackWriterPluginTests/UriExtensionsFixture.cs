using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageShackWriterPlugin;
using NUnit.Framework;

namespace ImageShackWriterPluginTests
{
	[TestFixture]
	public class UriExtensionsFixture
	{
		[Test(Description = "Converts a ImageShack image URI into a load-balanced URI.")]
		[TestCase("http://img4.imageshack.us/img4/8374/12345678someimage.png", "http://imageshack.us/a/img4/8374/12345678someimage.png")]
		[TestCase("http://img546.imageshack.us/img546/9079/50880488.gif", "http://imageshack.us/a/img546/9079/50880488.gif")]
		[TestCase("https://img546.imageshack.us/img546/9079/50880488.gif", "https://imageshack.us/a/img546/9079/50880488.gif")]
		public void ToLoadBalancedImageUri_ConvertsUrls(string orig, string expected)
		{
			var origUri = new Uri(orig);
			Assert.AreEqual(expected, origUri.ToLoadBalancedImageUri().ToString(), "The URL was not properly converted.");
		}

		[Test(Description = "Attempts to convert a non-compliant ImageShack URIs into a load-balanced URI.")]
		[TestCase("http://not.imageshack.us/invalid/format.gif")]
		[TestCase("http://img4.imageshack.us/img12345/8374/12345678someimage.png")]
		[TestCase("http://imgabc.imageshack.us/imgabc/8374/12345678someimage.png")]
		public void ToLoadBalancedImageUri_NoConversion(string orig)
		{
			var origUri = new Uri(orig);
			Assert.AreEqual(origUri, origUri.ToLoadBalancedImageUri(), "If the URL isn't the expected format, the original should be returned untouched.");
		}

		[Test(Description = "Attempts to convert a null URI into a load-balanced URI.")]
		public void ToLoadBalancedImageUri_NullOriginalUri()
		{
			Uri orig = null;
			Assert.Throws<ArgumentNullException>(() => orig.ToLoadBalancedImageUri());
		}
	}
}
