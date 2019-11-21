using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public GameObject heldItem;

    public void setItem(GameObject ob)
    {
        this.heldItem = ob;
    }

    public void UsedItem()
    {
        this.heldItem = null;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        this.heldItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (this.heldItem != null)
            {
                Instantiate(heldItem, transform.position, transform.rotation);
                UsedItem();
            }
            else
            {
                print("You don't have any items!");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("player currently holding " + heldItem);
        }
    }
}
