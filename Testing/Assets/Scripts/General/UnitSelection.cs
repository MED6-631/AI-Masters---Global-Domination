using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public class UnitSelection : MonoBehaviour {

    bool isSelecting = false;
    Vector3 mousePosition1;

    public GameObject selectionCirclePrefab;

    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach(var selectableObject in FindObjectsOfType<Unit>())
            {
                if(selectableObject.selectionCircle != null)
                {
                    Destroy(selectableObject.selectionCircle.gameObject);
                    selectableObject.selectionCircle = null;
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {

            var selectedObjects = new List<Unit>();
            foreach(var selectableObject in FindObjectsOfType<Unit>())
            {
                if(IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectedObjects.Add(selectableObject);
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
            foreach(var selectedObject in selectedObjects)
            {
                sb.AppendLine(" -> " + selectedObject.gameObject.name);
            }

            Debug.Log(sb.ToString());

            isSelecting = false;


        }

        if(isSelecting)
        {
            foreach(var selectableObject in FindObjectsOfType<Unit>())
            {
                if(IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    if(selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
                else
                {
                    if(selectableObject.selectionCircle != null)
                    {
                        Destroy(selectableObject.selectionCircle.gameObject);
                        selectableObject.selectionCircle = null;
                    }
                }
            }
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
