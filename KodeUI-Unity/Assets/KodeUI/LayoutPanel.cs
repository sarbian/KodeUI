using UnityEngine;
using UnityEngine.UI;

namespace KodeUI
{
    class LayoutPanel : Layout
    {
        public override void CreateUI()
        {
            base.CreateUI();
            
            // Set RectTransform to stretch
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;

            Image image = gameObject.AddComponent<Image>();
            image.sprite = Resources.Load("DefaultSkin/window", typeof(Sprite)) as Sprite;

            if (image.sprite == null)
                Debug.Log("Can not find the Sprite");

            image.type = Image.Type.Sliced;
            image.color = Color.white;
        }

        public override void Style()
        {
            base.Style();
            Padding(4);
        }
    }
}