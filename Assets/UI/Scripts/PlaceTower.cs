using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    public GameLogic GameLogic;
    public void PlaceTowerClick(int towerType)
    {
        if(GameLogic.money >= GameLogic.buildCost)
        {
            string towerResource;
            GenericTower.TowerTypeEnum myTowerType = (GenericTower.TowerTypeEnum)towerType;

            switch (myTowerType)
            {
                case GenericTower.TowerTypeEnum.Fire:
                    towerResource = "FireTower";
                    break;
                case GenericTower.TowerTypeEnum.Ice:
                    towerResource = "IceTower";
                    break;
                case GenericTower.TowerTypeEnum.Rock:
                    towerResource = "RockTower";
                    break;
                case GenericTower.TowerTypeEnum.Lightning:
                    towerResource = "LightningTower";
                    break;
                default:
                    throw new Exception("Unknown Tower Type:" + towerType);
            }

            GameObject prefab = Resources.Load<GameObject>("Towers/" + towerResource) as GameObject;
            GameObject newTower = Instantiate(prefab);

            var towers = GameObject.Find("Towers");
            newTower.transform.SetParent(towers.transform);

            GenericTower newGenericTower = newTower.GetComponent<GenericTower>();
            newGenericTower.Initizalize(myTowerType);

            GameLogic.money -= GameLogic.buildCost;
        }
    }
}
