using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cClothWindow : MonoBehaviour
{
    public GameObject purchaseItem;
    private cItemData itemData;

    public void Purchase_Item(cItemData itemdata)
    {
        itemData = itemdata;

        // purchase item prepab을 복제한다.
        GameObject item = Instantiate(purchaseItem, this.transform);
        // 버튼 이미지
        Image itemImage = item.GetComponent<Image>();
        itemImage.sprite = itemdata.itemSprite;
        // 아이템 설명
        Text itemDesc = item.GetComponentsInChildren<Text>()[0];
        itemDesc.text = itemdata.itemDesc;
        // 아이템 이름
        Text itemName = item.GetComponentsInChildren<Text>()[1];
        itemName.text = itemdata.itemName;

        Button itemButton = item.GetComponent<Button>();
        itemButton.onClick.AddListener(SetButton);
    }

    void SetButton()
    {
        GameManager.instance.str += itemData.str;
        GameManager.instance.spell += itemData.spell;
        GameManager.instance.defen += itemData.defen;
        GameManager.instance.hp += itemData.hp;
        GameManager.instance.coolTime += itemData.coolTime;
    }
}
