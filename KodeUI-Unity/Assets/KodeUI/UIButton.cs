using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KodeUI
{
    class UIButton : Layout
    {
        private UIText childText;
        private Button button;
        private Image image;

        public override void CreateUI()
        {
            Horizontal().ChildForceExpand(false, false).ControlChildSize(true, true).Pivot(PivotPresets.TopLeft);

            // TODO UIButton (and UIToggle) seems to share the base of a UIPanel ? Use that as parent ?

            image = gameObject.AddComponent<Image>();
            
            image.color = Color.white;

            button = gameObject.AddComponent<Button>();
            button.targetGraphic = image;

            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color(0.882f, 0f, 0.882f);
            colors.pressedColor     = new Color(0.0f, 0.8f, 0.1f);
            colors.disabledColor    = new Color(0f, 0f, 0f);
            colors.normalColor      = Color.white;
            button.colors = colors;
            
            Add<UIText>(out childText, "ButtonText").Text("Button").Alignment(TextAlignmentOptions.Center).Anchor(AnchorPresets.StretchAll).Width(0).Height(0).Finish();
        }

        public override void Style()
        {
            ImageLoader.SetupImage(image,"KodeUI/Default/button_on");
            Padding(3);
        }
        
        public UIButton Text(string text)
        {
            childText.Text(text);
            return this;
        }

        public UIButton OnClick(UnityAction action)
        {
            button.onClick.AddListener(action);
            return this;
        }
    }
}
