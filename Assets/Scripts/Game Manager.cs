using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { // enum 은 열거형!
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State = GameState.Intro; // 이렇게 상태 공유!

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;
    public TMP_Text ScoreText;

    public float PlayStartTime;
    
    public int Lives = 3;
    void Awake() // Start 메소드보다 먼저 실행됨
    {
        if(Instance == null){
            Instance = this; 
            // 이렇게 하면 GameManager Instance 를 다른곳에서도 공유헤서 사용가능
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroUI.SetActive(true); // 시작하면 intro ui able 되도록!
    }

    float CalculateScore() {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore(){
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        // PlayerPrefs 는 사용자의 디스크에 데이터를 저장할 수 있게 해줌! 
        if(score > currentHighScore){
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore(){
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed(){
        if(State != GameState.Playing){
            return 6f;
        }
        float speed = 8f + (1f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 35f);
    }
    // Update is called once per frame
    void Update()
    {
        if(State == GameState.Playing){
            ScoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if(State == GameState.Dead){
            ScoreText.text = "HighScore: " + GetHighScore();
        }

        if(State == GameState.Intro && Input.GetKey(KeyCode.Space)) { 
            // space 누르면 게임 상태 바꿔주기!
            State = GameState.Playing;
            IntroUI.SetActive(false);

            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }
        if(State == GameState.Playing && Lives == 0){
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            SaveHighScore();
            State = GameState.Dead;
        }
        if(State == GameState.Dead && Input.GetKey(KeyCode.Space)){
            SceneManager.LoadScene("main"); // sceneManager 를 사용해서 scene 을 다시 로드 시킴!
        }
    }
}
