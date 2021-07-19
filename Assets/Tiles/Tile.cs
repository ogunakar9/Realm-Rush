using System;
using UnityEngine;

  public class Tile : MonoBehaviour
  {
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    PathFinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
      gridManager = FindObjectOfType<GridManager>();
      pathfinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
      if (gridManager != null)
      {
        coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

        if (!isPlaceable)
        {
          gridManager.BlockNode(coordinates);
        }
      }
    }

    void OnMouseDown()
    {
      if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
      {
        bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
        if (isSuccessful)
        {
          gridManager.BlockNode(coordinates);
          pathfinder.NotifyReceivers();
        }
      }
    }
  }

