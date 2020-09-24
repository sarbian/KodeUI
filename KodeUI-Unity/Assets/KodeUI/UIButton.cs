using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KodeUI
{
    public class UIButton : Layout
    {
        private UIText childText;
        private UIImage childImage;
        private Button button;
        private Image image;

        public override void CreateUI()
        {
            Horizontal().ChildForceExpand(false, false).ControlChildSize(true, true).Pivot(PivotPresets.TopLeft);

            // TODO UIButton (and UIToggle) seems to share the base of a UIPanel ? Use that as parent ?

            image = gameObject.AddComponent<Image>();
            
            image.color = UnityEngine.Color.white;

            button = gameObject.AddComponent<Button>();
            button.targetGraphic = image;
        }

        public override void Style()
        {
            ImageLoader.SetupImage(image,"KodeUI/Default/button_on");
            Padding(3);

            button.colors = style.stateColors ?? ColorBlock.defaultColorBlock;
            Debug.Log($"[UIButton] Style sc:{style.stateColors} c:{style.color} tc:{style.textColor} ic:{style.imageColor} s:{style.standard} b:{style.background} i:{style.inputField} k:{style.knob} c:{style.checkmark} d:{style.dropdown} m:{style.mask}");
            image.sprite = style.standard;
            image.color = style.color ?? UnityEngine.Color.white;
        }
        
        public UIButton Text(string text)
        {
            if (childText == null) {
                Add<UIText>(out childText, "ButtonText").Text(text).Alignment(TextAlignmentOptions.Center).Anchor(AnchorPresets.StretchAll).Width(0).Height(0).Finish();
            } else {
                childText.Text(text);
            }
            return this;
        }

        public UIButton Image(Sprite sprite)
        {
            if (childImage == null) {
                Add<UIImage>(out childImage, "ButtomImage").Image(sprite).Anchor(AnchorPresets.StretchAll).Width(0).Height(0).Finish();
            } else {
                childImage.Image(sprite);
            }
            return this;
        }

        public UIButton OnClick(UnityAction action)
        {
            button.onClick.AddListener(action);
            return this;
        }
    }
}
