using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map Data", menuName = "Scriptable Object/Map Data", order = int.MaxValue)]
public class MapListSO : ScriptableObject
{
    public List<MapEffection> MapEffections = new List<MapEffection>();
    public bool isSingleMap = false;
}
