using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private EnemyManager EnemyManager;
    public static List<List<int>> myWaves = new List<List<int>>();
    public static int waveCount = 0;
    public GameObject Enemies;
    private float startTime = 0f;
    private float maxTime = .4f;
    public List<int> myWave = new List<int>();
    public static int money;
    public static int numTowers;
    public GameObject Towers;
    public static int buildCost;
    public static int previousMoney;
    public static int lives = 3;
    public GameObject nextWaveText;
    public static float endOfWaveTime;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject TheGame;

    // Start is called before the first frame update

    public void newGame()
    {
        foreach(Transform child in Towers.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in Enemies.transform)
        {
            Destroy(child.gameObject);
        }

        myWaves.Clear();

        waveCount = 10;
        buildCost = 100 + (20 * numTowers);
        EnemyManager = new EnemyManager();
        EnemyManager.SetWaypointsList();
        Waves();
        money = 5000;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        numTowers = Towers.transform.childCount;
        buildCost = 100 + (20 * numTowers);


        if (waveCount < myWaves.Count && Enemies.transform.childCount <= 0)
        {
            
                previousMoney = money;
                myWave = myWaves[waveCount];
                waveCount++;
                float interest = previousMoney * (float).2;
                money += (100 * waveCount) + (int)Mathf.Round(interest);

        }
        if(myWave.Count > 0)
        {
            if(Time.time - startTime > maxTime)
            {
                int numPosition = UnityEngine.Random.Range(0, myWave.Count - 1);
                EnemyManager.CreateEnemy((EnemyManager.EnemyTypeEnum)myWave[numPosition]);
                myWave.RemoveAt(numPosition);
                startTime = Time.time;
            }
           
        }
        if(waveCount == myWaves.Count - 1 && Enemies.transform.childCount <= 0)
        {
            foreach (Transform child in Towers.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in Enemies.transform)
            {
                Destroy(child.gameObject);
            }

            TheGame.SetActive(false);
            WinScreen.SetActive(true);

            
        }

        if(lives <= 0)
        {
            foreach (Transform child in Towers.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in Enemies.transform)
            {
                Destroy(child.gameObject);
            }

            TheGame.SetActive(false);
            LoseScreen.SetActive(true);
        }
        
    }

    public void Waves()
    {

        myWaves.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
        myWaves.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 });
        myWaves.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
        myWaves.Add(new List<int>() { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 });
        myWaves.Add(new List<int>() { 2, 2, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 });
        myWaves.Add(new List<int>() { 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1 });
        myWaves.Add(new List<int>() { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 });
        myWaves.Add(new List<int>() { 3, 3, 2, 2, 2, 1, 1, 0, 1, 0, 2, 2, 2, 2, 1, 1, 1, 0, 0, 0, 1, 2, 1 });
        myWaves.Add(new List<int>() { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 });
        myWaves.Add(new List<int>() { 4, 4, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 2, 2, 2, 2, 3, 3, 3, 3, 1, 0, 1 });
        myWaves.Add(new List<int>() { 4, 4, 4, 4, 0, 1, 0, 1, 2, 0, 1, 2, 3, 3, 3, 3, 2, 1, 0, 1, 2, 3, 3, 2, 0, 2, 1, 2, 2, 2, 3, 3, 3 });
    }
}
