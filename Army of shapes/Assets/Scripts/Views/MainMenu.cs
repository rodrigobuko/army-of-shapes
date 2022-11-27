using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private readonly string _randomBattleGameScene = "RandomBattle";
    [SerializeField] private Text _highScore;
    [SerializeField] private GameType _gameType;
    [SerializeField] private PlayerScore _playerScore;
    
    [Header("Change Generator")]
    [SerializeField] private Text _btnChangeText;
    [SerializeField] private UnitGeneratorSO _basicGameGenerator;
    [SerializeField] private UnitGeneratorSO _newGameGenerator;
    private bool _basicGame;
    
    private void Start()
    {
        _highScore.text = $"Bet Game HIGHSCORE: {_playerScore.GetHighScore()}";
        _basicGame = true;
        _gameType.UnitGenerator = _basicGameGenerator;
        ChangeBtnGeneratorText();
    }

    public void LoadBetBattleGameScene()
    {
        _playerScore.Score = 0;
        _gameType.SetGameType(TypeOfGame.BetBattle);
        SceneManager.LoadScene(_randomBattleGameScene);
    }
    
    public void LoadRandomBattleGameScene()
    {
        _gameType.SetGameType(TypeOfGame.RandomBattle);
        SceneManager.LoadScene(_randomBattleGameScene);
    }

    public void ChangeUnitGenerator()
    {
        if (_basicGame)
        {
            _basicGame = false;
            _gameType.UnitGenerator = _newGameGenerator;
        }
        else
        {
            _basicGame = true;
            _gameType.UnitGenerator = _basicGameGenerator;
        }
        ChangeBtnGeneratorText();
    }

    private void ChangeBtnGeneratorText()
    {
        if (!_basicGame)
        {
            _btnChangeText.text = $"NEW UNITS ON";
        }
        else
        {
            _btnChangeText.text = $"NEW UNITS OFF";
        }
    }
}
