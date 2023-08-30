using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTextScript : MonoBehaviour
{
    public int money = 100;

    public void UpdateMoneyAmount(int amount){
        money += amount;
        GetComponent<TMPro.TextMeshProUGUI>().text = "" + money;
    }
}
