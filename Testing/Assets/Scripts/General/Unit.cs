using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public bool isSelected = false;
    public GameObject selectionCircle;
    public GameObject marker;
    public bool hasReachedTarget = false;

    void FixedUpdate()
    {
        if(selectionCircle != null)
        {
            isSelected = true;
        }
        else if (selectionCircle == null)
        {
            isSelected = false;
        }


    }
}
