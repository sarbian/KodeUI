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

        public override void CreateUI()
        {
            Horizontal().ChildForceExpand(false, false).ControlChildSize(true, true).PreferredSizeFitter(true,true).Pivot(PivotPresets.TopLeft);;

            Image image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load("DefaultSkin/button_on", typeof(Sprite)) as Sprite;

            if (image.sprite == null)
                Debug.Log("Can not find the Sprite");

            image.type = Image.Type.Sliced;
            image.color = Color.white;

            button = gameObject.AddComponent<Button>();
            button.targetGraphic = image;

            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color(0.882f, 0f, 0.882f);
            colors.pressedColor     = new Color(0.0f, 0.8f, 0.1f);
            colors.disabledColor    = new Color(0f, 0f, 0f);
            colors.normalColor      = Color.white;
            button.colors = colors;
            
            Add<UIText>(out childText).Text("Button").Alignment(TextAlignmentOptions.Center).Anchor(AnchorPresets.StretchAll).Width(0).Height(0).PreferredSizeFitter(true, true).Finish();
        }

        public override void Style()
        {
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