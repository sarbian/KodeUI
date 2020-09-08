using System;
using System.IO;

namespace KodeUI {
	public class test
	{
		static void dump(ConfigNode node)
		{
			foreach (var v in node.values) {
				Console.WriteLine ("{0} = {1}", v.name, v.value);
			}
			foreach (var n in node.nodes) {
				Console.WriteLine ("{0} {1}", n.name, "{");
				dump(n);
				Console.WriteLine ("{0}", "}");
			}
		}
		static void Main ()
		{
			string text = File.ReadAllText ("/home/bill/ksp/src/KodeUI/KodeUI-Unity/GameData/KodeUI/UI/button.cfg");
			ConfigNode node = ConfigNode.Parse(text);
            Console.WriteLine ("{0} {1} {2} {3}", node, node.name, node.values.Count, node.nodes.Count);
			dump(node);
		}
	}
}
