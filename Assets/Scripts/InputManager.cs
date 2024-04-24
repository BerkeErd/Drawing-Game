
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public CanvasDrawer canvasDrawer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0) || Input.GetMouseButton(0))
        {
            if (!IsPointerOverUIObject())
            {
                canvasDrawer.Draw();
            }
        }
    }
    
    private bool IsPointerOverUIObject()
    {
        Vector2 touchPosition = Input.mousePosition;
        
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPosition;
        
        var results = new List<RaycastResult>();
        
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        
        return results.Count > 0;
    }
}

