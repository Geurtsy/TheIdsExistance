using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageMode : MonoBehaviour {

    [Header("Spawning")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _spawnAmount;

    [SerializeField] private int _maxSpawns;

    [Header("Rage")]
    [SerializeField] private Image _rageMeter;
    [SerializeField] private GameObject _ragePT;
    [SerializeField] private float _rageDepletionRate;
    [SerializeField] private float _currentRage;
    [SerializeField] private float _rageGoal;

    private float CurrentRage
    {
        get
        {
            return _currentRage;
        }

        set
        {
            if(_currentRage > _rageGoal)
                Rage();
            else
                _currentRage = value < 0 ? 0 : value;
        }
    }

    private float RagePercentage
    {
        get
        {
            return (_currentRage / _rageGoal);
        }
    }

    private int _currentSpawns;
    private GameGod _gg;

    [SerializeField] private Rect _boundaries;

    private IEnumerator Start()
    {
        _gg = GameObject.FindGameObjectWithTag("GameGod").GetComponent<GameGod>();
        _currentRage = 0;
        _currentSpawns = 0;
        _ragePT.SetActive(false);

        while(_currentSpawns < _maxSpawns)
        {
            Spawn();
            _currentSpawns++;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void Update()
    {
        AddRage(-Time.deltaTime * _rageDepletionRate);
    }

    public void AddRage(float rage)
    {
        CurrentRage += rage;
        _rageMeter.fillAmount = RagePercentage;
    }

    private void Rage()
    {
        _ragePT.SetActive(true);
        _gg.EndMode(false);
    }

    private void Spawn()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(_boundaries.xMin, _boundaries.xMax), Random.Range(_boundaries.yMin, _boundaries.yMax));
        IRageEnemy enemy = Instantiate(_enemyPrefab, spawnPoint, transform.rotation).GetComponent<IRageEnemy>();
        enemy.RageController = this;
    }
}
