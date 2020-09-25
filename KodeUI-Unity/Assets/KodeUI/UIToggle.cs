using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KodeUI
{
    public class UIToggle : UIObject
    {
        protected UIImage checkMark;
        protected Image image;
        protected Toggle toggle;

        public bool isOn
        {
            get { return toggle.isOn; }
        }

        public override void CreateUI()
        {
            Pivot(PivotPresets.MiddleCenter);
            toggle = gameObject.AddComponent<Toggle>();

            image = gameObject.AddComponent<Image>();
            image.type = Image.Type.Sliced;
            toggle.targetGraphic = image;

            Add<UIImage>(out checkMark, "CheckMark").Anchor(AnchorPresets.StretchAll).Pivot(PivotPresets.MiddleCenter).SizeDelta(0, 0).Finish();
            toggle.graphic = checkMark.image;
        }

        public UIToggle OnClick(UnityAction<bool> action)
        {
            toggle.onValueChanged.AddListener(action);
            return this;
        }

        public override void Style()
        {
            image.sprite = style.standard;
            image.color = style.color ?? UnityEngine.Color.white;

            checkMark.image.sprite = style.checkmark;
            checkMark.image.color = style.color ?? UnityEngine.Color.white;;

            toggle.colors = style.stateColors ?? ColorBlock.defaultColorBlock;;
        }
    }
}
