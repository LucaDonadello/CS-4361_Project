using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Health Points
    public int HP = 100;
    public GameObject bloodyScreen;
    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;
    public bool isDead;
    
    private void Start()
    {
        // default components defined in the editor
        //playerHealthUI.text = $"Health: {HP}";
    }
    
    // collison with zombie hand and triggers animations
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            // player dead
            print("Player Dead");
            PlayerDead();
            isDead = true;
        }
        else
        {
            // player hit
            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
            playerHealthUI.text = $"Health: {HP}";
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }
    private void PlayerDead()
    {
        // player dead
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDie);
        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
        SoundManager.Instance.playerChannel.PlayDelayed(1f);
    
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        // dying animation
        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        // fade to black
        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
    }
    
    private IEnumerator ShowGameOverUI()
    {
        // show game over UI
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
        
        int waveSurvived = GlobalReferences.Instance.waveNumber;

        // save the high score
        if (waveSurvived - 1 > SaveLoadManager.Instance.LoadHighScore())
        {
            SaveLoadManager.Instance.SaveHighScore(waveSurvived - 1);
        }
        
        StartCoroutine(ReturnToMainMenu());
    }
    
    private IEnumerator ReturnToMainMenu()
    {
        // return to main menu
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }
    
    private IEnumerator BloodyScreenEffect()
    {
        // show bloody screen
        if (bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }

        // get the Image component of the bloody screen.
        var image = bloodyScreen.GetComponentInChildren<Image>();

        // set the alpha value of the color to 1.
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // calculate the alpha value which is the lerp value between 1 and 0.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // update the alpha value of the color.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // add the time passed since the last frame.
            elapsedTime += Time.deltaTime;

            yield return null; ; // wait for the next frame.
        }

        // hide the bloody screen
        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }
    
    // collision with zombie hand
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
        }
    }
}
