using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameGameManager : MonoBehaviour
{
    [Header("Ingame Variables")]
    public Text textScore;
    public Text textHP;
    public Text textTimer;
    public Text topScoreText;
    public Text lastScore;

    [Header("Game Object Caller")]
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject ingameUI;

    [Header("Other Variables")]
    private int score;
    private int playerHP;
    public float timer;
    private int topScore;
    private bool togglePauseMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        topScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        score = PlayerPrefs.GetInt("TotalScore");
        textScore.text = "Score : " + score.ToString();

        textTimer.text = "Timer : " + timer.ToString();

        playerHP = PlayerPrefs.GetInt("PlayerHP");

        if (playerHP <= 0)
        {
            gameOverUI.gameObject.SetActive(true);
            ingameUI.gameObject.SetActive(false);
            //show total score and topscore
            lastScore.text = "Last Score : " + score.ToString();
            topScoreText.text = "TOP SCORE : " + topScore;
            TopScore();

            Time.timeScale = 0;

        }
        else
        {
            gameOverUI.gameObject.SetActive(false);
            //ingameUI.gameObject.SetActive(true);
            //Keep Going Until Player die
        }

        textHP.text = playerHP.ToString() + " : HP";
    
    }


    void TopScore()
    {
        topScore = PlayerPrefs.GetInt("TOPSCORE");

        if (topScore <= score)
        {
            topScore = score;
            PlayerPrefs.SetInt("TOPSCORE", topScore);
        }
    }

    public void PauseGame()
    {

        togglePauseMenu = !togglePauseMenu;

        if (togglePauseMenu == true)
        {
            pauseMenuUI.gameObject.SetActive(true);
            //ingameUI.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            //ingameUI.SetActive(true);
            Time.timeScale = 1;
        }

    }

    public void SceneTrigger(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
