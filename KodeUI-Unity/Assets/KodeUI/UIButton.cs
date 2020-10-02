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

        public bool interactable
        {
            get { return button.interactable; }
            set { button.interactable = value; }
        }

        public override void CreateUI()
        {
            Horizontal().ChildForceExpand(false, false).ControlChildSize(true, true).Pivot(PivotPresets.TopLeft);

            image = gameObject.AddComponent<Image>();
            image.type = UnityEngine.UI.Image.Type.Sliced;
            image.color = UnityEngine.Color.white;

            button = gameObject.AddComponent<Button>();
            button.targetGraphic = image;
        }

        public override void Style()
        {
            Padding(3);

            button.transition = style.transition ?? Selectable.Transition.ColorTint;
            if (style.stateSprites.HasValue) {
                button.spriteState = style.stateSprites.Value;
            }
            button.colors = style.stateColors ?? ColorBlock.defaultColorBlock;

            image.sprite = style.sprite;
            image.color = style.color ?? UnityEngine.Color.white;
        }
        
        public UIButton Text(string text)
        {
            if (childText == null) {
                Add<UIText>(out childText, "ButtonText").Text(text).Alignment(TextAlignmentOptions.Center).Anchor(AnchorPresets.StretchAll).SizeDelta(0,0).FlexibleLayout(true, true).Finish();
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
