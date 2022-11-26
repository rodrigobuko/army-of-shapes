using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ArmyGenerator : MonoBehaviour
{
    [SerializeField] private int numberOfUnitsInArmy;

    [SerializeField] private List<Renderer> _armiesPlaces;
    [SerializeField] private List<int> _armiesLayerMasks;

    [SerializeField] private GameObject Cube;
    [SerializeField] private GameObject Sphere;
    [SerializeField] private float OffesetX = 5.0f;

    private List<Army> armies;

    private void Start()
    {
        armies = new List<Army>();
    }

    public void GenerateArmy()
    {
        DeletePreviousArmies();
        UnitGenerator unitGenerator = gameObject.GetComponent<UnitGenerator>();
        int layerArmyIndex = 0;
        foreach (var location in _armiesPlaces)
        {
            Army newArmy = CreateArmy(layerArmyIndex);
            armies.Add(newArmy);
            float offset = 0.0f;
            for (int i = 0; i < numberOfUnitsInArmy; i++)
            {
                Unit unit = unitGenerator.GenerateUnit();
                GameObject unitGameObject = CreateUnit(unit, transform, GetLocation(location));
                
                newArmy.AddUnit(unit);
                newArmy.AddUnityObject(unitGameObject);
            }

            layerArmyIndex++;
        }
    }

    private Army CreateArmy(int layerArmyIndex)
    {
        Army newArmy = new Army();
        if (layerArmyIndex == 0)
        {
            newArmy.SetLayers(_armiesLayerMasks[0], _armiesLayerMasks[1]);
        }
        else
        {
            newArmy.SetLayers(_armiesLayerMasks[1], _armiesLayerMasks[0]);
        }

        return newArmy;
    }

    private GameObject CreateUnit(Unit unit, Transform location, Vector3 position)
    {
        GameObject unitCreated;
        switch (unit.Shape)
        {
            case TypeOfShape.Cube:
                unitCreated = Instantiate(Cube, location);
                break;
            case TypeOfShape.Sphere:
                unitCreated = Instantiate(Sphere, location);
                break;
            default:
                Debug.LogError("This Shape dont have prefab");
                return null;
        }
        
        unitCreated.GetComponent<UnitComponent>().AttachUnit(unit);
        unitCreated.transform.position = position;

        return unitCreated;
    }

    void DeletePreviousArmies()
    {
        foreach (var army in armies)
        {
            foreach (var unitGameObject in army.UnitGameObjects)
            {
                Destroy(unitGameObject);
            }
            army.DeleteArmy();
        }
    }

    Vector3 GetLocation(Renderer armyPlace)
    {
        Bounds placeBounds = armyPlace.bounds;
        float centerX = placeBounds.center.x;
        float centerZ = placeBounds.center.z;
        float minX = centerX - placeBounds.size.x / 2;
        float minZ = centerZ - placeBounds.size.z / 2;
        float maxX = centerX + placeBounds.size.x / 2;
        float maxZ = centerZ + placeBounds.size.z / 2;
        
        Vector3 position = new Vector3(UnityEngine.Random.Range(minX, maxX), 0, UnityEngine.Random.Range(minZ, maxZ));
        return position;
    }
}
