using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrawingInfo", menuName = "Drawing Info")]
public class DrawingInfo : ScriptableObject
{
    public int LineCount;
    public Vector3[] Positions;
    public Color LineColor;
}
