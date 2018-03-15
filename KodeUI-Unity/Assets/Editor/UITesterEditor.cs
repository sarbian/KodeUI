using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UITester))]
public class UITesterEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UITester controller = (UITester) target;
        if(!controller.testUI && GUILayout.Button("Build UI"))
        {
            controller.BuildUI();
        }

        if (controller.testUI && GUILayout.Button("Destroy UI"))
        {
            controller.DestroyUI();
        }

        Event current = Event.current;
        if (current.type != EventType.KeyDown)
            return;

        if (current.keyCode == KeyCode.RightAlt)
            Debug.Log("ALT!");
    }
}
