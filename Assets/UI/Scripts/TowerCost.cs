using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCost : MonoBehaviour
{
    public static int money;
    public GameLogic GameLogic;
    public Text Text;
    public bool forUpgrade;
    void Awake()
    {
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "$" + GameLogic.buildCost;
    }
}
