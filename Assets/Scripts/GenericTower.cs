using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class GenericTower : MonoBehaviour
{
    public enum TowerTypeEnum
    {
        Fire, Ice, Rock, Lightning
    }

    public bool IsPlacementMode = true;
    public bool IsFireMode = false;
    public TowerTypeEnum TowerType;
    public bool IsMultiHit = false;
    public float SlowAmount;
    public GameObject projectile;
    public float offsetx;
    public float offsety = 0.8f;
    public GameObject myProjectile;
    public Vector2 SpawnPoint = new Vector2(0, 0);
    private Rigidbody2D rb;
    public float projectileMovementSpeed;
    public int TowerLevel;
    public float damage;
    public float StartTime;
    public Dictionary<int, Enemy> myEnemies = new Dictionary<int, Enemy>();
    public List<int> myIDs = new List<int>();
    public Enemy myEnemy;
    public GameLogic GameLogic;
    public int boughtCost;

    float SaveLastPosX = -1;
    float SaveLastPosY = -1;
    bool IsPlaceable = false;


    public void Initizalize(TowerTypeEnum towerType)
    {
        TowerType = towerType;
    }

    public void Start()
    {
        boughtCost = GameLogic.buildCost - 20;
        switch(this.TowerType)
        {
            case TowerTypeEnum.Fire:
            {
                projectile = Resources.Load<GameObject>("Towers/Fire") as GameObject;
                
                break;
            }
            case TowerTypeEnum.Ice:
            {
                projectile = Resources.Load<GameObject>("Towers/Ice") as GameObject;
                break;
            }
            case TowerTypeEnum.Lightning:
            {
                projectile = Resources.Load<GameObject>("Towers/Lightning") as GameObject;
                break;
            }
            case TowerTypeEnum.Rock:
            {
                projectile = Resources.Load<GameObject>("Towers/Rock") as GameObject;
                break;
            }
        }

        rb = projectile.GetComponent<Rigidbody2D>();
        myProjectile = GameObject.Instantiate(projectile, SpawnPoint, Quaternion.Euler(0, 0, 0));
        myProjectile.SetActive(false);
        myProjectile.transform.SetParent(this.transform);

        if (TowerLevel == 1)
        {
            this.IsPlacementMode = true;
            this.IsFireMode = false;
        }

        else
        {
            this.IsPlacementMode = false;
            this.IsFireMode = true;
            myProjectile.transform.position = SpawnPoint;
        }


        rb.IsSleeping();

    }

    public void Update()
    {
        if (this.IsPlacementMode)
        {
            this.UpdatePlacement();
            SpawnPoint = new Vector2(transform.position.x + offsetx, transform.position.y + offsety);
            myProjectile.transform.position = SpawnPoint;
        }

        if (IsFireMode == true && myEnemies.Count > 0 && myProjectile.activeSelf == false)
        {

            rb.IsAwake();
            myEnemy = myEnemies[myIDs[0]];
            myProjectile.SetActive(true);
            StartTime = Time.time;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
              Enemy Enemy = collision.gameObject.GetComponent<Enemy>();
              myEnemies.Add(collision.gameObject.GetInstanceID(), Enemy);
              myIDs.Add(collision.gameObject.GetInstanceID());
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            myEnemies.Remove(collision.gameObject.GetInstanceID());
            myIDs.Remove(collision.gameObject.GetInstanceID());
        }
    }

    

    public void UpdatePlacement()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f);

        if (transform.position.x != SaveLastPosX || transform.position.y != SaveLastPosY)
        {
            var possibleTowers = Physics2D.OverlapCircleAll(transform.position, 0.4f, 16384 + 256);

            bool overAvailableSpot = false;
            bool overtower = false;

            foreach (var hit in possibleTowers)
            {
                if (hit.transform.gameObject == this.transform.gameObject)
                {
                    continue;
                }
                if (hit.transform.gameObject.layer == 8 && hit.transform.position == transform.position)
                {
                    overtower = true;
                }
                if (hit.transform.gameObject.layer == 14)
                {
                    overAvailableSpot = true;
                }
            }

            IsPlaceable = overAvailableSpot && !overtower;

            GetComponent<SpriteRenderer>().color = IsPlaceable ? Color.white : Color.red;
        }


        if (Input.GetMouseButton(0) && IsPlaceable)
        {
            IsPlacementMode = false;
            IsFireMode = true;
        }
        else if (Input.GetMouseButton(1))
        {
            Destroy(this.gameObject);
            GameLogic.money += boughtCost;
            GameLogic.buildCost -= 20;
        }

    }

    public void UpgradeTower()
    {
        this.TowerLevel++;
        Vector2 towerPosition = this.transform.position;

        if(TowerLevel < 4)
        {
            GameObject upgradedTower = Resources.Load("Towers/" + this.TowerType + "TowerL" + this.TowerLevel) as GameObject;
            GenericTower myUpgradedTower = Instantiate(upgradedTower, towerPosition, Quaternion.Euler(0, 0, 0)).GetComponent<GenericTower>();
            myUpgradedTower.SpawnPoint = SpawnPoint;
            myUpgradedTower.transform.SetParent(this.transform.parent);
            Destroy(myProjectile);
            Destroy(this.gameObject);
        }

    }

}
