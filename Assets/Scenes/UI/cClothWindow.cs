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

        // purchase item prepab�� �����Ѵ�.
        GameObject item = Instantiate(purchaseItem, this.transform);
        // ��ư �̹���
        Image itemImage = item.GetComponent<Image>();
        itemImage.sprite = itemdata.itemSprite;
        // ������ ����
        Text itemDesc = item.GetComponentsInChildren<Text>()[0];
        itemDesc.text = itemdata.itemDesc;
        // ������ �̸�
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
