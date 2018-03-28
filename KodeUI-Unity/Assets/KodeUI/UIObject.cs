using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KodeUI
{
    public abstract class  UIObject : UIBehaviour {

        enum LayoutPosition
        {
            NotSet,
            Default,
            TopLevelLayout,
            ChildOfLayout
        }

        public string id;

        private RectTransform _rectTransform;
        public RectTransform rectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        private LayoutElement _layoutElement;
        public LayoutElement LayoutElement
        {
            get
            {
                if (_layoutElement == null)
                    _layoutElement = GetComponent<LayoutElement>();
                return _layoutElement;
            }
        }

        private ContentSizeFitter _contentSizeFitter;
        public ContentSizeFitter ContentSizeFitter
        {
            get
            {
                if (_contentSizeFitter == null)
                    _contentSizeFitter = GetComponent<ContentSizeFitter>();
                return _contentSizeFitter;
            }
        }
        
        private LayoutPosition _layoutPosition = LayoutPosition.NotSet;
        private LayoutPosition layoutPosition
        {
            get
            {
                if (_layoutPosition == LayoutPosition.NotSet)
                {
                    UIObject parent = rectTransform.parent.GetComponent<UIObject>();
                    if (parent == null)
                    {
                        _layoutPosition = LayoutPosition.Default;
                    }
                    else if (parent.GetComponent<HorizontalOrVerticalLayoutGroup>() != null)
                    {
                        _layoutPosition = LayoutPosition.ChildOfLayout;
                    }
                    else if (GetComponent<HorizontalOrVerticalLayoutGroup>() != null || GetComponent<GridLayoutGroup>() != null)
                    {
                        _layoutPosition = LayoutPosition.TopLevelLayout;
                    }
                    else
                    {
                        _layoutPosition = LayoutPosition.Default;
                    }
                }
                return _layoutPosition;
            }
        }

        private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = gameObject.GetComponent<CanvasGroup>();
                    if (_canvasGroup == null)
                    {
                        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    }
                }
                return _canvasGroup;
            }
        }

        public bool CanvasGroupExist()
        {
            return _canvasGroup != null;
        }
    
        public abstract void CreateUI();

        public abstract void Style();

        public T Add<T>(string id = null) where T : UIObject
        {
            if (id == null)
                id = typeof(T).Name;
            T child = UIKit.CreateUI<T>(rectTransform, id);
            return child;
        }

        public T Add<T>(out T child, string id = null) where T : UIObject
        {
            if (id == null)
                id = typeof(T).Name;
            child = UIKit.CreateUI<T>(rectTransform, id);
            return child;
        }

        public UIObject Finish()
        {
            Style();

            if (!rectTransform.parent)
                return null;

            UIObject parent = rectTransform.parent.GetComponent<UIObject>();
            return parent != null ? parent : this;
        }
        
        public UIObject X(float x)
        {
            rectTransform.anchoredPosition3D = rectTransform.anchoredPosition3D.SetX(x);
            return this;
        }
        
        public UIObject Y(float y)
        {
            rectTransform.anchoredPosition3D = rectTransform.anchoredPosition3D.SetY(y);
            return this;
        }

        public UIObject Z(float z)
        {
            rectTransform.anchoredPosition3D = rectTransform.anchoredPosition3D.SetZ(z);
            return this;
        }

        /// <summary>
        /// Must be set prior to width/height
        /// </summary>
        /// <param name="anchor"></param>
        /// <returns></returns>
        public UIObject Anchor(AnchorPresets anchor)
        {
            rectTransform.SetAnchor(anchor);
            return this;
        }

        public UIObject Anchor(Vector2 min, Vector2 max)
        {
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
            return this;
        }

        public UIObject Pivot(PivotPresets pivot)
        {
            rectTransform.SetPivot(pivot);
            return this;
        }

        public UIObject Pivot(Vector2 pivot)
        {
            rectTransform.pivot = pivot;
            return this;
        }

        public UIObject Width(float w)
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta.SetX(w);
            return this;
        }

        public UIObject Height(float h)
        {
            rectTransform.sizeDelta = rectTransform.sizeDelta.SetY(h);
            return this;
        }

        public UIObject Scale(float s)
        {
            rectTransform.localScale = new Vector3(s, s, 1);
            return this;
        }

        public UIObject ScaleW(float w)
        {
            rectTransform.localScale.SetX(w);
            return this;
        }

        public UIObject ScaleH(float h)
        {
            rectTransform.localScale.SetY(h);
            return this;
        }

        public UIObject PreferredSizeFitter(bool v, bool h)
        {
            if (!ContentSizeFitter)
                gameObject.AddComponent<ContentSizeFitter>();
            ContentSizeFitter.horizontalFit = h ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
            ContentSizeFitter.verticalFit = v ? ContentSizeFitter.FitMode.PreferredSize : ContentSizeFitter.FitMode.Unconstrained;
            return this;
        }

        public UIObject FlexibleLayout(bool w, bool h)
        {
            if (!LayoutElement)
                gameObject.AddComponent<LayoutElement>();
            LayoutElement.flexibleHeight = h ? 1 : -1;
            LayoutElement.flexibleWidth = w ? 1 : -1;
            return this;
        }

        public UIObject PreferredWidth(float w)
        {
            if (!LayoutElement)
                gameObject.AddComponent<LayoutElement>();
            LayoutElement.preferredWidth = w;
            return this;
        }
        public UIObject PreferredHeight(float h)
        {
            if (!LayoutElement)
                gameObject.AddComponent<LayoutElement>();
            LayoutElement.preferredHeight = h;
            return this;
        }
        
        public UIObject BlocksRaycasts(bool state)
        {
            CanvasGroup.blocksRaycasts = state;
            return this;
        }
    }
}
