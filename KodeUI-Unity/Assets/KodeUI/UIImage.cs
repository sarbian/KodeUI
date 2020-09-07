using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KodeUI
{
    class UIImage : UIObject
    {
		public Image image { get; private set; }

        public override void CreateUI()
        {
            Pivot(PivotPresets.TopLeft);
			image = gameObject.AddComponent<Image>();
        }

        public override void Style()
        {
        }

        public UIImage Image(Sprite sprite)
        {
			image.sprite = sprite;
            return this;
        }
    }
}
