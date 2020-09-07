using UnityEditor;
using UnityEngine;

namespace Assets.SupportScript
{
    [InitializeOnLoad]
    public static class UITesterKeyboard
    {
        static UITesterKeyboard()
        {
            SceneView.duringSceneGui += view =>
            {
                var e = Event.current;
                if (e == null || !e.isKey)
                    return;

                if (e.alt && e.keyCode == KeyCode.R)
                {
                    UITester tester = Object.FindObjectOfType<UITester>();
                    Debug.Log("Recreating the UI");
                    if (tester.testUI != null){
                        
                        tester.DestroyUI();
                    }
                    tester.BuildUI();
                }

            };
        }


    }
}
