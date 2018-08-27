using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : BaseEnemy, IRageEnemy {

    [SerializeField] private float _rageStored;

    public float StoredRage { get; set; }

    public RageMode RageController { get; set; }

    protected override void Start()
    {
        base.Start();
        StoredRage = _rageStored;
    }

    public override void Die()
    {
        base.Die();

        RageController.AddRage(StoredRage);
    }
}
