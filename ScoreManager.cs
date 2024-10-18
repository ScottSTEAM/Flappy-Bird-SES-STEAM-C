using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ScoreManager : MonoBehaviour
{
    public int _score;
    [SerializeField]public  TextMeshProUGUI ScoreText;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI PanelScoreText;
    [SerializeField] private TextMeshProUGUI PanelBestScoreText;
    [SerializeField] private GameObject OverlayPanel;
    [SerializeField] private GameObject B1, B2;
    [SerializeField] private SpriteRenderer birdSpriteRenderer;
    [SerializeField] private Sprite birdSprite1, birdSprite2;
    [SerializeField] public GameObject q1, q2, q3;
    public static ScoreManager instance;
    private void OnEnable()
    {
        instance = this;
        PillerMovement.OnPillerPassed += UpdateScore;
        BirdMovement.OnGameOver += ShowGameOverPanel;
    }
    private void OnDisable()
    {
        PillerMovement.OnPillerPassed -= UpdateScore;
        BirdMovement.OnGameOver -= ShowGameOverPanel;
    }
    private void Start()
    {
        _score = 0;

    }

    private void ShowGameOverPanel(object sender, EventArgs e)
    {
        if (!GameOverPanel.activeInHierarchy)
        {
            GameOverPanel.SetActive(true);
            OverlayPanel.SetActive(true);
            ScoreText.gameObject.SetActive(false);
           // Reset the position to be off-screen (below the bottom of the screen)
           GameOverPanel.transform.localPosition = new Vector3(0, -Screen.height, 0);

            // Animate the panel to slide up into view
            GameOverPanel.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce);

            // Update score text
            PanelScoreText.text = _score.ToString();
            PanelBestScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }


    public void UpdateScore(object sender, EventArgs e)
    {
        if (GameOverPanel == null) return;

        if (!GameOverPanel.activeInHierarchy)
        {
            _score++;
            if (_score == 4)
            {
                B1.SetActive(false);
                birdSpriteRenderer.sprite = birdSprite1;

                BirdMovement.instance.OnPhasePassed(q1);

            }
            if (_score == 8)
            {
                B2.SetActive(false);
                birdSpriteRenderer.sprite = birdSprite2;

                BirdMovement.instance.OnPhasePassed(q2);
            }
            if(_score == 12)
            {

            }
   
            Sounder.instance.Play("POINT");
            ScoreText.text = (int.Parse(ScoreText.text) + 1).ToString();
            if (_score >= PlayerPrefs.GetInt("HighScore", 0))
            {
                Debug.Log($"{_score} :{PlayerPrefs.GetInt("HighScore", 0)} ");
                PlayerPrefs.SetInt("HighScore", _score);
                PlayerPrefs.Save();
            }
        }
    }
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
    public void ReloadGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int levelNo = currentScene.buildIndex;
        SceneManager.LoadScene(levelNo);
        Time.timeScale = 1;
    }

}
