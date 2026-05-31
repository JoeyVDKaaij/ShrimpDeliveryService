using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private float timeUntilRoundEnds = 180;
    [SerializeField] private int goodPackageScore = 3;
    [SerializeField] private int badPackageScore = 1;

    private float _timer;
    public float Timer {  get { return _timer; } }

    private int _score;
    public int Score {  get { return _score; } }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // foreach (Display display in Display.displays)
        // {
        //     display.Activate(1280, 720, Screen.resolutions[0].refreshRateRatio);
        //     print(display);
        // }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
        _timer = timeUntilRoundEnds;
    }

    // Update is called once per frame
    void Update()
    {
        // Ignore logic if scene is not gameplay scene
        if (SceneManager.GetActiveScene().buildIndex != 1) return;
        
        // Count down timer
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            print("Game ended!!");
            _timer = timeUntilRoundEnds;
            
            // Game Over screen
            SceneManager.LoadScene(2);
        }
    }

    public void AddScore(bool pCorrectPackage = false)
    {
        if (pCorrectPackage) _score += goodPackageScore;
        else _score += badPackageScore;
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void ResetTimer()
    {
        _timer = timeUntilRoundEnds;
    }
}
