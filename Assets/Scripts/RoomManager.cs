using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    [SerializeField] private Vector2Int roomSize = new Vector2Int(20, 20);
    [SerializeField] private int distanceBetweenRooms;
    [Min(1)][SerializeField] private int roomsAmount;
    [SerializeField] private LevelComposition[] roomsComposition;

    private List<Vector2> roomsMap;

    private void GenerateDungeonMap()
    {
        roomsMap = new List<Vector2>(roomsAmount);
        roomsMap.Add(new Vector2(0, 0));
        while (roomsMap.Count < roomsAmount)
        {
            var index = Random.Range(0, roomsMap.Count - 1);
            Vector2 direction = roomsMap[index] + RandomDirection();
            if (!roomsMap.Contains(direction))
            {
                roomsMap.Add(direction);
            }
        }
    }

    private Vector2 RandomDirection()
    {
        var random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                return new Vector2(0, 1);
            case 1:
                return new Vector2(0, -1);
            case 2:
                return new Vector2(1, 0);
            case 3:
                return new Vector2(-1, 0);
            default:
                return Vector2.zero;
        }
    }

    private void CreateRoom(Vector2 coordinates)
    {
        GameObject obj = new GameObject("Room");
        Room room = obj.AddComponent<Room>();

        room.RoomSize = roomSize;

        room.Coordinates = coordinates * (roomSize + new Vector2(distanceBetweenRooms, distanceBetweenRooms));


        bool isUp = false;
        bool isDown = false;
        bool isLeft = false;
        bool isRight = false;

        if (roomsMap.Contains(coordinates + Vector2.up))
        {
            isUp = true;
        }

        if (roomsMap.Contains(coordinates + Vector2.down))
        {
            isDown = true;
        }

        if (roomsMap.Contains(coordinates + Vector2.left))
        {
            isLeft = true;
        }

        if (roomsMap.Contains(coordinates + Vector2.right))
        {
            isRight = true;
        }

        if (isUp)
        {
            if (isDown)
            {
                if (isLeft)
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.UDLRwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.UDLwall;
                    }
                }
                else
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.UDRwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.UDwall;
                    }
                }
            }
            else
            {
                if (isLeft)
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.ULRwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.ULwall;
                    }
                }
                else
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.URwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.Uwall;
                    }
                }
            }
        }
        else
        {
            if (isDown)
            {
                if (isLeft)
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.DLRwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.DLwall;
                    }
                }
                else
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.DRwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.Dwall;
                    }
                }
            }
            else
            {
                if (isLeft)
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.LRwall;
                    }
                    else
                    {
                        room.Walls = roomsComposition[currentLevel].walls.Lwall;
                    }
                }
                else
                {
                    if (isRight)
                    {
                        room.Walls = roomsComposition[currentLevel].walls.Rwall;
                    }
                }
            }
        }

        if (roomsComposition[currentLevel].environment.Length != 0)
        {
            var envIndex = Random.Range(0, roomsComposition[currentLevel].environment.Length);
            if (coordinates != Vector2.zero)
            {
                room.Environment = roomsComposition[currentLevel].environment[envIndex];
            }
        }

        if (roomsComposition[currentLevel].floor.Length != 0)
        {
            var floorIndex = Random.Range(0, roomsComposition[currentLevel].floor.Length);
            room.Floor = roomsComposition[currentLevel].floor[floorIndex];
        }

        if (roomsComposition[currentLevel].enemies.Length != 0)
        {
            room.Enemies = roomsComposition[currentLevel].enemies;
        }

        if (roomsComposition[currentLevel].doors)
        {
            room.Doors = roomsComposition[currentLevel].doors;
        }

        room.GenerateRoom();
    }

    private void CreateHorizontalRoomConnector(Vector2 coordinates)
    {
        if (roomsComposition[currentLevel].horizontalRoomConnector)
        {
            var connector = Instantiate(roomsComposition[currentLevel].horizontalRoomConnector, transform.position, Quaternion.identity);
            connector.transform.position = coordinates * (roomSize + new Vector2(distanceBetweenRooms, distanceBetweenRooms));
        }
    }

    private void CreateVerticalRoomConnector(Vector2 coordinates)
    {
        if (roomsComposition[currentLevel].verticalRoomConnector)
        {
            var connector = Instantiate(roomsComposition[currentLevel].verticalRoomConnector, transform.position, Quaternion.identity);
            connector.transform.position = coordinates * (roomSize + new Vector2(distanceBetweenRooms, distanceBetweenRooms));
        }
    }

    private void GenerateDungeon()
    {
        GenerateDungeonMap();
        SortMap();
        foreach (var coordinate in roomsMap)
        {
            CreateRoom(coordinate);
            if(roomsMap.Contains(coordinate + Vector2.right))
            {
                CreateHorizontalRoomConnector(coordinate + new Vector2(0.5f, 0));
            }
            if(roomsMap.Contains(coordinate + Vector2.down))
            {
                CreateVerticalRoomConnector(coordinate + new Vector2(0, -0.5f));
            }
        }
    }

    private void SortMap()
    {
        roomsMap.Sort((a, b) =>
        {
            if (a.y == b.y)
            {
                return a.x.CompareTo(b.x);
            }

            return a.y.CompareTo(b.y);
        }
        );
    }

    private void Awake()
    {
        GenerateDungeon();
        AstarPath.active.Scan();
    }
}
