using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    [SerializeField] private Transform _targetTransform;

    public void Setup(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    private void Update()
    {
        if(_targetTransform != null)
            transform.position = _targetTransform.position;
    }
}
