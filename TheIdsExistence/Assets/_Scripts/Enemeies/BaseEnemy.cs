using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IHealth {

    [Header("Health")]
    [SerializeField] private HealthManager _myHealthManager;
    [SerializeField] private GameObject _hurtPT;
    [SerializeField] private GameObject _deathPT;

    public HealthManager MyHealthManager
    {
        get { return _myHealthManager;  }
        set { _myHealthManager = value; }
    }

    protected virtual void Start()
    {
        _myHealthManager.SetupHealth(this, transform);
    }

    public void Hurt()
    {
        PlayEffect(_hurtPT);
    }

    public virtual void Die()
    {
        PlayEffect(_deathPT);

        Destroy(gameObject);
    }

    private void PlayEffect(GameObject effect)
    {
        if(effect != null)
            Instantiate(effect, transform.position, transform.rotation);
    }
}
