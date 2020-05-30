using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager
{
    static List<Transform> WaypointList;
    static List<GameObject> EnemyList = new List<GameObject>();
    public int AssignmentNumber = 0;

    public enum EnemyTypeEnum
    {
        Enemy1, Enemy2, Enemy3, Enemy4, Enemy5
    }

    public static void SetWaypointsList()
    {
        var wayPointParent = GameObject.Find("Waypoints");
        WaypointList = new List<Transform>();
        foreach (Transform child in wayPointParent.transform) WaypointList.Add(child);
    }

    public void CreateEnemy(EnemyTypeEnum enemyType)
    {
        string enemyName = GetEnemyNameFromEnum(enemyType);

        GameObject enemyPrefab = Resources.Load<GameObject>("Enemies/" + enemyName) as GameObject;
        GameObject newEnemy = UnityEngine.Object.Instantiate(enemyPrefab);
        var enemyParent = GameObject.Find("Enemies");
        newEnemy.transform.SetParent(enemyParent.transform);
        newEnemy.tag = enemyParent.tag;
        
        SpriteRenderer spriteRendered = newEnemy.GetComponent<SpriteRenderer>();
        spriteRendered.sortingOrder = (int)enemyType;

        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.Initialize(WaypointList);

        EnemyList.Add(newEnemy);
    }

    private string GetEnemyNameFromEnum(EnemyTypeEnum enemyType)
    {
        switch (enemyType)
        {
            case EnemyTypeEnum.Enemy1:
                {
                    return "Enemy1";
                }
            case EnemyTypeEnum.Enemy2:
                {
                    return "Enemy2";
                }
            case EnemyTypeEnum.Enemy3:
                {
                    return "Enemy3";
                }
            case EnemyTypeEnum.Enemy4:
                {
                    return "Enemy4";
                }
            case EnemyTypeEnum.Enemy5:
                {
                    return "Enemy5";
                }
            default:
                {
                    throw new Exception("Unknown EnemyTypeEnum:" + enemyType);
                }
        }
    }
}
