using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveItem : MonoBehaviour
{
    public GameObject[] itemPrefabs;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            givePlayer();
        }
    }

    void givePlayer()
    {
        int randomItem = Random.Range(0, itemPrefabs.Length);
        print("Player got "+  randomItem);
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
