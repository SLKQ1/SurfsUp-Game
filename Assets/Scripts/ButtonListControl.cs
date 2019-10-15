using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            // making button and making visiable
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            // setting button text 
            button.GetComponent<ButtonListButton>().SetText("Button #" + i);
            // setting button position
            button.transform.SetParent(buttonTemplate.transform.parent, false); 
        }
    }

    public void ButtonClicked(string myTextString)
    {
        Debug.Log(myTextString);
    }

    // function to generate buttons
    void GenButtons()
    {

    }

}
