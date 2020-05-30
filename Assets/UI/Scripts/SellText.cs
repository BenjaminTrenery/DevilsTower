using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellText : MonoBehaviour
{
    public static int money;
    public UpgradeAndDestroyTower myTower;

    public Text Text;
    void Awake()
    {
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myTower.myTower != null)
        {
            Text.text = "$" + (myTower.myTower.boughtCost / 2);
        }

    }
}

