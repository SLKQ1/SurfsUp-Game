using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    /// Force impulse depends on rigit body mass
    public float impulseForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter( Collider collider )
    {
        Debug.Log("Collision: " + collider.gameObject );

        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        // TODO: this is very dangerous, keep collider & rigit body consistant on the same level to avoid parent / child scanning

        if( body != null ) {}

        else if( collider.gameObject.GetComponentInParent<Rigidbody>() )
            body = collider.gameObject.GetComponentInParent<Rigidbody>();

        else if( collider.gameObject.GetComponentInChildren<Rigidbody>() )
            body = collider.gameObject.GetComponentInChildren<Rigidbody>();

        else body = null;

        Debug.Log("Body: " + body );

        body?.AddForce( transform.forward * impulseForce * Time.deltaTime, ForceMode.Impulse );
    }
}
