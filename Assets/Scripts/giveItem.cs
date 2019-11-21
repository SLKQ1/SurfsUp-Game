using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Fragsurf.Movement;

public class giveItem : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        GameObject player = other.transform.parent.gameObject;
        SurfCharacter surfChar = player.GetComponent<SurfCharacter>();
        if (surfChar != null)
        {
            givePlayer(other);
        }
    }

    void givePlayer(Collider player)
    {
        Debug.Log("Picked up!");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        int randomItem = Random.Range(0, itemPrefabs.Length);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
