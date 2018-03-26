using UnityEngine;

[ExecuteInEditMode] // Update the size even whn not in play mode
public class UICameraResizer : MonoBehaviour
{
    private Camera cam;
    private int screenHeight = -1;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        Update();
    }

    private void Update()
    {
        int newHeight = Screen.height;
        if (screenHeight != newHeight)
        {
            screenHeight = newHeight;
            cam.orthographicSize = screenHeight * 0.5f;
        }
    }
}
