using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGod : MonoBehaviour
{
    public enum GameMode { RAGE, NONE }

    private GameMode _activeGamemode;

    [SerializeField] private Canvas _mainCanvasRef;
    public static Canvas _mainCanvas;

    [Header("Other")]
    [SerializeField] private CameraAdjuster _camAdjuster;

    private EventController _eventCtrl;
    private bool _won;

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
        if(scene.buildIndex == 0)
        {
            _camAdjuster.gameObject.SetActive(true);

            if(_won)
            {
                _camAdjuster.Adjust();
                _eventCtrl.ActivateNextEvent();
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
        print("Mode Active");
        _eventCtrl.DeactiveEvent(false);

        switch(gameMode)
        {
            case GameMode.RAGE:
                SceneManager.LoadScene("SCN_Mode_Rage");
                break;
            default:
                print("Setup mode first.");
                return;
        }
    }

    public void EndMode(bool won)
    {
        _won = won;
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
