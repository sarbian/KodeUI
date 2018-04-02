using UnityEngine;
using UnityEngine.UI;

namespace KodeUI
{
    class UIToggle : UIToggleBase
    {
        public override void CreateUI()
        {
            base.CreateUI();
            
            Image image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load("DefaultSkin/toggle", typeof(Sprite)) as Sprite;
            
            Sprite spriteOn = Resources.Load("DefaultSkin/toggle_on", typeof(Sprite)) as Sprite;

            if (image.sprite == null || spriteOn == null)
                Debug.Log("Can not find the Sprite");

            toggle.targetGraphic = image;

            ColorBlock colors = toggle.colors;
            colors.highlightedColor = Color.magenta;
            colors.pressedColor     = new Color(0.0f, 0.8f, 0.1f);
            colors.disabledColor    = Color.clear;
            colors.normalColor      = Color.white;
            toggle.colors = colors;

            LayoutPanel checkMark;
            Add<LayoutPanel>(out checkMark, "CheckMark").Background(spriteOn, Image.Type.Sliced).BackgroundColor(Color.white).Anchor(AnchorPresets.StretchAll).Pivot(PivotPresets.MiddleCenter).Scale(0.625f).Finish();

            // TODO This should be handled in a parent class with some logic related to Stretch axis. In Finish ? Might need something similar with anchoredPosition
            checkMark.rectTransform.sizeDelta = Vector2.zero;

            toggle.graphic = checkMark.BackGround;
        }

        public override void Style()
        {
            base.Style();
        }
    }
}