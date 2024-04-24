using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stamp
{
    public Vector3 position;

    //Line class Initializer. Takes in a LineRenderer and stores its data in the respective places
    public Stamp(GameObject stamp)
    {
        position = stamp.transform.position;
    }
    public void Draw()
    {        
        GameObject newStamp = GameObject.Instantiate(CanvasDrawer.staticStampPrefab, position, Quaternion.identity);
        CanvasDrawer.stampObjects.Add(newStamp);
    }
}
