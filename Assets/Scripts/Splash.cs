using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Splash
{
    public Vector3 position;
    public Color color;

    //Line class Initializer. Takes in a LineRenderer and stores its data in the respective places
    public Splash(GameObject splash)
    {
        position = splash.transform.position;
        color = splash.GetComponent<SpriteRenderer>().color;
    }
    public void Draw()
    {
        GameObject newStamp = GameObject.Instantiate(CanvasDrawer.staticSplashPrefab, position, Quaternion.identity);
        newStamp.GetComponent<SpriteRenderer>().color = color;
        CanvasDrawer.splashObjects.Add(newStamp);
    }
}
