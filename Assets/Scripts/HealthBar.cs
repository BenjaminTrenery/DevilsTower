using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform greenBar;
    float saveScaleX;

    private void Awake()
    {
        greenBar = transform.Find("HealthbarGreen");
        saveScaleX = greenBar.localScale.x;
    }

    public void SetHealth(float percent)
    {
        Vector3 theScale = greenBar.localScale;
        theScale.x = saveScaleX * percent / 100;
        greenBar.localScale = theScale;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
