using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using WindowsLive.Writer.Api;

namespace ImageShackWriterPlugin
{
	/// <summary>
	/// Uploads images to ImageShack for hosting.
	/// </summary>
	public class Uploader
	{
		/// <summary>
		/// Developer key for accessing the upload API.
		/// </summary>
		private const string DeveloperKey = "3ABDHMQTc8ca13880bf77f082ab9841ff64ebdb5";

		/// <summary>
		/// URL to the API call for checking validity of a user's registration code.
		/// </summary>
		private static readonly Uri RegistrationUrl = new Uri("http://my.imageshack.us/setlogin.php");

		/// <summary>
		/// URL to the API call to upload an image.
		/// </summary>
		private static readonly Uri UploadUrl = new Uri("http://www.imageshack.us/upload_api.php");

		/// <summary>
		/// Gets the user's registration code.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> containing the registration code being
		/// used by the uploader to post images.
		/// </value>
		public string RegistrationCode { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Uploader"/> class.
		/// </summary>
		/// <param name="registrationCode">The registration code corresponding to the user posting images.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="registrationCode" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="registrationCode" /> is empty.
		/// </exception>
		public Uploader(string registrationCode)
		{
			if (registrationCode == null)
			{
				throw new ArgumentNullException("registrationCode");
			}
			if (registrationCode.Length == 0)
			{
				throw new ArgumentException("Registration code may not be empty.", "registrationCode");
			}
			this.RegistrationCode = registrationCode;
		}

		/// <summary>
		/// Checks with ImageShack to see if the registration code is valid.
		/// </summary>
		/// <returns>
		/// <see langword="true" /> if ImageShack recognizes the set <see cref="ImageShackWriterPlugin.Uploader.RegistrationCode"/>,
		/// <see langword="false" /> if not.
		/// </returns>
		public bool RegistrationIsValid()
		{
			Dictionary<string, string> postData = new Dictionary<string, string>();
			postData.Add("login", this.RegistrationCode);
			postData.Add("xml", "yes");
			// Getting registration info through GET instead of POST because
			// POST doesn't seem to work - it always returns an empty string.
			// Filed issue with ImageShack: http://code.google.com/p/imageshackapi/issues/detail?id=9
			UriBuilder builder = new UriBuilder(RegistrationUrl);
			builder.Query = postData.ToQueryString();
			XDocument result = this.ExecuteWebRequest(builder.Uri, null, null);
			var exists = from node in result.Element("account_data").Elements() where node.Name == "exists" select node.Value;
			return String.CompareOrdinal("yes", exists.First()) == 0;
		}

		/// <summary>
		/// Uploads an image to ImageShack.
		/// </summary>
		/// <param name="imageFile">The image file to upload.</param>
		/// <returns>
		/// An <see cref="ImageInfo"/> containing data about the uploaded image.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The XML returned from ImageShack on a successful upload looks like this:
		/// </para>
		/// <code>
		/// &lt;?xml version="1.0" encoding="iso-8859-1"?&gt;
		/// &lt;imginfo xmlns="http://ns.imageshack.us/imginfo/7/" version="7" timestamp="1260898720"&gt;
		///   &lt;rating&gt;
		///     &lt;ratings&gt;0&lt;/ratings&gt;
		///     &lt;avg&gt;0.0&lt;/avg&gt;
		///   &lt;/rating&gt;
		///   &lt;files server="191" bucket="9430"&gt;
		///      &lt;image size="6355" content-type="image/png"&gt;gifticon.png&lt;/image&gt;
		///   &lt;/files&gt;
		///   &lt;resolution&gt;
		///     &lt;width&gt;48&lt;/width&gt;
		///     &lt;height&gt;48&lt;/height&gt;
		///   &lt;/resolution&gt;
		///   &lt;class&gt;r&lt;/class&gt;
		///   &lt;visibility&gt;yes&lt;/visibility&gt;
		///   &lt;uploader&gt;
		///     &lt;ip&gt;127.0.0.1&lt;/ip&gt;
		///     &lt;cookie&gt;your_key_here&lt;/cookie&gt;
		///     &lt;username&gt;your_username_here&lt;/username&gt;
		///   &lt;/uploader&gt;
		///   &lt;links&gt;
		///     &lt;image_link&gt;http://img191.imageshack.us/img191/9430/gifticon.png&lt;/image_link&gt;
		///     &lt;image_html&gt;&lt;a href=&quot;http://img191.imageshack.us/my.php?image=gifticon.png&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://img191.imageshack.us/img191/9430/gifticon.png&quot; alt=&quot;Free Image Hosting at www.ImageShack.us&quot; border=&quot;0&quot;/&gt;&lt;/a&gt;&lt;/image_html&gt;
		///     &lt;image_bb&gt;[URL=http://img191.imageshack.us/my.php?image=gifticon.png][IMG]http://img191.imageshack.us/img191/9430/gifticon.png[/IMG][/URL]&lt;/image_bb&gt;
		///     &lt;image_bb2&gt;[url=http://img191.imageshack.us/my.php?image=gifticon.png][img=http://img191.imageshack.us/img191/9430/gifticon.png][/url]&lt;/image_bb2&gt;
		///     &lt;thumb_html&gt;&lt;a href=&quot;http://img191.imageshack.us/my.php?image=gifticon.png&quot; target=&quot;_blank&quot;&gt;&lt;img src=&quot;http://www.imageshack.us/thumbnail.png&quot; alt=&quot;Free Image Hosting at www.ImageShack.us&quot; border=&quot;0&quot;/&gt;&lt;/a&gt;&lt;/thumb_html&gt;
		///     &lt;thumb_bb&gt;[URL=http://img191.imageshack.us/my.php?image=gifticon.png][IMG]http://www.imageshack.us/thumbnail.png[/IMG][/URL]&lt;/thumb_bb&gt;
		///     &lt;thumb_bb2&gt;[url=http://img191.imageshack.us/my.php?image=gifticon.png][img=http://www.imageshack.us/thumbnail.png][/url]&lt;/thumb_bb2&gt;
		///     &lt;yfrog_link&gt;http://yfrog.com/5bgifticonp&lt;/yfrog_link&gt;
		///     &lt;yfrog_thumb&gt;http://yfrog.com/5bgifticonp.th.jpg&lt;/yfrog_thumb&gt;
		///     &lt;ad_link&gt;http://img191.imageshack.us/my.php?image=gifticon.png&lt;/ad_link&gt;
		///     &lt;done_page&gt;http://img191.imageshack.us/content.php?page=done&amp;l=img191/9430/gifticon.png&lt;/done_page&gt;
		///   &lt;/links&gt;
		/// &lt;/imginfo&gt;
		/// </code>
		/// <para>
		/// The <c>image_link</c> is the direct link to the image that would normally
		/// get included in the blog entry; however, ImageShack switched its
		/// load-balancing mechanism so you don't address servers directly like
		/// this anymore. The <see cref="ImageShackWriterPlugin.UriExtensions.ToLoadBalancedImageUri"/>
		/// extension takes care of converting to the new URL format.
		/// </para>
		/// </remarks>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="imageFile" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.IO.FileNotFoundException">
		/// Thrown if <paramref name="imageFile" /> doesn't exist.
		/// </exception>
		/// <exception cref="System.Configuration.ConfigurationErrorsException">
		/// Thrown if the <see cref="ImageShackWriterPlugin.Uploader.RegistrationCode"/>
		/// doesn't correspond to a valid user.
		/// </exception>
		public ImageInfo Upload(FileInfo imageFile)
		{
			if (imageFile == null)
			{
				throw new ArgumentNullException("imageFile");
			}
			if (!imageFile.Exists)
			{
				throw new FileNotFoundException("Unable to find selected image file for upload.", imageFile.FullName);
			}
			if (!this.RegistrationIsValid())
			{
				throw new ConfigurationErrorsException("Your ImageShack user key is invalid. Please visit the options page and verify you entered it correctly.");
			}

			Dictionary<string, string> postData = new Dictionary<string, string>();
			postData.Add("rembar", "yes"); // Remove the info bar from the thumbnail.
			postData.Add("cookie", this.RegistrationCode); // User's account info.
			postData.Add("key", DeveloperKey); // Application developer key.
			XDocument doc = this.ExecuteWebRequest(UploadUrl, postData, imageFile);
			XNamespace ns = doc.Root.GetDefaultNamespace();
			var resolution = doc.Root.Element(ns + "resolution");
			string url = doc.Root.Element(ns + "links").Element(ns + "image_link").Value;
			ImageInfo info = new ImageInfo()
			{
				Url = new Uri(url).ToLoadBalancedImageUri(),
				Width = Convert.ToInt32(resolution.Element(ns + "width").Value),
				Height = Convert.ToInt32(resolution.Element(ns + "height").Value)
			};
			return info;
		}

		/// <summary>
		/// Creates a multipart/form-data boundary.
		/// </summary>
		/// <returns>
		/// A dynamically generated form boundary for use in posting multipart/form-data requests.
		/// </returns>
		private static string CreateFormDataBoundary()
		{
			return "---------------------------" + DateTime.Now.Ticks.ToString("x");
		}

		/// <summary>
		/// Executes a web request against ImageShack and parses the result.
		/// </summary>
		/// <param name="url">The URL to execute against.</param>
		/// <param name="postData">Name/value pairs to send to ImageShack.</param>
		/// <param name="fileToUpload">Info about a file to upload.</param>
		/// <returns>
		/// A parsed XML document with the results.
		/// </returns>
		/// <remarks>
		/// <para>
		/// An error response from ImageShack looks like this:
		/// </para>
		/// <code>
		/// &lt;links&gt;
		///   &lt;error id="partial_file_uploaded"&gt;Only part of the file was uploaded. Please try to upload file later&lt;/error&gt;
		/// &lt;/links&gt;
		/// </code>
		/// <para>
		/// These error responses will be thrown as <see cref="System.InvalidOperationException"/>
		/// where the message is the text from the error.
		/// </para>
		/// </remarks>
		/// <exception cref="System.NotSupportedException">
		/// Thrown if there is no current internet connection.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown if ImageShack returns an error.
		/// </exception>
		protected XDocument ExecuteWebRequest(Uri url, Dictionary<string, string> postData, FileInfo fileToUpload)
		{
			if (!PluginHttpRequest.InternetConnectionAvailable)
			{
				throw new NotSupportedException("You must have an internet connection avialable to upload to ImageShack.");
			}
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
			request.AllowAutoRedirect = true;
			request.Proxy = PluginHttpRequest.GetWriterProxy();
			request.Timeout = 120000; // Increase timeout from default 100s to 120s to account for timeouts.
			if (postData != null || fileToUpload != null)
			{
				request.Method = "POST";
				request.KeepAlive = true;

				// Articles on formatting multipart/form-data requests:
				// http://stackoverflow.com/questions/566462/upload-files-with-httpwebrequest-multipart-form-data
				// http://www.15seconds.com/Issue/001003.htm
				// http://blog.dmbcllc.com/2009/11/10/upload-a-file-via-webrequest-using-csharp/

				string boundary = CreateFormDataBoundary();
				request.ContentType = "multipart/form-data; boundary=" + boundary;
				Stream requestStream = request.GetRequestStream();
				postData.WriteMultipartFormData(requestStream, boundary);
				if (fileToUpload != null)
				{
					string imageMimeType = this.GetImageMimeType(fileToUpload);
					fileToUpload.WriteMultipartFormData(requestStream, boundary, imageMimeType, "fileupload");
				}
				byte[] endBytes = System.Text.Encoding.UTF8.GetBytes("--" + boundary + "--");
				requestStream.Write(endBytes, 0, endBytes.Length);
				requestStream.Close();
			}
			using (WebResponse response = request.GetResponse())
			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				string results = reader.ReadToEnd();
				XDocument doc = XDocument.Parse(results);
				var error = doc.Root.Elements("error").FirstOrDefault();
				if (error != null)
				{
					throw new InvalidOperationException("An error occurred while communicating with ImageShack: " + error.Value);
				}
				return doc;
			};
		}

		/// <summary>
		/// Gets MIME type of an image file based on the currently installed codecs.
		/// </summary>
		/// <param name="imageFile">The image file from which the MIME type should be determined.</param>
		/// <returns>
		/// A <see cref="System.String"/> containing the MIME type of the image
		/// if found; <c>image/unknown</c> if it's an image and the MIME type is
		/// not found; or <c>application/octet-stream</c> if the file can't be read
		/// as an image.
		/// </returns>
		public string GetImageMimeType(FileInfo imageFile)
		{
			if (imageFile == null)
			{
				throw new ArgumentNullException("imageFile");
			}
			if (!imageFile.Exists)
			{
				throw new FileNotFoundException("Unable to find image to determine MIME type.", imageFile.FullName);
			}
			if (String.CompareOrdinal(".pdf", imageFile.Extension) == 0)
			{
				return "application/pdf";
			}
			else if (String.CompareOrdinal(".swf", imageFile.Extension) == 0)
			{
				return "application/x-shockwave-flash";
			}

			try
			{
				Image image = Image.FromFile(imageFile.FullName);
				foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
				{
					if (codec.FormatID == image.RawFormat.Guid)
					{
						return codec.MimeType;
					}
				}
				return "image/unknown";
			}
			catch (OutOfMemoryException)
			{
				// This is what you get if the file selected isn't a supported image type.
				return "application/octet-stream";
			}
		}
	}
}
