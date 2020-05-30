using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Enemy : MonoBehaviour
{
    private List<Transform> WaypointList;
    private HealthBar HealthbarScript;
    public float health;


    public float moveSpeed;
    public float originalMoveSpeed;
    public float offsetX = 0, offsetY = 0;
    private float currOffsetX;
    private Vector2 moveVector;
    public int AssignmentNumber;
    public float maxTime = 2f;
    public float StartTime;
    public bool IsSlowed = false;
    public float slowAmount = 0;
    public GameLogic GameLogic;

    private int currWaypoint = 0;

    public void Initialize(List<Transform> waypointList)
    {
        WaypointList = waypointList;

        transform.position = new Vector2(WaypointList[currWaypoint].transform.position.x + offsetX, WaypointList[currWaypoint].transform.position.y + offsetY);
        currOffsetX = offsetX;
        currWaypoint++;

        Animator animator = GetComponent<Animator>();
        animator.SetFloat("WalkSpeedMultiplier", moveSpeed - 0.3f);
        SetMoveVector();
        HealthbarScript = GetComponentInChildren<HealthBar>();

    }

    public void SetHealth(float percent)
    {
        if (HealthbarScript != null)
        {
            HealthbarScript.SetHealth(percent);
        }
    }

    private void SetMoveVector()
    {
        moveVector = new Vector2(WaypointList[currWaypoint].transform.position.x + currOffsetX, WaypointList[currWaypoint].transform.position.y + offsetY);
    }

    public void Start()
    {
        this.originalMoveSpeed = this.moveSpeed;
    }

    private void Update()
    {
        moveEnemy();

        if(this.health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (currWaypoint == WaypointList.Count - 1)
        {
            GameLogic.lives--;
            Destroy(this.gameObject);

        }

        moveSpeedSlow();
    }

    public void moveSpeedSlow()
    {
        if (IsSlowed == true)
        {
            //Debug.Log("Slow Amount: " + slowAmount);
            if(Time.time - StartTime <= maxTime)
            {
                this.moveSpeed = originalMoveSpeed * ((100 - slowAmount) / 100);
                Debug.Log("Current Move Speed: " + this.moveSpeed);
                Debug.Log("Original Move Speed: " + originalMoveSpeed);
            }
            else
            {
                this.moveSpeed = this.originalMoveSpeed;
                IsSlowed = false;
            }
        }
    }

    private void moveEnemy()
    {
        if (WaypointList == null) return;

        transform.position = Vector2.MoveTowards(transform.position, moveVector, moveSpeed * Time.deltaTime);


        if(Mathf.Abs(transform.position.x - WaypointList[currWaypoint].transform.position.x - currOffsetX) < 0.01 && Mathf.Abs(transform.position.y - WaypointList[currWaypoint].transform.position.y - offsetY) < 0.01)
        {
            currWaypoint++;

            bool isPartTurnWaypoint = currWaypoint <= 11;
            Quaternion rotation = isPartTurnWaypoint ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

            if (rotation != transform.rotation)
            {
                transform.rotation = rotation;
                if (HealthbarScript != null) HealthbarScript.SetRotation(Quaternion.Euler(0, 0, 0));
            }

            var saveOffsetX = currOffsetX;
            currOffsetX = isPartTurnWaypoint ? offsetX : -offsetX;

            if (saveOffsetX != currOffsetX)
            {
                transform.position = new Vector2(transform.position.x + currOffsetX * 2, transform.position.y);
            }

            SetMoveVector();
        }
    }
}
