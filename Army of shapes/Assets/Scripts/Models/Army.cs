using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    private int _armyNumber;

    public int ArmyNumber => _armyNumber;

    private List<Unit> _units;
    private List<GameObject> _unitGameObjects;
    
    public List<Unit> Units => _units;
    public List<GameObject> UnitGameObjects => _unitGameObjects;

    private int _armyLayerMask;
    private int _enemyArmyLayerMask;
    public Army(int armyNumber)
    {
        // to start with one
        _armyNumber = armyNumber+1;
        _units = new List<Unit>();
        _unitGameObjects = new List<GameObject>();
    }

    public void SetLayers(int armyLayerMask, int enemyArmyLayerMask)
    {
        _armyLayerMask = armyLayerMask;
        _enemyArmyLayerMask = enemyArmyLayerMask;
    }

    public void AddUnityObject(GameObject unitGameObject)
    {
        unitGameObject.layer = _armyLayerMask;
        unitGameObject.GetComponent<UnitComponent>().SetEnemyArmyLayerMask(_enemyArmyLayerMask);
        _unitGameObjects.Add(unitGameObject);
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }
    
    public void RemoveUnit(Unit unit)
    {
        _units.RemoveAll(u =>  u.Id == unit.Id);
    }

    public void DeleteArmy()
    {
        _units = new List<Unit>();
        _unitGameObjects = new List<GameObject>();
    }

    public int AliveUnits()
    {
        int count = 0;
        foreach (var gameObject in _unitGameObjects)
        {
            count += gameObject.GetComponent<UnitComponent>().IsDead? 0 : 1;
        }

        return count;
    }
}
