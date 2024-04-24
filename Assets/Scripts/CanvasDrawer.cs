using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDrawer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private GameObject brushPrefab; 
    private GameObject currentBrush;

    

    private LineRenderer currentLine;


    [SerializeField] private GameObject paintballPrefab;
    [SerializeField] private GameObject stampPrefab;
    [SerializeField] private GameObject splashPrefab;

    public static GameObject staticLinePrefab;
    public static GameObject staticStampPrefab;
    public static GameObject staticSplashPrefab;

    public static List<LineRenderer> drawnLineRenderers = new List<LineRenderer>();
    public static List<GameObject> stampObjects = new List<GameObject>();
    public static List<GameObject> splashObjects = new List<GameObject>();
    
    [SerializeField] private FileManager fileManager;

    [SerializeField] private SfxManager sfxManager;
    


    private void Start()
    {
        Application.targetFrameRate = 144;
        staticLinePrefab = brushPrefab;
        staticStampPrefab = stampPrefab;
        staticSplashPrefab = splashPrefab;
        fileManager.LoadCanvas();
    }

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
            case DrawingTool.PaintBall:
                PaintBall();
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
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                currentLine.positionCount++;
                currentLine.SetPosition(currentLine.positionCount - 1, mousePosition);
                sfxManager.PlaySound(SoundType.Pen);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine = null;
            fileManager.SaveCanvas();
        }
    }

    private void CreateBrush()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentBrush = Instantiate(brushPrefab, mousePosition, Quaternion.identity);
        drawnLineRenderers.Add(currentBrush.GetComponent<LineRenderer>());
        currentLine = currentBrush.GetComponent<LineRenderer>();

        if (currentLine != null)
        {
            currentLine.positionCount = 1;
            currentLine.SetPosition(0, mousePosition);
            if (ToolManager.Instance.currentTool == DrawingTool.Eraser)
                currentLine.startColor = currentLine.endColor = mainCamera.backgroundColor;
            else
                currentLine.startColor = currentLine.endColor = ToolManager.Instance.CurrentColor;
        }
    }
    
    private void FillCanvas()
    {
        ClearCanvas();
        GameObject.Find("Canvas").GetComponent<Camera>().backgroundColor  = ToolManager.Instance.CurrentColor;
        ToolManager.Instance.CurrentBackgroundColor = ToolManager.Instance.CurrentColor;
        sfxManager.PlaySound(SoundType.Bucket);
        fileManager.SaveCanvas();
    }

    private void ClearCanvas()
    {
        foreach (LineRenderer lineRenderer in drawnLineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }

        foreach (GameObject stamp in stampObjects)
        {
            Destroy(stamp);
        }

        foreach (GameObject splash in splashObjects)
        {
            Destroy(splash);
        }

        drawnLineRenderers.Clear();
        splashObjects.Clear();
        stampObjects.Clear();
        fileManager.SaveCanvas();
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
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 1f; 
        GameObject stampInstance = Instantiate(stampPrefab, mousePosition, Quaternion.identity);
        sfxManager.PlaySound(SoundType.Stamp);
        stampObjects.Add(stampInstance);
        fileManager.SaveCanvas();
    }

    private void PaintBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(GeneratePaintball());
        }
    }


    private IEnumerator GeneratePaintball()
    {
        Color ballColor = ToolManager.Instance.CurrentColor;
        sfxManager.PlaySound(SoundType.PaintGun);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 
        
        GameObject paintball = Instantiate(paintballPrefab, mousePosition, Quaternion.identity);
        paintball.GetComponent<SpriteRenderer>().color = ballColor;

        yield return new WaitForEndOfFrame();
        
        Vector3 targetScale = Vector3.one * 0.1f;
        
        float duration = 1f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            paintball.transform.localScale = Vector3.Lerp(Vector3.one, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        
        paintball.transform.localScale = targetScale;
        
        Destroy(paintball);

        mousePosition.z = 1f;
        InstantiateSplash(mousePosition, ballColor);
    }

    private void InstantiateSplash(Vector3 position, Color splashColor)
    {
        GameObject splashInstance = Instantiate(splashPrefab, position, Quaternion.identity);
        sfxManager.PlaySound(SoundType.PaintballExplosion);
        splashInstance.GetComponent<SpriteRenderer>().color = splashColor;
        splashObjects.Add(splashInstance);
        fileManager.SaveCanvas();
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
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                currentLine.positionCount++;
                currentLine.SetPosition(currentLine.positionCount - 1, mousePosition);

                sfxManager.PlaySound(SoundType.Eraser);

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine = null;
            fileManager.SaveCanvas();
        }
    }


}

