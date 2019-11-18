using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveItem : MonoBehaviour
{
    public GameObject[] _itemPrefabs;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerGet();
        }
    }

    void playerGet()
    {
        int randomItem = Instantiate(_itemPrefabs[Random.Range(0,_itemPrefabs.Length)],transform.position,Quaternion.identity);
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
