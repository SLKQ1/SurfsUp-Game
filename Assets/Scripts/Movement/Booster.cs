using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fragsurf.Movement;
using Mirror;

public class Booster : NetworkBehaviour
{
    [SerializeField]
    public GameObject pickupEffect;

    /// Force impulse depends on rigit body mass
    [SerializeField]
    float velocityMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter( Collider other )
    {
        if (other.transform.parent == null)
            return;
        GameObject player = other.transform.parent.gameObject;
        Quaternion playerRotation = player.GetComponent<Transform>().rotation;
        SurfCharacter surfCharacter = player.GetComponent<SurfCharacter>();
        if (surfCharacter != null)
        {
            //pick up effect
            CollideEffect();
            //surfCharacter.ResetPosition();
            surfCharacter.moveData.velocity *= velocityMultiplier;
        }
    }

    void CollideEffect()
    {
        GameObject explosion = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(explosion, 1.5f);
    }

    private void OnTriggerStay(Collider other)
    {
    }
}
