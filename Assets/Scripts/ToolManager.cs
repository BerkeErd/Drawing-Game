using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrawingTool
{
    Pen,
    Bucket,
    Stamp,
    Eraser,
    PaintBall
}

public class ToolManager : MonoBehaviour
{
    private static ToolManager instance;
    public static ToolManager Instance => instance;

    public DrawingTool currentTool;

    private Color currentColor = Color.black;
    private Color currentBackgroundColor = Color.white; 

    public Color CurrentColor
    {
        get { return currentColor; }
        set { currentColor = value; }
    }

    public Color CurrentBackgroundColor
    {
        get { return currentBackgroundColor; }
        set { currentBackgroundColor = value; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SelectTool(int toolID)
    {
        switch (toolID)
        {
            case 0:
                currentTool = DrawingTool.Pen;
                break;
            case 1:
                currentTool = DrawingTool.Bucket;
                break;
            case 2:
                currentTool = DrawingTool.Stamp;
                break;
            case 3:
                currentTool = DrawingTool.Eraser;
                break;
            case 4:
                currentTool = DrawingTool.PaintBall;
                break;
            default:
                break;
        }
        
    }

    public void SelectColor(Color color)
    {
        CurrentColor = color;
    }
}
