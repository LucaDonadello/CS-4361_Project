using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // UI elements
    public TMP_Text highScoreUI;
    string newGameScene = "SampleScene";
    public AudioClip bg_music;
    public AudioSource main_channel;
    public EventListener EventListener;

    // start is called before the first frame update
    void Start()
    {
        main_channel.PlayOneShot(bg_music);
        // release the cursor
        Cursor.visible = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        // load the highscore
        int highScore = SaveLoadManager.Instance.LoadHighScore();
        highScoreUI.text = $"Top Wave Survived: {highScore}";
     }

    public void StartNewGame()
    {
        main_channel.Stop();
        SceneManager.LoadScene(newGameScene);
    }

    public void ExitApplication()
    {
#if UnityEditor
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }

}
