using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfGame
{
    RandomBattle,
    BetBattle
}
[CreateAssetMenu(menuName = "Army of Shapes/GameType")]
public class GameType : ScriptableObject
{
    private TypeOfGame _typeOfGame;

    public TypeOfGame TypeOfGame => _typeOfGame;
    
    public void SetGameType(TypeOfGame gameType)
    {
        _typeOfGame = gameType;
    }
}
