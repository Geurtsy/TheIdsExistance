using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask _attackableLayers;

    [SerializeField] private float _attackPoints;
    [SerializeField] private float _attackRadius;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
	}

    private void Attack()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, _attackRadius, _attackableLayers);

        if(collidersInRange != null)
        {
            foreach(Collider2D col in collidersInRange)
            {
                IHealth health = col.GetComponent<IHealth>();

                if(health != null)
                    health.MyHealthManager.CurrentHealth -= _attackPoints;
            }
        }
    }
}
