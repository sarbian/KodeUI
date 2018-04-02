using System;

namespace KodeUI
{
	public class ScriptErrorException: Exception
	{
		public ScriptErrorException()
		{
		}

		public ScriptErrorException(string message)
			: base(message)
		{
		}

		public ScriptErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

	public class Script
	{
		string filename;
		string text;
		string single;
		bool quotes;
		int pos = 0;
		int line = 1;
		bool unget = false;
		int unget_start = 0;
		int start = 0;
		int end = 0;

		public Script (string filename, string text, string single = "{}()':",
					   bool quotes = true)
		{
			this.filename = filename;
			this.text = text;
			this.single = single;
			this.quotes = quotes;
		}

		void error (string msg)
		{
			msg = String.Format("{0}:{1}:{2}", filename, line, msg);
			throw new ScriptErrorException (msg);
		}

		bool isComment ()
		{
			return (pos < text.Length - 1
					&& text[pos] == '/' && text[pos + 1] == '/');
		}

		public int Pos { get { return pos; } }
		public int Line { get { return line; } }
		public string Filename { get { return filename; } }

		public string Text (int start, int end)
		{
			return text.Substring (start, end - start);
		}

		public string Token
		{
			get {
				return text.Substring (start, end - start);
			}
		}

		public bool TokenAvailable (bool crossline = false)
		{
			if (unget) {
				return true;
			}

			while (pos < text.Length) {
				while (pos < text.Length && Char.IsWhiteSpace (text[pos])) {
					if (text[pos] == Environment.NewLine[0]) {
						if (!crossline) {
							return false;
						}
						line += 1;
					}
					pos += 1;
				}
				if (pos == text.Length) {
					return false;
				}
				if (text[pos] == '\x1a' || text[pos] == '\x04') {
					// end of file characters
					pos += 1;
					continue;
				}
				if (isComment ()) {
					while (pos < text.Length && text[pos] != Environment.NewLine[0]) {
						pos += 1;
					}
					if (pos == text.Length) {
						return false;
					}
					if (!crossline) {
						return false;
					}
					continue;
				}
				return true;
			}
			return false;
		}

		public bool GetLine ()
		{
			if (pos == text.Length) {
				return false;
			}
			start = pos;
			end = start;
			while (pos < text.Length) {
				if (text[pos] == Environment.NewLine[0]) {
					line += 1;
					pos += 1;
					break;
				}
				if (isComment ()) {
					break;
				}
				pos += 1;
				end = pos;
			}
			if (unget) {
				start = unget_start;
				unget = false;
			}
			return true;
		}

		public bool NextToken (bool crossline = false)
		{
			if (unget) {
				unget = false;
				return true;
			}
			if (!TokenAvailable (crossline)) {
				if (!crossline) {
					error ("line is incomplete");
				}
				return false;
			}
			if (quotes && text[pos] == '\"') {
				pos += 1;
				start = pos;
				if (pos == text.Length) {
					error ("EOF inside quoted string");
				}
				while (text[pos] != '\"') {
					if (text[pos] == Environment.NewLine[0]) {
						line += 1;
					}
					pos += 1;
					if (pos == text.Length) {
						error ("EOF inside quoted string");
					}
				}
				end = pos;
				pos += 1;
			} else {
				start = pos;
				if (single.IndexOf (text[pos]) >= 0) {
					pos += 1;
				} else {
					while (pos < text.Length
						   && !Char.IsWhiteSpace (text[pos])
						   && single.IndexOf (text[pos]) < 0) {
						pos += 1;
					}
				}
				end = pos;
			}
			return true;
		}

		public void UngetToken ()
		{
			unget = true;
			unget_start = start;
		}
	}
}
