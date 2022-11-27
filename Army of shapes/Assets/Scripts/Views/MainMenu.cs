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

    private void Start()
    {
        _highScore.text = $"Bet Game HIGHSCORE: {_playerScore.GetHighScore()}";
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
}
