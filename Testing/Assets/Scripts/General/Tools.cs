using UnityEngine;
using System.Collections;

//Utility Library for useful commands for organizing's sake >.<
public static class Tools {

    static Texture2D _whiteTexture;

    public static Texture2D WhiteTexture
    {
        get
        {
            if(_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        //TOP
        Tools.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        //LEFT
        Tools.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        //RIGHT
        Tools.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        //BOTTOM
        Tools.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);

    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        //FIX mousePosition to coordinate with Unity's screen origin (0,0)
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;

        //Calculated Corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        //Return the new Rectangle
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);

    }

    public static Bounds GetViewportBounds(Camera cam, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = cam.nearClipPlane;
        max.z = cam.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;

    }

}
