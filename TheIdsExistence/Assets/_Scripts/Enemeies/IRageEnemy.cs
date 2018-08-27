using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRageEnemy {

    RageMode RageController { get; set; }
    float StoredRage { get; set; }

}
