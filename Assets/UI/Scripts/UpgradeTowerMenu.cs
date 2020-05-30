using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeTowerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayLength;
    public LayerMask layermask;
    public Camera Camera;
    public GenericTower myTower = null;
    public GameObject myUpgradeMenu;

    
    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var position = new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f);
            var possibleTowers = Physics2D.OverlapCircleAll(position, 0.3f, 256);

            GameObject tower = null;

            foreach (var hit in possibleTowers)
            {
                if (hit.transform.position.x == position.x && hit.transform.position.y == position.y)
                {
                    tower = hit.transform.gameObject;
                }
            }

            //RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.ScreenToWorldPoint(Input.mousePosition).x, Camera.ScreenToWorldPoint(Input.mousePosition).y), -Vector2.up, 0f, LayerMask.GetMask("Towers"));
            if (tower != null)
            {
                myTower = tower.GetComponent<GenericTower>();

                if (myTower.IsFireMode == true)
                {
                    myUpgradeMenu.SetActive(true);
                }
            }
        }
    }
    
}
