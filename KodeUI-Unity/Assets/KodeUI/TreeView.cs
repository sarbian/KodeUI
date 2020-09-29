using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace KodeUI
{

    public class TreeView : ScrollView
    {
        public class TreeViewEvent : UnityEvent<int, bool> { }

        public class TreeItem
        {
            System.Func<object, string> getText;
            System.Func<object, bool> canOpen;
            object obj;
            bool isOpen;
            int level;

            public object Object { get { return obj; } }
            public int Level
            {
                get { return level; }
                set { level = value; }
            }
            public string Text { get { return getText (obj); } }
            public bool CanOpen { get { return canOpen (obj); } }
            public bool IsOpen
            {
                get { return isOpen; }
                set {
                    isOpen = value;
                }
            }

            public TreeItem(object obj, System.Func<object, string> getText, System.Func<object, bool> canOpen, int level)
            {
                this.obj = obj;
                this.getText = getText;
                this.canOpen = canOpen;
                this.level = level;
            }
        }

        public class TreeItemView : UIObject, IPointerClickHandler
        {
            Image background;
            UIToggle toggle;
            UIText text;
            TreeItem treeItem;
            int index;
            TreeViewEvent onStateChanged = new TreeViewEvent();

            public UIText Text { get { return text; } }
            public int Index { get { return index; } }

            public override void CreateUI()
            {
                background = gameObject.AddComponent<Image>();
                background.type = Image.Type.Sliced;

                Add<UIToggle>(out toggle, "ItemToggle")
                    .OnValueChanged(onValueChanged)
                    .Finish()
                .Add<UIText>(out text, "ItemText")
                    .Text("ItemText")
                    .Alignment(TextAlignmentOptions.Left)
                    .Anchor(AnchorPresets.StretchAll)
                    .Pivot(PivotPresets.MiddleLeft)
                    .Finish();
                Anchor(AnchorPresets.HorStretchMiddle);
                Pivot(PivotPresets.MiddleCenter);
                Vector2 size = text.tmpText.GetPreferredValues ();
                SizeDelta(0, size.y);

                toggle
                    .Anchor(AnchorPresets.VertStretchLeft)
                    .Pivot(PivotPresets.MiddleLeft)
                    .SizeDelta(size.y, 0);
            }

            public override void Style()
            {
                background.sprite = style.sprite;
                background.color = style.color ?? UnityEngine.Color.white;
            }

            void onValueChanged (bool open)
            {
                if (treeItem.CanOpen) {
                    treeItem.IsOpen = open;
                    onStateChanged.Invoke(index, open);
                } else {
                    toggle.SetIsOnWithoutNotify(false);
                }
            }

            public TreeItemView Item(TreeItem item, int index)
            {
                this.treeItem = item;
                this.index = index;
                int level = item.Level;
                text.Text (item.Text);
                Vector2 size = text.tmpText.GetPreferredValues ();
                float x = level * size.y + size.y;
                text.SizeDelta(-x, 0).X(x);

                toggle.CheckMark.image.enabled = item.IsOpen;
                toggle.Image.enabled = item.CanOpen;
                toggle.SetIsOnWithoutNotify(item.IsOpen);
                return this;
            }

            public TreeItemView OnStateChanged(UnityAction<int, bool> action)
            {
                onStateChanged.AddListener(action);
                return this;
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (eventData.button == PointerEventData.InputButton.Left) {
                }
            }
        }

        Image background;
        List<TreeItem> items;
        TreeViewEvent onStateChanged = new TreeViewEvent();

        public override void CreateUI()
        {
            base.CreateUI();
            UIScrollbar scrollbar;
            this
                .Horizontal(false)
                .Vertical(true)
                .Horizontal()
                .ChildForceExpand(false,false)
                .Add<UIScrollbar>(out scrollbar, "Scrollbar")
                    .Direction(Scrollbar.Direction.BottomToTop)
                    .Anchor(AnchorPresets.VertStretchRight)
                    .SizeDelta(15, -1)
                    .FlexibleLayout(false, true)
                    .Finish();
            Viewport.FlexibleLayout(true, true);
            VerticalScrollbar = scrollbar;
            Content.Vertical().ControlChildSize(true, true).ChildForceExpand(false,false).Anchor(AnchorPresets.HorStretchTop).Pivot(PivotPresets.TopCenter).WidthDelta (0).PreferredSizeFitter(true, false);
        }

        public override void Style()
        {
            base.Style();
        }

        void RebuildContent()
        {
            var contentRect = Content.rectTransform;
            int childCount = contentRect.childCount;
            int childIndex = 0;
            int itemIndex = 0;
            int itemCount = items.Count;

            while (childIndex < childCount && itemIndex < itemCount) {
                var child = contentRect.GetChild(childIndex);
                var itemView = child.GetComponent<TreeItemView>();
                itemView.Item (items[itemIndex], itemIndex);
                ++childIndex;
                ++itemIndex;
            }
            while (childIndex < childCount) {
                var go = contentRect.GetChild(childIndex++).gameObject;
                Debug.Log($"[TreeView] deleting {go.name} {go.GetComponent<TreeItemView>().Text.tmpText.text}");
                Destroy(go);
            }
            while (itemIndex < itemCount) {
                Content
                    .Add<TreeItemView>()
                    .Item(items[itemIndex], itemIndex)
                    .OnStateChanged((int i, bool o) => onStateChanged.Invoke(i, o))
                    .FlexibleLayout(true, false)
                    .Finish();
                ++itemIndex;
            }
        }

        public TreeView OnStateChanged(UnityAction<int, bool> action)
        {
            onStateChanged.AddListener(action);
            return this;
        }

        public TreeView Items(List<TreeItem> items)
        {
            this.items = items;
            RebuildContent();
            return this;
        }
    }
}
