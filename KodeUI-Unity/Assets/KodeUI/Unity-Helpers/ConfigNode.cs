using System;
using System.Collections.Generic;

using UnityEngine;

namespace KodeUI
{
	public class ConfigNodeErrorException: Exception
	{
		public ConfigNodeErrorException()
		{
		}

		public ConfigNodeErrorException(string message)
			: base(message)
		{
		}

		public ConfigNodeErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

	public class ConfigNode
	{
		public class Value
		{
			public string name;
			public string value;
			public int line;

			public Value (string name, string value, int line)
			{
				this.name = name;
				this.value = value;
				this.line = line;
			}
		}

		public string name;
		public int line;
		public List<Value> values;
		public List<ConfigNode> nodes;

		public ConfigNode ()
		{
			this.name = string.Empty;
			values = new List<Value> ();
			nodes = new List<ConfigNode> ();
		}

		public ConfigNode (string name)
		{
			this.name = name;
			values = new List<Value> ();
			nodes = new List<ConfigNode> ();
		}

		public bool HasValue (string name)
		{
			for (int i = 0; i < values.Count; i++) {
				if (values[i].name == name) {
					return true;
				}
			}
			return false;
		}

		public string GetValue (string name)
		{
			for (int i = 0; i < values.Count; i++) {
				if (values[i].name == name) {
					return values[i].value;
				}
			}
			return null;
		}

		public string[] GetValues (string name)
		{
			List<string> valuelist = new List<string> ();
			for (int i = 0; i < values.Count; i++) {
				if (values[i].name == name) {
					valuelist.Add (values[i].value);
				}
			}
			return valuelist.ToArray ();
		}

		public bool HasNode (string name)
		{
			for (int i = 0; i < nodes.Count; i++) {
				if (nodes[i].name == name) {
					return true;
				}
			}
			return false;
		}

		public ConfigNode GetNode (string name)
		{
			for (int i = 0; i < nodes.Count; i++) {
				if (nodes[i].name == name) {
					return nodes[i];
				}
			}
			return null;
		}

		public ConfigNode[] GetNodes (string name)
		{
			List<ConfigNode> nodelist = new List<ConfigNode> ();
			for (int i = 0; i < nodes.Count; i++) {
				if (nodes[i].name == name) {
					nodelist.Add (nodes[i]);
				}
			}
			return nodelist.ToArray ();
		}

		static readonly char []comma = {','};
		static string []ParseArray (string text)
		{
			string []array = text.Split(comma, StringSplitOptions.RemoveEmptyEntries);
			for (int i = array.Length; i-- > 0; ) {
				array[i] = array[i].Trim();
			}
			return array;
		}

		static float []ParseFloatArray (string text)
		{
			string []elements = ParseArray(text);
			float []values = new float[elements.Length]; 
			for (int i = elements.Length; i-- > 0; ) {
				if (!float.TryParse (elements[i], out values[i])) {
					return null;
				}
			}
			return values;
		}

		public bool TryGetValue (string name, ref string value)
		{
			string str = GetValue (name);
			if (str != null) {
				value = str;
				return true;
			}
			return false;
		}

		public bool TryGetValue (string name, ref uint value)
		{
			string str = GetValue (name);
			uint val;

			if (str != null && uint.TryParse (str, out val)) {
				value = val;
				return true;
			}
			return false;
		}

		public bool TryGetValue (string name, ref float value)
		{
			string str = GetValue (name);
			float val;

			if (str != null && float.TryParse (str, out val)) {
				value = val;
				return true;
			}
			return false;
		}

		public bool TryGetValue (string name, ref Vector2 value)
		{
			string str = GetValue (name);
			if (str != null) {
				float [] vals = ParseFloatArray (str);
				if (vals.Length == 2) {
					value = new Vector2 (vals[0], vals[1]);
					return true;
				}
				value = Vector2.zero;
			}
			return false;
		}

		public bool TryGetValue (string name, ref Vector4 value)
		{
			string str = GetValue (name);
			if (str != null) {
				float [] vals = ParseFloatArray (str);
				if (vals.Length == 4) {
					value = new Vector4 (vals[0], vals[1], vals[2], vals[3]);
					return true;
				}
				value = Vector4.zero;
			}
			return false;
		}

		public bool TryGetValue (string name, ref Rect value)
		{
			string str = GetValue (name);
			if (str != null) {
				float [] vals = ParseFloatArray (str);
				if (vals.Length == 4) {
					value = new Rect (vals[0], vals[1], vals[2], vals[3]);
					return true;
				}
				value = new Rect ();
			}
			return false;
		}

		static void cfg_error (Script script, string msg)
		{
			msg = String.Format ("{0}:{1}:{2}", script.Filename, script.Line,
								 msg);
			throw new ConfigNodeErrorException (msg);
		}

		static void ParseNode (ConfigNode node, Script script, bool top = false)
		{
			while (script.TokenAvailable (true)) {
				int token_start = script.Pos;
				if (!script.NextToken (true)) {
					break;
				}
				string token = script.Token;
				if (token == "{" || token == "=" || (top && token == "}")) {
					cfg_error (script, "unexpected " + token);
				}
				if (token == "}") {
					return;
				}
				int token_end = script.Pos;
				while (script.TokenAvailable (true)) {
					script.NextToken (true);
					int line = script.Line;
					token = script.Token;
					if (token == "=") {
						string value = string.Empty;
						string key = script.Text (token_start, token_end);
						if (script.TokenAvailable (false)) {
							script.GetLine ();
							value = script.Token;
						}
						node.values.Add (new Value (key, value, line));
						break;
					} else if (token == "{") {
						string key = script.Text (token_start, token_end);
						ConfigNode new_node = new ConfigNode (key);
						new_node.line = line;
						ParseNode (new_node, script);
                        node.nodes.Add(new_node);
						break;
					} else {
						token_end = script.Pos;
					}
				}
			}
			if (!top) {
				cfg_error (script, "unexpected end of file");
			}
		}

		public static ConfigNode Parse (string s)
		{
			Script script = new Script ("<string>", s, "{}=", false);
			ConfigNode node = new ConfigNode ();
			ParseNode (node, script, true);
			return node;
		}
	}
}
