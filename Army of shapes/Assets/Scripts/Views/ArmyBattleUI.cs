using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArmyBattleUI : MonoBehaviour
{
    private readonly string _mainMenuScene = "MainMenu";
    
    [Header("UI")] 
    [SerializeField] private GameObject _beforeBattleUI;
    [SerializeField] private GameObject _battleUI;
    [SerializeField] private GameObject _afterBattleUI;
    [SerializeField] private GameObject _betUI;
    [SerializeField] private Button _battleButton;
    
    [SerializeField] private Text _army1Text;
    [SerializeField] private Text _army2Text;
    
    [SerializeField] private Text _winnerArmyText;
    [SerializeField] private ParticleSystem _winParticle;
    
    [SerializeField] private Text _score;

    private ArmyGenerator _armyGenerator;
    private List<Army> _armies;

    private Army _armyBeted;

    private TypeOfGame _typeOfGame;
    private void Start()
    {
        _army1Text.text = "0";
        _army2Text.text = "0";
    }

    public void Setup(TypeOfGame typeOfGame)
    {
        _armies = new List<Army>();
        _typeOfGame = typeOfGame;
        switch (typeOfGame)
        {
            case TypeOfGame.RandomBattle:
                _score.gameObject.SetActive(false);
                _beforeBattleUI.SetActive(true);
                _afterBattleUI.SetActive(false);
                _battleUI.SetActive(false);
                _betUI.SetActive(false);
                break;
            case TypeOfGame.BetBattle:
                _score.text = $"Score: 0";
                _beforeBattleUI.SetActive(false);
                _afterBattleUI.SetActive(false);
                _battleUI.SetActive(false);
                _betUI.SetActive(true);
                break;
        }
    }

    public void StartBattle()
    {
        _beforeBattleUI.SetActive(false);
        _afterBattleUI.SetActive(false);
        _battleUI.SetActive(true);
        _betUI.SetActive(false);
        UpdateArmyNumber();
    }
    
    public void EndBattle(Army winner)
    {
        _winParticle.Play();
        _afterBattleUI.SetActive(true);
        _beforeBattleUI.SetActive(false);
        _battleUI.SetActive(false);

        _winnerArmyText.text = $"ARMY {winner.ArmyNumber} WON!!!";
    }

    public void BackToBeforeBattle()
    {
        _winParticle.Stop();
        switch (_typeOfGame)
        {
            case TypeOfGame.RandomBattle:
                _beforeBattleUI.SetActive(true);
                _afterBattleUI.SetActive(false);
                _battleUI.SetActive(false);
                _betUI.SetActive(false);
                break;
            case TypeOfGame.BetBattle:
                _beforeBattleUI.SetActive(false);
                _afterBattleUI.SetActive(false);
                _battleUI.SetActive(false);
                _betUI.SetActive(true);
                break;
        }
    }

    public void SetArmies(List<Army> armies)
    {
        _armies = armies;
        UpdateArmyNumber();
    }

    private void Update()
    {
        if (_armies.Count >= 2)
        {
            _battleButton.interactable = true;
        }
        else
        {
            _battleButton.interactable = false;
        }
    }

    public void UpdateArmyNumber()
    {
        if (_armies.Count >= 2)
        {
            _army1Text.text = _armies[0].AliveUnits().ToString();
            _army2Text.text = _armies[1].AliveUnits().ToString();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
    
    public void EndBetBattle(Army winner, PlayerScore playerScore)
    {
        _afterBattleUI.SetActive(true);
        
        if (_armyBeted == winner)
        {
            _winParticle.Play();
            playerScore.Score++;
            playerScore.SaveHighScore();
            _score.text = $"Score: {playerScore.Score}";
            _winnerArmyText.text = $"ARMY {winner.ArmyNumber} WON! WELL DONE!";
        }
        else
        {
            playerScore.Score = 0;
            _score.text = $"Score: {playerScore.Score}";
            _winnerArmyText.text = $"ARMY {_armyBeted.ArmyNumber} LOST! TRY AGAIN NEXT TIME!";
        }

    }

    public void BetLeftArmy()
    {
        _armyBeted = _armies[0];
    }
    
    public void BetRightArmy()
    {
        _armyBeted = _armies[1];
    }
}
