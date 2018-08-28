using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGod : MonoBehaviour
{
    public enum GameMode { RAGE, COMPASSION, GREED, NONE }

    public GameMode _activeGamemode;

    [SerializeField] private Canvas _mainCanvasRef;
    public static Canvas _mainCanvas;

    [Header("Other")]
    public CameraAdjuster _camAdjuster;

    private EventController _eventCtrl;
    private bool _won;

    private bool _isFirstSceneLoad = true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;

        if(GameObject.FindGameObjectsWithTag("GameGod").Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(_isFirstSceneLoad)
        {
            _isFirstSceneLoad = false;
            return;
        }

        if(scene.buildIndex == 0)
        {
            _camAdjuster.gameObject.SetActive(true);

            if(_won)
            {
                _camAdjuster.Adjust();
                StartCoroutine(_eventCtrl.ActivateNextEvent());
            }
            else
                _eventCtrl.ReactivateEvent();
        }
        else
        {
            _camAdjuster.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
    }

    private void Start()
    {
        _eventCtrl = GetComponent<EventController>();
        _mainCanvas = _mainCanvasRef;

        _camAdjuster.Adjust();
    }

    public void ActivateMode(GameMode gameMode)
    {
        _activeGamemode = gameMode;
        _eventCtrl.DeactiveEvent(false);

        switch(gameMode)
        {
            case GameMode.RAGE:
                SceneManager.LoadScene("SCN_Mode_Rage");
                break;

            case GameMode.COMPASSION:
                SceneManager.LoadScene("SCN_Mode_Compassion");
                break;

            case GameMode.GREED:
                SceneManager.LoadScene("SCN_Mode_Greed");
                break;

            default:
                print("Setup mode first.");
                return;
        }
    }

    public void EndMode(bool won)
    {
        _won = won;

        if(!won)
            _activeGamemode = GameMode.NONE;

        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
