using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveFile
{
    [SerializeField]
    public List<Line> lines;

    [SerializeField]
    public List<Stamp> stamps;

    [SerializeField]
    public List<Splash> splashes;

    public SaveFile(List<LineRenderer> lineRenderers = null, List<GameObject> stampObjects = null, List<GameObject> splashObjects = null)
    {
        lines = new List<Line>();

        stamps = new List<Stamp>();

        splashes = new List<Splash>();

        if (lineRenderers != null)
        {
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                lines.Add(new Line(lineRenderer));
            }
        }

        if (stampObjects != null)
        {
            foreach (GameObject stampObject in stampObjects)
            {
                stamps.Add(new Stamp(stampObject));
            }
        }

        if (splashObjects != null)
        {
            foreach (GameObject splashObject in splashObjects)
            {
                splashes.Add(new Splash(splashObject));
            }
        }
    }

    public void Draw()
    {
        foreach (Line line in lines)
        {
            line.Draw();
        }

        foreach (Stamp stamp in stamps)
        {
            stamp.Draw();
        }

        foreach (Splash splash in splashes)
        {
            splash.Draw();
        }
    }
}