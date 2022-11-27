using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using Random = System.Random;

public class ArmyGenerator : MonoBehaviour
{
    [Header("Armies Presets")]
    [SerializeField] private int numberOfUnitsInArmy;

    [SerializeField] private List<Renderer> _armiesPlaces;
    [SerializeField] private List<int> _armiesLayerMasks;

    [SerializeField] private GameObject Cube;
    [SerializeField] private GameObject Sphere;
    
    [SerializeField] private GameObject _board;

    [Header("Camera")] [SerializeField] public CinemachineTargetGroup TargetGroup;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [Header("UI")] 
    [SerializeField] private Canvas _canvas;
    
    [Header("Gameplay")] 
    [SerializeField] private GameType _gameType;
    [SerializeField] private PlayerScore _playerScore;

    private Camera _mainCamera;
    private List<Army> armies;
    private ArmyBattleUI _armyBattleUI;

    private void Start()
    {
        armies = new List<Army>();
        _mainCamera = Camera.main;
        _virtualCamera.LookAt = _board.transform;
        _armyBattleUI = gameObject.GetComponent<ArmyBattleUI>();
        _armyBattleUI.Setup(_gameType.TypeOfGame);
        if (_gameType.TypeOfGame == TypeOfGame.BetBattle)
        {
            GenerateArmy();
        }
    }

    public void GenerateArmy()
    {
        DeletePreviousArmies();
        _virtualCamera.LookAt = TargetGroup.transform;
        TargetGroup.RemoveMember(_board.transform);
        
        //UnitGenerator unitGenerator = gameObject.GetComponent<UnitGenerator>();
        int armyNumber = 0;
       
        foreach (var location in _armiesPlaces)
        {
            Army newArmy = CreateArmy(armyNumber);
            armies.Add(newArmy);
            for (int i = 0; i < numberOfUnitsInArmy; i++)
            {
                Unit unit = _gameType.UnitGenerator.GenerateUnit();
                GameObject unitGameObject = CreateUnit(unit, transform, GetLocation(location));
                unitGameObject.GetComponent<DamageComponent>().SetUpHealthBar(_canvas, _mainCamera);
                unitGameObject.GetComponent<UnitComponent>().SetEnemyArmyName(armyNumber+1, _mainCamera);
                newArmy.AddUnit(unit);
                newArmy.AddUnityObject(unitGameObject);
                TargetGroup.AddMember(unitGameObject.transform, 1, 2);
            }

            armyNumber++;
        }
        
        _armyBattleUI.SetArmies(armies);
    }

    private Army CreateArmy(int armyNumber)
    {
        Army newArmy = new Army(armyNumber);
        if (armyNumber == 0)
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
                TargetGroup.RemoveMember(unitGameObject.transform);
                Destroy(unitGameObject);
            }
            army.DeleteArmy();
        }
        foreach (Transform child in _canvas.transform) {
            Destroy(child.gameObject);
        }
        armies.Clear();
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

    public void CheckWinner()
    {
        _armyBattleUI.UpdateArmyNumber();
        List<Army> armiesToRemove = new List<Army>();
        foreach (Army army in armies)
        {
            int unitsAlive = army.AliveUnits();
            if (unitsAlive == 0)
            {
                Debug.Log($"Army {army.ArmyNumber} DEFEATED");
                armiesToRemove.Add(army);
            }
        }

        foreach (Army armyToRemove in armiesToRemove)
        {
            armies.Remove(armyToRemove);
        }

        if (armies.Count == 1)
        {
            Debug.Log($"Army {armies.First().ArmyNumber} WON");
            if (_gameType.TypeOfGame == TypeOfGame.BetBattle)
            {
                _armyBattleUI.EndBetBattle(armies.First(), _playerScore);
            }
            else
            {
                _armyBattleUI.EndBattle(armies.First());
            }
            
            TargetGroup.AddMember(_board.transform, 100, 100);
        }
            
    }

    public void GenerateArmisAfterBet()
    {
        if (_gameType.TypeOfGame == TypeOfGame.BetBattle)
        {
            GenerateArmy();
        }
    }
}
