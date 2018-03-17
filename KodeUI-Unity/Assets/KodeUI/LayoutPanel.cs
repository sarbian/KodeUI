using UnityEngine;
using UnityEngine.UI;

namespace KodeUI
{
    class LayoutPanel : Layout
    {
        // TODO split into an empty panel and one with a background
        private Image backGround;

        public Image BackGround
        {
            get { return backGround; }
        }

        public override void CreateUI()
        {
            base.CreateUI();

            Anchor(AnchorPresets.StretchAll).Pivot(PivotPresets.TopLeft);
            
            rectTransform.anchoredPosition = Vector2.zero;
            
            backGround = gameObject.AddComponent<Image>();
            backGround.sprite = Resources.Load("DefaultSkin/window", typeof(Sprite)) as Sprite;

            if (backGround.sprite == null)
                Debug.Log("Can not find the Sprite");

            backGround.type = Image.Type.Sliced;
            backGround.color = Color.white;
        }

        public override void Style()
        {
            base.Style();

            if (!LayoutElement)
                return;

            Padding(4);
        }

        public LayoutPanel Background(Sprite sprite)
        {
            backGround.sprite = sprite;
            return this;
        }
    }
}