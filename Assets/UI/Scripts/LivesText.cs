using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    public GameLogic GameLogic;
    public Text Text;
    void Awake()
    {
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Lives Left: " + GameLogic.lives + " / 3";
    }
}