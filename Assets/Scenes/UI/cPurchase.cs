using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPurchase : MonoBehaviour
{
    public GameObject clothItem;

    GameObject prefab;
    cItemData itemData;
    bool isDecide = false;
    bool answer = false;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("UI_Prefabs/PurchaseCheckBox");
    }

    public void Purchase_Item(cItemData itemdata)
    {
        itemData = itemdata;
        int money = GameManager.instance.money;
        int price = itemdata.price;
        if (money >= price)
        {
            Instantiate(prefab, this.transform);
            StartCoroutine("waitAnswer");
            //GameManager.instance.money -= price;
            //clothItem.GetComponent<cClothWindow>().Purchase_Item(itemdata);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    IEnumerator waitAnswer()
    {
        yield return new WaitUntil(Decide);

        int money = GameManager.instance.money;
        int price = itemData.price;
        if (answer)
        {
            GameManager.instance.money -= price;
            clothItem.GetComponent<cClothWindow>().Purchase_Item(itemData);
            isDecide = false;
            answer = false;
        }
    }

    bool Decide()
    {
        return answer;
    }

    public void CheckBox_Yes()
    {
        isDecide = true;
        answer = true;
    }

    public void CheckBox_No()
    {
        isDecide = true;
        answer = false;
    }

    
//        if (itemData.itmeType == "Amror")
//        {
//            GameObject obj = GameObject.Find("Cloth_Item_Armor");
//}
            

}
