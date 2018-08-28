using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    [SerializeField] private string _sceneName;
    [SerializeField] private GameGod.GameMode _gameMode;
    private GameGod _gg;
    [HideInInspector] public bool _activated;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _gg = GameObject.FindGameObjectWithTag("GameGod").GetComponent<GameGod>();
    }

    private void OnMouseDown()
    {
        print("Shit.");
        _activated = true;
        _gg.ActivateMode(_gameMode);
    }
}
