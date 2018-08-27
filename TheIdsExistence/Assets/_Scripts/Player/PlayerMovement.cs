using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rb2;

    private void Start()
    {
        _rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(horizontalInput != 0 || verticalInput != 0)
        {
            Move(new Vector2(horizontalInput, verticalInput));
        }
    }

    private void Move(Vector2 input)
    {
        _rb2.AddForce(input * _speed, ForceMode2D.Force);

        _rb2.velocity = Vector2.ClampMagnitude(_rb2.velocity, _maxSpeed);
    }
}
