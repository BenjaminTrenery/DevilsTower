using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    public static int money;
    public GameLogic GameLogic;
    public Text Text;
    public UpgradeAndDestroyTower myTower;

    void Awake()
    {
        Text = GetComponent<Text>();
        myTower = GetComponentInParent<UpgradeAndDestroyTower>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myTower != null)
        {
            Text.text = "$" + (myTower.myTower.TowerLevel * 75);
        }
    }
}
