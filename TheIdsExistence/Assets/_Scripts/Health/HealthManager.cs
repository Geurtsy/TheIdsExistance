using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthManager {

    [Header("Health Settings")]
    public float _maxHealth;
    [SerializeField] private float _currentHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }

        set
        {
            if(value < 0)
            {
                _myObject.Die();
                return;
            }

            if(value < _currentHealth)
                _myObject.Hurt();

            _currentHealth = value;
            _healthBar.fillAmount = CurrentHealthPercentage;
        }
    }

    [Header("Visualisation")]
    public Image _healthBar;

    public float CurrentHealthPercentage
    {
        get { return (_currentHealth / _maxHealth); }
    }

    private IHealth _myObject;

    public void SetupHealth(IHealth health, Transform targetTransform)
    {
        _myObject = health;

        // Health Bar Setup.
        _healthBar.fillAmount = CurrentHealthPercentage;
    }

}
