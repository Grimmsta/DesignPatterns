using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private ScriptableEnemy m_ScriptableObject;
    private Pathfinding pathFinding;
    private GameManager gameManager;
    private Vector3 endBase;
    private int currentTile;
    private List<Vector3> path;
    private int speed = 1;

    private void Awake()
    {
        pathFinding = FindObjectOfType<Pathfinding>();
        gameManager = FindObjectOfType<GameManager>();

        path = pathFinding.WorldPos;
        endBase = new Vector3(pathFinding.MapMono.EndPos.x*2, 0f, pathFinding.MapMono.EndPos.y*2);
        speed = m_ScriptableObject.speed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPos = path[currentTile];
        
        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
        }
        else
        {
            if (transform.position == new Vector3(endBase.x, endBase.y+ 0.75f, endBase.z))
            {
                gameObject.SetActive(false);
                gameManager.UpdateTowerHealth();
                ResetUnit();
            }
            currentTile++;
        }
    }
    public void ResetUnit()
    {
        currentTile = 0;
    }
}