using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Army of Shapes/PlayerScore")]
public class PlayerScore : ScriptableObject
{
    public string ScoreKey;
    public int Score;

    public int GetHighScore()
    {
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            return PlayerPrefs.GetInt(ScoreKey);
        }

        return 0;
    }

    public void SaveHighScore()
    {
        if (!PlayerPrefs.HasKey(ScoreKey))
        {
            PlayerPrefs.SetInt(ScoreKey, 0);
        }

        int highscore = PlayerPrefs.GetInt(ScoreKey);
        if (Score > highscore)
        {
            PlayerPrefs.SetInt(ScoreKey, Score);
        }
    }
}
