using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cPurchase : MonoBehaviour
{
    public GameObject clothItem;

    GameObject prefab;
    cItemData itemData;
    Button btn;
    int index;
    bool isDecide = false;
    bool answer = false;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("UI_Prefabs/PurchaseCheckBox");

        if (this.name == "Item_Helmet")
            index = PlayerPrefs.GetInt("Index_Helmet");
        else if (this.name == "Item_Armor")
            index = PlayerPrefs.GetInt("Index_Armor");

        int a = 0;
    }

    public void Purchase_Item(cItemData itemdata)
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        btn = obj.GetComponent<Button>();

        // 데이터 저장
        if (itemdata.itmeType == "Helmet")
        {
            PlayerPrefs.SetString("PurchaseItem_Helmet" + index, obj.name);
            index++;
            PlayerPrefs.SetInt("Index_Helmet", index);
        }
        else if (itemdata.itmeType == "Armor")
        {
            PlayerPrefs.SetString("PurchaseItem_Armor" + index, obj.name);
            index++;
            PlayerPrefs.SetInt("Index_Armor", index);
        }

        itemData = itemdata;
        int money = GameManager.instance.money;
        int price = itemdata.price;
        if (money >= price)
        {
            Instantiate(prefab, this.transform);
            btn.interactable = false;
            StartCoroutine("waitAnswer");
        }
        else
        {
            Debug.Log("돈이 부족합니다.");

            // audio
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.DENIED);
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

        // audio
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.BUY);
    }

    public void CheckBox_No()
    {
        isDecide = true;
        answer = false;
        btn.interactable = true;

        // audio
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.DENIED);
    }
}
