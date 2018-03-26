using KodeUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITester : MonoBehaviour
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

    // Reminder since I keep forgetting: The UI must be built while in play mode for the buttons to work
    public void BuildUI()
    {
        if (testUI)
            return;

        LayoutPanel basePanel = UIKit.CreateUI<LayoutPanel>(appCanvas.transform as RectTransform, "testUI");
        testUI = basePanel.gameObject;

        basePanel.Vertical().ControlChildSize(true, true).ChildForceExpand(false,false).PreferredSizeFitter(true, true).Anchor(AnchorPresets.MiddleCenter).Pivot(PivotPresets.TopLeft).PreferredWidth(150).Finish()
            .Add<UIText>().Text("A test").Alignment(TextAlignmentOptions.Top).FlexibleLayout(true,false).Pivot(PivotPresets.TopCenter).Finish()
            .Add<UIButton>().Text("Button A").OnClick(ButtonAction).FlexibleLayout(true,false).Finish()
            .Add<Layout>().Horizontal().ControlChildSize(false, false).ChildForceExpand(false,false).Anchor(AnchorPresets.HorStretchTop).FlexibleLayout(true,false)
                .Add<UIButton>().Text("B").OnClick(ButtonAction).Finish()
                .Add<UIButton>().Text("C").OnClick(ButtonAction).Finish()
            .Finish()
            .Add<Layout>().Horizontal().ControlChildSize(false, false).ChildForceExpand(false,false).Anchor(AnchorPresets.HorStretchTop).FlexibleLayout(true,false)
                .Add<UIButton>().Text("Very Long text").OnClick(ButtonAction).FlexibleLayout(true,false).Finish()
                .Add<UIButton>().Text("Is Long").OnClick(ButtonAction).FlexibleLayout(true,false).Finish()
            .Finish()
            .Add<UIText>().Text("A long text that should overflow to the next line").Alignment(TextAlignmentOptions.TopLeft).FlexibleLayout(true,false).Finish()
            .Add<UIText>(out text).Text("N/A").Alignment(TextAlignmentOptions.TopRight).FlexibleLayout(true,false).Finish()
            .Add<UIToggle>().OnClick(Action).FlexibleLayout(false,false).Width(40).Height(40).Finish()
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
        text.Text("Stuff");
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

}
