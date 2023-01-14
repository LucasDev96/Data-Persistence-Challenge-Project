using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public GameObject PauseText;
    public GameObject ButtonsHolder;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_HighScore = 0;
    
    private bool m_GameOver = false;
    private bool m_GamePaused = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started && !m_GamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        PauseGame();
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SetPlayerScore();
        SetHighScoreText();
    }

    private void PauseGame()
    {
        if (!m_GamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            m_GamePaused = true;
            ButtonsHolder.SetActive(true);
            PauseText.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (m_GamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            m_GamePaused = false;
            ButtonsHolder.SetActive(false);
            PauseText.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Set the text for the high score if the new score is higher
    private void SetHighScoreText()
    {
        if (m_Points > m_HighScore) m_HighScore = m_Points;
        HighScoreText.text = $"High Score: {m_HighScore}";
    }

    // Set the playerScore in the DataManager, and adjust highscore variables if needed
    public void SetPlayerScore()
    {
        DataManager.Instance.playerScore = m_Points;

        if (m_Points > DataManager.Instance.highScore)
        {
            DataManager.Instance.highScore = m_Points;
            DataManager.Instance.highScoreName = DataManager.Instance.playerName;
        }
    }
}
