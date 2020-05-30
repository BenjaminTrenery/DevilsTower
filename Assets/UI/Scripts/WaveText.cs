using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
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
        Text.text = "Wave: " + GameLogic.waveCount + " / " + GameLogic.myWaves.Count;
    }
}
