using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cClothWindow : MonoBehaviour
{
    public GameObject purchaseItem;
    public List<cItemData> saveItemData = new List<cItemData>();
    private cItemData itemData;

    public GameObject preObj;
    public float preStr;
    public float preSpell;
    public float preDefen;
    public float preHp;
    public float preCoolTime;

    void Start()
    {
        // ����� �����Ͱ� ������ ����
        if (PlayerPrefs.HasKey("Key"))
        {
            if (PlayerPrefs.HasKey("itemKey"))
            {
                if (!preObj)
                    return;

                if (this.name == "Cloth_Item_Helmet")
                {
                    preObj.name = PlayerPrefs.GetString("Using_Helmet_Name");
                    preStr = PlayerPrefs.GetFloat("Using_Helmet_Str");
                    preSpell = PlayerPrefs.GetFloat("Using_Helmet_Spell");
                    preDefen = PlayerPrefs.GetFloat("Using_Helmet_Defen");
                    preHp = PlayerPrefs.GetFloat("Using_Helmet_Hp");
                    preCoolTime = PlayerPrefs.GetFloat("Using_Helmet_CoolTime");
                }
                else if (this.name == "Cloth_Item_Armor")
                {
                    preObj.name = PlayerPrefs.GetString("Using_Armor_Name");
                    preStr = PlayerPrefs.GetFloat("Using_Armor_Str");
                    preSpell = PlayerPrefs.GetFloat("Using_Armor_Spell");
                    preDefen = PlayerPrefs.GetFloat("Using_Armor_Defen");
                    preHp = PlayerPrefs.GetFloat("Using_Armor_Hp");
                    preCoolTime = PlayerPrefs.GetFloat("Using_Armor_CoolTime");
                }
            }
        }
    }

    public void Purchase_Item(cItemData itemdata)
    {
        itemData = itemdata;

        // ������ �������� ����ٴ� ǥ��
        PlayerPrefs.SetInt("itemKey", 1);

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

        saveItemData.Add(itemdata);
    }

    public void SetButton()
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
