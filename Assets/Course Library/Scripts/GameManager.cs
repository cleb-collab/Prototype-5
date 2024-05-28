using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{ 
      public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI livesText;
    private int health;
    public bool isGamepaused;

    public Slider volumeSlider;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
      volumeSlider.value = 1;
      volumeSlider.onValueChanged.AddListener(delegate{ValueChangeCheck();});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChangeCheck()
        {
            audioSource.volume = volumeSlider.value;
            Debug.Log(volumeSlider.value);
        }
    
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    
    public void UpdateLives(int livesToAdd)
    {
        health += livesToAdd;

        if (health <  1)
        {
            health = 0;
            GameOver();

        }
        livesText.text = "Lives: " + health;
        
        
    } 
    
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;

        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateLives(3);
        UpdateScore(0);
        

        titleScreen.gameObject.SetActive(false);
    }
    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
