﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ImageShackWriterPlugin
{
	/// <summary>
	/// Extension methods for generic dictionaries.
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Converts a <see cref="System.Collections.Generic.Dictionary{K, V}"/>
		/// into a querystring.
		/// </summary>
		/// <param name="dictionary">The dictionary to convert.</param>
		/// <returns>
		/// A <see cref="System.String"/> with the encoded querystring items
		/// or <see langword="null" /> if the dictionary is <see langword="null" />
		/// or empty.
		/// </returns>
		public static string ToQueryString(this Dictionary<string, string> dictionary)
		{
			if (dictionary == null || dictionary.Count == 0)
			{
				return null;
			}
			List<string> pairs = new List<string>();
			foreach (string key in dictionary.Keys)
			{
				string pair = HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(dictionary[key]);
				pairs.Add(pair);
			}
			string queryString = String.Join("&", pairs.ToArray());
			return queryString;
		}

		/// <summary>
		/// Template for a multipart/form-data item.
		/// </summary>
		public const string FormDataTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";

		/// <summary>
		/// Writes a dictionary to a stream as a multipart/form-data set.
		/// </summary>
		/// <param name="dictionary">The dictionary of form values to write to the stream.</param>
		/// <param name="stream">The stream to which the form data should be written.</param>
		/// <param name="mimeBoundary">The MIME multipart form boundary string.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="stream" /> or <paramref name="mimeBoundary" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="mimeBoundary" /> is empty.
		/// </exception>
		/// <remarks>
		/// If <paramref name="dictionary" /> is <see langword="null" /> or empty,
		/// nothing wil be written to the stream.
		/// </remarks>
		public static void WriteMultipartFormData(this Dictionary<string, string> dictionary, Stream stream, string mimeBoundary)
		{
			if (dictionary == null || dictionary.Count == 0)
			{
				return;
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (mimeBoundary == null)
			{
				throw new ArgumentNullException("mimeBoundary");
			}
			if (mimeBoundary.Length == 0)
			{
				throw new ArgumentException("MIME boundary may not be empty.", "mimeBoundary");
			}
			foreach (string key in dictionary.Keys)
			{
				string item = String.Format(FormDataTemplate, mimeBoundary, key, dictionary[key]);
				byte[] itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
				stream.Write(itemBytes, 0, itemBytes.Length);
			}
		}
	}
}
