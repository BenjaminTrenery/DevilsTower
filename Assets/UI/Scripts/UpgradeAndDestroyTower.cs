using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAndDestroyTower : MonoBehaviour
{
    public GenericTower myTower;
    public GameLogic GameLogic;

    private void Update()
    {
        myTower = this.gameObject.GetComponentInParent<UpgradeTowerMenu>().myTower;
    }

    public void UpgradeTower()
    {

        if (myTower.IsFireMode == true && GameLogic.money >= (75 * myTower.TowerLevel) && myTower.TowerLevel < 3)
        {
            myTower.UpgradeTower();
            GameLogic.money -= (75 * myTower.TowerLevel);
        }
    }

    public void DestroyTower()
    {
        GameLogic.money += myTower.boughtCost / 2;
        Destroy(myTower.gameObject);
    }
}
