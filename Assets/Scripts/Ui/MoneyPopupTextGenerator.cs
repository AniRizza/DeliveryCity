using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPopupTextGenerator : MonoBehaviour
{
    public GameObject popupTextObject;
    
    public void ShowAddingMoneyAnimation(string text) {
        GameObject popupObject = Instantiate(popupTextObject, transform);
        popupObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }
}
