using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject hazardPrefab;
    [SerializeField] private int maxHazardsToSpawn = 3;
    [SerializeField] private GameObject CMVcam1;
    [SerializeField] private GameObject CMVcam2zoom;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Image backgroundMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI highScoreText;

    private int score;
    private int highScore;
    private float timer;
    private Coroutine hazardsCoroutine;
    private bool gameOver;
    private static GameManager instance;
    private const string HighScorePreferenceKey = "HighScore";

    public static GameManager Instance => instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt(HighScorePreferenceKey);
    }

    private void OnEnable()
    {
        player.transform.position = new Vector3(0f, 0.75f, 0f);
        player.SetActive(true);

        CMVcam1.SetActive(true);
        CMVcam2zoom.SetActive(false);

        gameOver = false;
        scoreText.text = "0";
        score = 0;
        timer = 0;

        hazardsCoroutine = StartCoroutine(SpawnHazards());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                ResumePause();
            }
            if (Time.timeScale == 1)
            {
                Pause();
            }

        }

         if (gameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score++;
            scoreText.text = score.ToString();

            timer = 0;
        }
    }

    private void Pause()
    {
        LeanTween.value(1, 0, 0.3f)
            .setOnUpdate(setTimeScale)
            .setIgnoreTimeScale(true);
        backgroundMenu.gameObject.SetActive(true);
    }

    private void ResumePause()
    {
        LeanTween.value(0, 1, 0.3f)
            .setOnUpdate(setTimeScale)
            .setIgnoreTimeScale(true);
        backgroundMenu.gameObject.SetActive(false);
    }

    private void setTimeScale(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = 0.02f * value;
    }

    private IEnumerator SpawnHazards()
    {
        var hazardToSpaw = Random.Range(1, maxHazardsToSpawn);
        for (int i = 0; i < hazardToSpaw; i++)
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(0f, 3f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        var timeToWait = Random.Range(0.3f, 1.8f);

        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazards();
    }

    public void GameOver()
    {
        StopCoroutine(hazardsCoroutine);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HighScorePreferenceKey, highScore);
        }

        gameOver = true;
        highScoreText.text = highScore.ToString();

        if (Time.timeScale < 1)
        {
            ResumePause();
        }

        CMVcam1.SetActive(false);
        CMVcam2zoom.SetActive(true);

        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }
    public void Enable()
    {
        gameObject.SetActive(true);

    }
}
