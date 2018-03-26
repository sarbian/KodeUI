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
                    configNodes.Add(node);
                    Debug.Log("Loaded " + cfg);
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
            var filePath = resourcePath + filename  + ".png";

            var fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            return tex;
        }

    }
}