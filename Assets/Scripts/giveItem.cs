using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Fragsurf.Movement;

public class giveItem : MonoBehaviour
{
    public playerInventory pi;
    public GameObject[] itemPrefabs;
    public GameObject pickupEffect;


    void OnTriggerEnter(Collider other)
    {
        GameObject player = other.transform.parent.gameObject;
        SurfCharacter surfChar = player.GetComponent<SurfCharacter>();
        if (surfChar != null)
        {
            givePlayer();
        }
    }

    void givePlayer()
    {
        GameObject explosion = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(explosion, 1.5f);
        GameObject itemtogive = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        pi.setItem(itemtogive);
        Debug.Log("setting players item to have " + itemtogive.name);
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
