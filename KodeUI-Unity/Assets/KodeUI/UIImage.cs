using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KodeUI
{
    public class UIImage : UIObject
    {
        public Image image { get; private set; }

        public override void CreateUI()
        {
            Pivot(PivotPresets.TopLeft);
            image = gameObject.AddComponent<Image>();
        }

        public override void Style()
        {
            image.color = style.imageColor ?? UnityEngine.Color.white;
        }

        public UIImage Image(Sprite sprite)
        {
            image.sprite = sprite;
            return this;
        }

        public UIImage Type(Image.Type type)
        {
            image.type = type;
            return this;
        }
    }
}
