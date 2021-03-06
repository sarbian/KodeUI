﻿using System.Collections.Generic;
using KodeUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITester : LoadingSystem
{
    public static UITester Instance { get; private set; }

    public Canvas appCanvas;

    public GameObject testUI;

    private void Awake()
    {
        UITester.Instance = this;
        appCanvas = GetComponent<Canvas>();
    }

    UIText text;
    private bool ready = false;

    public override bool IsReady()
    {
        return Instance.ready;
    }

    public override void StartLoad()
    {
        Instance.BuildUI();
        ready = true;
    }

    class TestTreeItem {
        string name;
        TestTreeItem []children;

        public int Count
        {
            get {
                if (children != null) {
                    return children.Length;
                }
                return 0;
            }
        }
        public TestTreeItem this[int index]
        {
            get { return children[index]; }
        }
        public string Name { get { return name; } }

        public TestTreeItem(string name, TestTreeItem []children = null)
        {
            this.name = name;
            this.children = children;
        }
    }

    static TestTreeItem [] testTreeItems = {
        new TestTreeItem ("item 1", new TestTreeItem [] {
            new TestTreeItem ("item A"),
            new TestTreeItem ("item B"),
            new TestTreeItem ("item C"),
        }),
        new TestTreeItem ("item 2"),
        new TestTreeItem ("item 3", new TestTreeItem [] {
            new TestTreeItem ("item E"),
            new TestTreeItem ("item F"),
            new TestTreeItem ("item G"),
            new TestTreeItem ("item H"),
        }),
        new TestTreeItem ("item 4", new TestTreeItem [] {
            new TestTreeItem ("item I"),
        }),
        new TestTreeItem ("item 5"),
    };

    List<TreeView.TreeItem> treeItems;
    TreeView treeView;

    // Reminder since I keep forgetting: The UI must be built while in play mode for the buttons to work
    public void BuildUI()
    {
        if (testUI)
            return;

        treeItems = new List<TreeView.TreeItem> ();
        foreach (var item in testTreeItems) {
            treeItems.Add (new TreeView.TreeItem (item, i => (i as TestTreeItem).Name, i => (i as TestTreeItem).Count != 0, 0));
        }

        Window basePanel = UIKit.CreateUI<Window>(appCanvas.transform as RectTransform, "testUI");
        testUI = basePanel.gameObject;

        basePanel
            .Title("A test")
            .Vertical()
            .ControlChildSize(true, true)
            .ChildForceExpand(false,false)
            .PreferredSizeFitter(true, true)
            .Anchor(AnchorPresets.MiddleCenter)
            .Pivot(PivotPresets.TopLeft)
            .PreferredWidth(300)
            .Add<UIButton>()
                .Text("Button A")
                .OnClick(ButtonAction)
                .FlexibleLayout(true,false)
                .Finish()
            .Add<Layout>()
                .Horizontal()
                .ControlChildSize(true, true)
                .ChildForceExpand(false,false)
                .Anchor(AnchorPresets.HorStretchTop)
                .FlexibleLayout(true,false)
                    .Add<UIEmpty>()
                        .FlexibleLayout(true, true)
                        .Finish()
                    .Add<UIButton>()
                        .Text("B")
                        .OnClick(ButtonAction)
                        .Finish()
                    .Add<UIEmpty>()
                        .FlexibleLayout(true, true)
                        .Finish()
                    .Add<UIButton>()
                        .Text("C")
                        .OnClick(ButtonAction)
                        .Finish()
                    .Add<UIEmpty>()
                        .FlexibleLayout(true, true)
                        .Finish()
                    .Add<UIButton>()
                        .Image(SpriteLoader.GetSprite("KodeUI/Default/toggle_on"))
                        .OnClick(ButtonAction)
                        .Finish()
                    .Finish()
            .Add<Layout>()
                .Horizontal()
                .ControlChildSize(true, true)
                .ChildForceExpand(false,false)
                .Anchor(AnchorPresets.HorStretchTop)
                .FlexibleLayout(true,false)
                .Add<UIButton>()
                    .Text("Very Long text")
                    .OnClick(ButtonAction)
                    .FlexibleLayout(true,false)
                    .Finish()
                .Add<UIEmpty>()
                    .FlexibleLayout(true, true)
                    .Finish()
                .Add<UIButton>()
                    .Text("Is Long")
                    .OnClick(ButtonAction)
                    .FlexibleLayout(true,false)
                    .Finish()
                .Finish()
            .Add<UIText>()
                .Text("A long text that should overflow to the next line")
                .Alignment(TextAlignmentOptions.TopLeft)
                .FlexibleLayout(true,false)
                .Finish()
            
            .Add<Layout>()
                .Horizontal()
                .ControlChildSize(true, true)
                .ChildForceExpand(false,false)
                .Anchor(AnchorPresets.HorStretchTop)
                .Add<UIText>(out text)
                    .Text("N/A")
                    .Alignment(TextAlignmentOptions.TopLeft)
                    .FlexibleLayout(true,false)
                    .Finish()
                .Finish()
            
            .Add<UIToggle>()
                .OnValueChanged(Action)
                .FlexibleLayout(false,true)
                .PreferredSize(15,15)
                .Finish()
            .Add<UIInputField>()
                .FlexibleLayout(true, false)
                .SizeDelta(0,0)
                .Finish()
            .Add<TreeView>(out treeView)
                .Items(treeItems)
                .OnClick(OnTreeClicked)
                .OnStateChanged(OnTreeStateChanged)
                .PreferredSize(-1,150)
                .FlexibleLayout(true, true)
                .Finish()
            .Finish();

        
        // needed in the editor to have the proper layout when not in play mode
        LayoutRebuilder.ForceRebuildLayoutImmediate(basePanel.rectTransform);


        // TODO:
        // * LayoutElement sur tout ? Flexible  Size H/V en fonction du layout parent ? 
        // * ContentSizeFitter sur tout/certain ? H/V en fonction du layout parent ? 
    }

    private void Update()
    {
        if (!text)
            return;

        //float f = Mathf.Exp( Mathf.Sin(Time.fixedTime) * 10f) * 1000f;
        float f = Mathf.Exp( 10f) * 1000f;
        text.Text(f.ToString("F12"));
    }

    public void DestroyUI()
    {
        if (testUI)
            DestroyImmediate(testUI);
    }

    public void ButtonAction()
    {
        Debug.Log("YAY");
    }

    private void Action(bool arg0)
    {
        Debug.Log("Now " + arg0);
    }

    private void OnTreeClicked(int index)
    {
        var item = treeItems[index].Object as TestTreeItem;
        Debug.Log("Tree item: " + item.Name);
    }

    private void OnTreeStateChanged(int index, bool open)
    {
        var item = treeItems[index].Object as TestTreeItem;
        if (open) {
            var newItems = new List<TreeView.TreeItem> ();
            int level = treeItems[index].Level + 1;
            for (int i = 0; i < item.Count; i++) {
                newItems.Add (new TreeView.TreeItem (item[i], it => (it as TestTreeItem).Name, it => (it as TestTreeItem).Count != 0, level));
            }
            treeItems.InsertRange(index + 1, newItems);
        } else {
            treeItems.RemoveRange(index + 1, item.Count);
        }
        treeView.Items(treeItems);
    }

}
