using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // save and load the highscore
    public static SaveLoadManager Instance { get; set; }

    string highScoreKey = "BestWaveSavedValue";

    // start to get the instance
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    // save the highscore
    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(highScoreKey, score);
    }

    // load the highscore
    public int LoadHighScore()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            return PlayerPrefs.GetInt(highScoreKey);
        }
        else
        {
            return 0;
        }

    }
}
