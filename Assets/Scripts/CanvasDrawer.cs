using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDrawer : MonoBehaviour
{ 
    public GameObject brushPrefab; 

    private GameObject currentBrush; 
    private LineRenderer currentLine; 
    public GameObject stampPrefab;
    

    public void Draw()
    {
        switch (ToolManager.Instance.currentTool)
        {
            case DrawingTool.Pen:
                DrawPen();
                break;
            case DrawingTool.Bucket:
                FillCanvas();
                break;
            case DrawingTool.Stamp:
                DrawStamp();
                break;
            case DrawingTool.Eraser:
                Erase();
                break;
        }

        

    }

    private void DrawPen()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))
        {
            if (currentLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentLine.positionCount++;
                currentLine.SetPosition(currentLine.positionCount - 1, mousePosition);
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine = null;
        }
    }

    private void CreateBrush()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentBrush = Instantiate(brushPrefab, mousePosition, Quaternion.identity);
        currentLine = currentBrush.GetComponent<LineRenderer>();

        if (currentLine != null)
        {
            currentLine.positionCount = 1;
            currentLine.SetPosition(0, mousePosition);
            if (ToolManager.Instance.currentTool == DrawingTool.Eraser)
                currentLine.startColor = currentLine.endColor = Camera.main.backgroundColor;
            else
                currentLine.startColor = currentLine.endColor = ToolManager.Instance.currentColor;
        }
    }
    
    private void FillCanvas()
    {
        ClearCanvas();

        GameObject canvasObject = GameObject.Find("Canvas");
        if (canvasObject != null)
        {
            Camera canvasRenderer = canvasObject.GetComponent<Camera>();
            if (canvasRenderer != null)
            {
                canvasRenderer.backgroundColor = ToolManager.Instance.currentColor;
            }
        }
    }

    private void ClearCanvas()
    {
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
        
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
    }

    private void DrawStamp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceStamp();
        }
    }

    private void PlaceStamp()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 
        GameObject stampInstance = Instantiate(stampPrefab, mousePosition, Quaternion.identity);
    }

    private void Erase()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))
        {
            if (currentLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentLine.positionCount++;
                currentLine.SetPosition(currentLine.positionCount - 1, mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine = null;
        }
    }


}

