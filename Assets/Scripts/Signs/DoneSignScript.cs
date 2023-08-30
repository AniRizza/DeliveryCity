using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneSignScript : MonoBehaviour
{
    public GameObject questHitboxPrefab;
    public GameObject minimapSignPrefab;

    private Vector3 tileCenter;
    private GameObject minimapObject;

    public int money = 200;

    // Start is called before the first frame update
    void Start()
    {
        minimapObject = Instantiate(minimapSignPrefab, transform.position, minimapSignPrefab.transform.rotation, transform.parent);
        Collider[] colliders = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), 1f /* Radius */);
        foreach(var collider in colliders) {
            if (collider.tag == "Tile"){
                tileCenter = collider.gameObject.transform.position;
            }
        }
        Instantiate(questHitboxPrefab, tileCenter, questHitboxPrefab.transform.rotation, transform);
    }

    public void FinishDeliveryQuest() {
        GameObject.Find("Audio Manager").GetComponent<AudioManager>().PlaySound("QuestDone");
        GameObject.Find("Money Popup Text Holder").GetComponent<MoneyPopupTextGenerator>().ShowAddingMoneyAnimation("+ " + money);
        GameObject.Find("Money Text").GetComponent<MoneyTextScript>().UpdateMoneyAmount(money);
        Destroy(minimapObject);
        Destroy(gameObject);
    }
}
