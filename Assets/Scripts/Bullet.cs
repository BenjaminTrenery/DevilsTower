using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fireSpeed;
    public Vector2 ShootCoordinates;
    public Vector2 SpawnPoint;
    public float damage;
    private Enemy Enemy;
    public float maxTime = 1f;
    public Enemy myEnemy;
    public bool IsMultiTarget;
    public GenericTower myTower;
    public float slowAmount;

    public void Start()
    {
        myTower = this.GetComponentInParent<GenericTower>();
        damage = myTower.damage;
        fireSpeed = myTower.projectileMovementSpeed;
        slowAmount = myTower.SlowAmount;
    }


    public void Update()
    {
        if(myTower.IsFireMode == true)
        {
            if(myTower.myEnemy != null)
            {
                SpawnPoint = myTower.SpawnPoint;
                myEnemy = myTower.myEnemy;
                ShootCoordinates = myEnemy.transform.position;

                this.transform.position = Vector2.MoveTowards(transform.position, ShootCoordinates, fireSpeed * Time.deltaTime);


                if (Mathf.Abs(transform.position.x - ShootCoordinates.x) <= .05 && Mathf.Abs(transform.position.y - ShootCoordinates.y) <= .05)
                {
                    if (myTower.IsMultiHit == true)
                    {
                        var missleHits = Physics2D.OverlapCircleAll(myEnemy.transform.position, 0.5f, 4096);

                        foreach (var hit in missleHits)
                        {
                            Enemy SplashEnemy = hit.GetComponent<Enemy>();
                            doDamage(SplashEnemy);

                            if (slowAmount > 0)
                            {
                                slowEnemy(SplashEnemy);
                            }
                        }
                    }
                    else
                    {
                        doDamage(myEnemy);
                    }

                    this.gameObject.SetActive(false);
                    transform.position = SpawnPoint;
                }

                 if (Time.time - myTower.StartTime > maxTime)
                 {
                    this.gameObject.SetActive(false);
                    this.transform.position = SpawnPoint;
                 }
            }
            else
            {
                this.gameObject.SetActive(false);
                this.transform.position = SpawnPoint;
            }

        }
        


    }
  
    public void doDamage(Enemy Enemy)
    {
        //Debug.Log(Enemy.health);
        if(Enemy != null)
        {
            float percentage = ((Enemy.health - damage) / Enemy.health) * 100;
            Enemy.health -= damage;

            if (Enemy.health > 0)
            {
                Enemy.SetHealth(percentage);

            }
        }

    }

    public void slowEnemy(Enemy Enemy)
    {

        Enemy.IsSlowed = true;

        Enemy.StartTime = Time.time;

        Enemy.slowAmount = slowAmount;
    }
}
