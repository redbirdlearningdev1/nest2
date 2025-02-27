using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObjectPlacer : MonoBehaviour
{
    public static TempObjectPlacer instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject PlaceNewObject(GameObject obj, Vector2 pos)
    {
        // remove previous object
        foreach (Transform child in this.transform)
            GameObject.Destroy(child.gameObject);

        // IDK WHY THIS IS A THING BUT I GOTTA 
        //pos.x += 8.9f;

        var pos2 = Camera.main.ScreenToWorldPoint(pos);
        pos2.z = 0;

        // make new object at position
        return Instantiate(obj, pos2, Quaternion.identity, transform);
    }

    public void RemoveObject()
    {
        // remove previous object
        foreach (Transform child in this.transform)
            GameObject.Destroy(child.gameObject);
    }
}
