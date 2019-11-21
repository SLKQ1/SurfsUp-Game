using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private ButtonListControl lobbyNum;

    // string to hold text of button 
    private string myTextString; 

    // function to set button text
    public void SetText(string textString)
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick); 
        myTextString = textString; 
        myText.text = textString; 
    }

    // function to provide logic on a click 
    public void OnClick()
    {
        lobbyNum.ButtonClicked(myTextString); 
    }
}
