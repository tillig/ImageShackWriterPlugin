using System;
using System.CodeDom.Compiler;
using System.Drawing.Imaging;
using System.IO;
using ImageShackWriterPlugin;
using NUnit.Framework;

namespace ImageShackWriterPluginTests
{
	[TestFixture]
	public class FileInfoExtensionsFixture
	{
		private TempFileCollection _tempFiles;
		private string _tempImagePath;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			this._tempFiles = new TempFileCollection();
			this._tempImagePath = this._tempFiles.AddExtension("png", false);
			Properties.Resources.GiftIcon.Save(this._tempImagePath, ImageFormat.Png);
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			this._tempFiles.Delete();
		}

		[Test(Description = "Verifies that the header information is properly written.")]
		public void WriteMultipartFormData_Headers()
		{
			FileInfo file = new FileInfo(this._tempImagePath);
			using (MemoryStream memStream = new MemoryStream())
			{
				string boundary = "---boundary";
				string mimeType = "image/png";
				string formKey = "fieldname";
				file.WriteMultipartFormData(memStream, boundary, mimeType, formKey);

				memStream.Position = 0;
				using (StreamReader memStreamReader = new StreamReader(memStream))
				{
					string writtenBoundary = memStreamReader.ReadLine();
					Assert.AreEqual("--" + boundary, writtenBoundary, "The boundary line was not written correctly.");

					string writtenContentInfo = memStreamReader.ReadLine();
					string expectedContentInfo = String.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"", formKey, file.Name);
					Assert.AreEqual(expectedContentInfo, writtenContentInfo, "The Content-Disposition header was not written correctly.");

					string writtenMimeType = memStreamReader.ReadLine();
					string expectedMimeType = "Content-Type: " + mimeType;
					Assert.AreEqual(expectedMimeType, writtenMimeType, "The Content-Type header was not written correctly.");

					Assert.AreEqual("", memStreamReader.ReadLine(), "There should be a blank line after the Content-Type header.");
				}
			}
		}

		[Test(Description = "Verifies that the file information is properly written.")]
		public void WriteMultipartFormData_FileContent()
		{
			FileInfo file = new FileInfo(this._tempImagePath);
			using (MemoryStream memStream = new MemoryStream())
			{
				string boundary = "---boundary";
				string mimeType = "image/png";
				string formKey = "fieldname";
				file.WriteMultipartFormData(memStream, boundary, mimeType, formKey);

				memStream.Position = 0;

				// Seek to the two newlines at the end of the header.
				byte cr = (byte)'\r';
				byte lf = (byte)'\n';
				bool foundNewline = false;
				while (memStream.Position < memStream.Length)
				{
					int read = memStream.ReadByte();
					if (read == cr && memStream.Position < memStream.Length - 1)
					{
						read = memStream.ReadByte();
						if (read == lf)
						{
							if (foundNewline)
							{
								break;
							}
							foundNewline = true;
						}
					}
					else
					{
						foundNewline = false;
					}
				}

				// Compare, byte-for-byte, the content written to the stream against
				// the original file.
				using (FileStream fileStream = file.OpenRead())
				{
					// When comparing against the file, ignore (in the memory stream)
					// the multipart header and the trailing \r\n.
					Assert.AreEqual(fileStream.Length, memStream.Length - memStream.Position - 2, "The file and memory streams aren't the same length.");
					for (int i = 0; i < fileStream.Length; i++)
					{
						int memByte = memStream.ReadByte();
						int fileByte = fileStream.ReadByte();
						Assert.AreEqual(fileByte, memByte, "The file and memory bytes did not match at byte " + (i + 1).ToString());

					}
				}

				Assert.AreEqual(2, memStream.Length - memStream.Position, "After the file should be a newline.");
				Assert.AreEqual(13, memStream.ReadByte(), "Missing the \\r.");
				Assert.AreEqual(10, memStream.ReadByte(), "Missing the \\n.");
			}
		}
	}
}
