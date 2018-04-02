using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace KodeUI
{
    public class GameDatabase : LoadingSystem
    {
        private string GameDataPath;
        private string resourcePath;
        private List<ConfigNode> configNodes;
        private bool ready = false;

        public static GameDatabase Instance;

        private void Awake()
        {
            GameDataPath = Application.dataPath + "/../GameData/";
            resourcePath = GameDataPath + "KodeUI/UI/";
            configNodes = new List<ConfigNode>();

            Instance = this;
        }

        public override bool IsReady()
        {
            return ready;
        }

        public override void StartLoad()
        {
            var cfgs  = Directory.GetFiles(resourcePath);

            foreach (string cfg in cfgs)
            {
                if (cfg.ToLower().EndsWith(".cfg"))
                {
                    string text = File.ReadAllText (cfg);
                    ConfigNode node = ConfigNode.Parse(text);
                    configNodes.AddRange(node.nodes);
                    Debug.Log("Loaded " +  node.nodes.Count + " nodes from " + Path.GetFileName(cfg));
                }
            }
            ready = true;
        }

        public ConfigNode[] GetConfigNodes(string typeName)
        {
            List<ConfigNode> list = new List<ConfigNode>();

            foreach (ConfigNode node in configNodes)
            {
                if (node.name  == typeName)
                    list.Add(node);
            }

            return list.ToArray();
        }

        public Texture2D GetTexture(string filename, bool isNormalMap)
        {
            var filePath = GameDataPath + filename  + ".png";
            
            Texture2D tex = new Texture2D(2, 2);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning("Missing texture file " + filePath);
                return tex;
            }

            var fileData = File.ReadAllBytes(filePath);
            tex.LoadImage(fileData);
            return tex;
        }

    }
}