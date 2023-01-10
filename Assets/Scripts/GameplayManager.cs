using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public Text GameOverText;
    public Button playGameButton;
    public Button startMenuButton;

    private string playerName;
    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        playerName = MainManager.Instance.playerName;
        ScoreText.text = $"{playerName} Score : {m_Points}";

        int bestScore = MainManager.Instance.bestScore;
        string bestScoreName = MainManager.Instance.bestScoreName;
        BestScoreText.text = $"Best Score: {bestScoreName}: {bestScore}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
        if (!m_Started && Ball)
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
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{playerName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;

        if (m_Points > MainManager.Instance.bestScore)
        {
            MainManager.Instance.bestScore = m_Points;
            MainManager.Instance.bestScoreName = MainManager.Instance.playerName;

            int bestScore = MainManager.Instance.bestScore;
            string bestScoreName = MainManager.Instance.bestScoreName;
            BestScoreText.text = $"Best Score: {bestScoreName}: {bestScore}";
        }

        GameOverText.gameObject.SetActive(true);
        playGameButton.gameObject.SetActive(true);
        startMenuButton.gameObject.SetActive(true);
    }
}
