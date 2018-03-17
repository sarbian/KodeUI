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

            ColorBlock colors = toggle.colors;
            colors.highlightedColor = new Color(0.882f, 0f, 0.882f);
            colors.pressedColor     = new Color(0.0f, 0.8f, 0.1f);
            colors.disabledColor    = new Color(0f, 0f, 0f);
            colors.normalColor      = Color.white;
            toggle.colors = colors;

            LayoutPanel checkMark;
            Add<LayoutPanel>(out checkMark).Background(spriteOn).Anchor(AnchorPresets.StretchAll).Pivot(PivotPresets.MiddleCenter).Scale(0.625f).Finish();

            toggle.graphic = checkMark.BackGround;
        }

        public override void Style()
        {
            base.Style();
        }
    }
}