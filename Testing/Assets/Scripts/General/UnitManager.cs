using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {

    private static UnitManager instance;

    private ArrayList allUnitsList = new ArrayList();
    private ArrayList selectedUnitsList = new ArrayList();



    private int GetSelectedUnitsCount()
    {

        return selectedUnitsList.Count;
    }

    private void AddUnit(GameObject go)
    {
        allUnitsList.Add(go);

    }

    private void AddSelectedUnit(GameObject go)
    {
        selectedUnitsList.Add(go);
        go.SendMessage("SetUnitSelected", true);

    }

    private void ClearSelectedUnitsList()
    {
        foreach(GameObject go in allUnitsList)
        {
            go.SendMessage("SetUnitSelected", false);
        }

        selectedUnitsList.Clear();
    }

    private void MoveSelectedUnitsToPoint(Vector3 dest)
    {
        foreach (GameObject go in selectedUnitsList)
        {
            go.SendMessage("MoveToPoint", dest);
        }
    }

    void SelectUnitsInArea(Vector3 p1, Vector3 p2)
    {
        if (p2.x < p1.x)
        {
            var x1 = p1.x;
            var x2 = p2.x;
            p1.x = x2;
            p2.x = x1;

        }

        if (p2.z > p1.z)
        {
            var z1 = p1.z;
            var z2 = p2.z;
            p1.z = z2;
            p2.z = z1;
        }

        foreach (GameObject go in allUnitsList)
        {
            Vector3 goPos = go.transform.position;

            if (goPos.x > p1.x && goPos.x < p2.x && goPos.z < p1.z && goPos.z > p2.z)
            {
                selectedUnitsList.Add(go);
                go.SendMessage("SetUnitSelected", true);

            }

        }

    }

    void OnApplicationQuit()
    {
        instance = null;
    }

    void SetTarget(UnityEngine.GameObject newTarget)
    {
        foreach (GameObject go in selectedUnitsList)
        {
            go.SendMessage("SetTarget", newTarget);
        }
    }
}
