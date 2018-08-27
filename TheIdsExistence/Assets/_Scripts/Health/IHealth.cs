using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth {

    HealthManager MyHealthManager { get; set; }

    void Die();

    void Hurt();
}
