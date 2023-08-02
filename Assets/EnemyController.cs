using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    public EnemySpawner enemySpawner;
    public Transform target; // Referência para o Transform do jogador.
    public Tilemap obstaclesTilemap; // Referência para o Tilemap que contém os obstáculos.
    public float moveSpeed = 0.5f;

    private Queue<Vector3Int> pathQueue = new Queue<Vector3Int>();

    public float health = 1;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get { return health; }
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        obstaclesTilemap = GameObject.FindWithTag("Ground").GetComponent<Tilemap>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null)
        {
            CalculatePath();
            MoveOnPath();
        }
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");

        if (enemySpawner != null)
        {
            enemySpawner.EnemyDestroyed();
        }
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
    
    #region EnemyMovement

    private void CalculatePath()
    {
        pathQueue.Clear();

        Vector3Int startCell = obstaclesTilemap.WorldToCell(transform.position);
        Vector3Int targetCell = obstaclesTilemap.WorldToCell(target.position);

        if (startCell == targetCell)
            return;

        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();

        frontier.Enqueue(startCell);
        cameFrom[startCell] = startCell;

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

            if (current == targetCell)
                break;

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    frontier.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        Vector3Int currentCell = targetCell;
        while (currentCell != startCell)
        {
            pathQueue.Enqueue(currentCell);
            currentCell = cameFrom[currentCell];
        }
    }

    private List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();
        neighbors.Add(cell + Vector3Int.up);
        neighbors.Add(cell + Vector3Int.down);
        neighbors.Add(cell + Vector3Int.left);
        neighbors.Add(cell + Vector3Int.right);
        return neighbors;
    }

    private void MoveOnPath()
    {
        if (pathQueue.Count > 0)
        {
            Vector3Int nextCell = pathQueue.Peek();
            Vector3 nextPosition = obstaclesTilemap.GetCellCenterWorld(nextCell);
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
                pathQueue.Dequeue();
        }
    }

    #endregion
}