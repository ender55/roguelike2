using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    [SerializeField] private Vector2 coordinates;
    [SerializeField] private Vector2Int roomSize;
    [SerializeField] private GameObject walls;
    [SerializeField] private GameObject doors;
    [SerializeField] private Environment environment;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject[] enemies;
    
    private BoxCollider2D boxCollider;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    
    public bool isActive;

    public Vector2Int RoomSize
    {
        get => roomSize;
        set => roomSize = value;
    }

    public GameObject Walls 
    {
        get => walls;
        set => walls = value;
    }
    public GameObject Doors 
    {
        get => doors;
        set => doors = value;
    }
    public Environment Environment
    {
        get => environment;
        set => environment = value;
    }
    public GameObject Floor 
    {
        get => floor;
        set => floor = value;
    }
    public GameObject[] Enemies
    {
        get => enemies;
        set => enemies = value;
    }
    public Vector2 Coordinates
    {
        get => coordinates;
        set => coordinates = value;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player component))
        {
            if (isActive == false)
            {
                if (enemies.Length != 0 && environment.spawnPoints != null)
                {
                    foreach (var coordinate in environment.spawnPoints)
                    {
                        var index = Random.Range(0, enemies.Length);
                        spawnedEnemies.Add(Instantiate(enemies[index], coordinate + coordinates, Quaternion.identity));
                    }
                    doors.SetActive(true);
                }
                isActive = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy component))
        {
            if (spawnedEnemies.Contains(other.gameObject))
            {
                spawnedEnemies.Remove(other.gameObject);
                if (spawnedEnemies.Count == 0)
                {
                    doors.SetActive(false);
                }
            }
        }
    }

    public void GenerateRoom()
    {
        gameObject.transform.position = coordinates;
        
        if (walls)
        {
            walls = Instantiate(walls, coordinates, Quaternion.identity);
            walls.transform.SetParent(gameObject.transform);
        }

        if (environment.decorations)
        {
            environment.decorations = Instantiate(environment.decorations, coordinates, Quaternion.identity);
            environment.decorations.transform.SetParent(gameObject.transform);
        }

        if (floor)
        {
            floor = Instantiate(floor, coordinates, Quaternion.identity);
            floor.transform.SetParent(gameObject.transform);
        }

        if (doors)
        {
            doors = Instantiate(doors, coordinates, Quaternion.identity);
            doors.transform.SetParent(gameObject.transform);
            doors.SetActive(false);
        }
        
        AddCollider();
        NameObject("Room");
    }

    private void AddCollider()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(roomSize.x - 3.5f, roomSize.y - 3.5f);
        boxCollider.isTrigger = true;
    }

    private void NameObject(string name)
    {
        gameObject.name = name;
    }
}
