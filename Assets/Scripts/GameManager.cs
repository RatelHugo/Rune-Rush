using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] public GameObject canvas;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public GameObject restartButton;
    public bool isTriggerRune = false;
    public bool isValidationRune = false;
    public bool isDead = false;
    public bool isDeadEnd = false;
    public bool isDestroy = false;
    public float speedPlayer = 6f;
    public int score = 0;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = Instance;

        Screen.orientation = ScreenOrientation.Portrait;

        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        scoreText.color = Color.white;
    }

    public void TriggerRune()
    {
        canvas.SetActive(true);
        isTriggerRune = true;
        isValidationRune = false;
    }

    private void Update()
    {
        scoreText.text = score.ToString("D3");
        RestartGame();
    }

    private void RestartGame()
    {
        if (isDeadEnd)
        {
            restartButton.SetActive(true);
        }
    }
}
