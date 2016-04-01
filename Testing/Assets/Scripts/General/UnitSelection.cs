using UnityEngine;
using System.Collections;

public class UnitSelection : MonoBehaviour {

    bool isSelecting = false;
    Vector3 mousePosition1;

    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
        }

    }

    void OnGUI()
    {
        if(isSelecting)
        {
            //Create the Rectangle
            var rect = Tools.GetScreenRect(mousePosition1, Input.mousePosition);
            Tools.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Tools.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public bool IsWithinSelectionBounds(GameObject go)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds = Tools.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(camera.WorldToViewportPoint(go.transform.position));
    }


}
