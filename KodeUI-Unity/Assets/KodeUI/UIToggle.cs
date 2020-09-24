using UnityEngine;
using UnityEngine.UI;

namespace KodeUI
{
    public class UIToggle : UIToggleBase
    {
        private LayoutPanel checkMark;
        private Image image;

        public override void CreateUI()
        {
            base.CreateUI();
            
            image = gameObject.AddComponent<Image>();
            toggle.targetGraphic = image;

            Add<LayoutPanel>(out checkMark, "CheckMark").Anchor(AnchorPresets.StretchAll).Pivot(PivotPresets.MiddleCenter).Finish();
            toggle.graphic = checkMark.BackGround;

            // TODO This should be handled in a parent class with some logic related to Stretch axis. In Finish ? Might need something similar with anchoredPosition
            checkMark.rectTransform.sizeDelta = Vector2.zero;
        }

        public override void Style()
        {
            base.Style();
            image.sprite = style.standard;
            image.color = style.color ?? UnityEngine.Color.white;

            checkMark.BackGround.sprite = style.checkmark;
            checkMark.BackGround.color = style.color ?? UnityEngine.Color.white;;

            toggle.colors = style.stateColors ?? ColorBlock.defaultColorBlock;;
        }
    }
}
