using UnityEngine;
using System.Collections;

public class UnitSelection : MonoBehaviour {

    private Vector2 mouseButton1DownP;
    private Vector2 mouseButton1UpP;
    private Vector2 mouseButton2DownPoint;
    private Vector2 mouseButton2UpPoint;


    private Vector3 nouseButton1DownTerrainHitPoint;
    private Vector3 selectionPointStart;
    private Vector3 selectionPointEnd;

    private bool mouseLeftDrag = false;

    private LayerMask terrainLayerMask = 1 << 8;

    private float raycastLength = 200.0f;

    public Texture selectionTexture;

    private int mouseButtonReleaseBlurRange = 20;


    private void OnGUI()
    {
        if (mouseLeftDrag)
        {
            float w = mouseButton1UpP.x - mouseButton1DownP.x;
            float h = (Screen.height - mouseButton1UpP.y) - (Screen.height - mouseButton1DownP.y);
            Rect rect = new Rect(mouseButton1DownP.x, Screen.height - mouseButton1DownP.y, w, h);
            GUI.DrawTexture(rect, selectionTexture, ScaleMode.StretchToFill, true);
        }

    }

   

    private void Start()
    {
        this.transform.position = Vector3.zero;
    }
	
	private void FixedUpdate () {

        if (Input.GetButtonDown("Fire1"))
        {
           

        }




        //var mTarget = Input.mousePosition;

        //if (Input.GetMouseButtonDown(1))
        //{
        //    this.transform.position = mTarget;

        //}

    }

    private void Mouse1DownDrag(Vector2 screenPos)
    {
        if (screenPos != mouseButton1DownP)
        {
            mouseLeftDrag = true;

            mouseButton1UpP = screenPos;

            RaycastHit hit;


        }

    }
}
