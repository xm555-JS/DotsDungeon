using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cClothWindow : MonoBehaviour
{
    public GameObject purchaseItem;
    private cItemData itemData;

    private GameObject preObj;
    private float preStr;
    private float preSpell;
    private float preDefen;
    private float preHp;
    private float preCoolTime;

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
        // ������ ����Ÿ
        item.GetComponent<cData>().itemdata = itemdata;
        cItemData Data = item.GetComponent<cData>().itemdata;
        Data.str = itemdata.str;
        Data.spell = itemdata.spell;
        Data.defen = itemdata.defen;
        Data.hp = itemdata.hp;
        Data.coolTime = itemdata.coolTime;

        Debug.Log(Data);

        Button itemButton = item.GetComponent<Button>();
        itemButton.onClick.AddListener(SetButton);
    }

    void SetButton()
    {
        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        cItemData objData = obj.GetComponent<cData>().itemdata;

        if (preObj == obj)
            return;

        GameManager.instance.str += objData.str - preStr;
        GameManager.instance.spell += objData.spell - preSpell;
        GameManager.instance.defen += objData.defen - preDefen;
        GameManager.instance.hp += objData.hp - preHp;
        GameManager.instance.coolTime += objData.coolTime - preCoolTime;

        preObj = obj;
        preStr = objData.str;
        preSpell = objData.spell;
        preDefen = objData.defen;
        preHp = objData.hp;
        preCoolTime = objData.coolTime;
    }
}
