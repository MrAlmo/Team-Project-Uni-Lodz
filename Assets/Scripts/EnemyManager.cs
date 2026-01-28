using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] LVL3_teleport teleport;

    int enemiesAlive;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("Enemies: " + enemiesAlive);
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        Debug.Log("Enemies left: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted() 
    {
        teleport.Active = true;
        teleport.gameObject.SetActive(true);
    }

}
