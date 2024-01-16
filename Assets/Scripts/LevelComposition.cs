using System;
using UnityEngine;

[Serializable]
public struct LevelComposition
{
    public Walls walls;
    public GameObject doors;
    public Environment[] environment;
    public GameObject[] floor;
    public GameObject[] enemies;
    public GameObject horizontalRoomConnector;
    public GameObject verticalRoomConnector;
}