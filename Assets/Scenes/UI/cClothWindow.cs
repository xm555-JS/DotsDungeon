using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cClothWindow : MonoBehaviour
{
    public GameObject purchaseItem;
    //public List<cItemData> saveItemData = new List<cItemData>();
    public List<cItemData> saveItemData_Helmet = new List<cItemData>();
    public List<cItemData> saveItemData_Armor = new List<cItemData>();
    private cItemData itemData;

    public GameObject preObj;
    public float preStr;
    public float preSpell;
    public float preDefen;
    public float preHp;
    public float preCoolTime;

    public void Initialize()
    {
        // 저장된 데이터가 있으면 적용
        if (PlayerPrefs.HasKey("Key"))
        {
            if (PlayerPrefs.HasKey("itemKey"))
            {
                string usingItemName_Helmet = PlayerPrefs.GetString("Using_Helmet_Name");
                string usingItemName_Armor = PlayerPrefs.GetString("Using_Armor_Name");

                GameObject[] item = GameObject.FindGameObjectsWithTag("PurchaseItem");
                foreach (GameObject obj in item)
                {
                    if (obj.GetComponent<cData>().itemdata.itmeType == "Helmet")
                    {
                        saveItemData_Helmet.Add(obj.GetComponent<cData>().itemdata);

                        if (obj.GetComponent<cData>().itemdata.itemName == usingItemName_Helmet)
                        {
                            cItemData objData = obj.GetComponent<cData>().itemdata;
                            preObj = obj;
                        }
                    }
                    else if (obj.GetComponent<cData>().itemdata.itmeType == "Armor")
                    {
                        saveItemData_Armor.Add(obj.GetComponent<cData>().itemdata);

                        if (obj.GetComponent<cData>().itemdata.itemName == usingItemName_Armor)
                        {
                            cItemData objData = obj.GetComponent<cData>().itemdata;
                            preObj = obj;
                        }
                    }
                }

                if (this.name == "Cloth_Item_Helmet")
                {
                    preStr = PlayerPrefs.GetFloat("Using_Helmet_Str");
                    preSpell = PlayerPrefs.GetFloat("Using_Helmet_Spell");
                    preDefen = PlayerPrefs.GetFloat("Using_Helmet_Defen");
                    preHp = PlayerPrefs.GetFloat("Using_Helmet_Hp");
                    preCoolTime = PlayerPrefs.GetFloat("Using_Helmet_CoolTime");
                }
                else if (this.name == "Cloth_Item_Armor")
                {
                    preStr = PlayerPrefs.GetFloat("Using_Armor_Str");
                    preSpell = PlayerPrefs.GetFloat("Using_Armor_Spell");
                    preDefen = PlayerPrefs.GetFloat("Using_Armor_Defen");
                    preHp = PlayerPrefs.GetFloat("Using_Armor_Hp");
                    preCoolTime = PlayerPrefs.GetFloat("Using_Armor_CoolTime");
                }

                if (!preObj)
                    preObj = purchaseItem;
            }
        }
        else
            preObj = purchaseItem;
    }

    public void Purchase_Item(cItemData itemdata)
    {
        itemData = itemdata;

        // 저장할 아이템이 생겼다는 표시
        PlayerPrefs.SetInt("itemKey", 1);

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
        // 아이템 데이타
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

        if (item.GetComponent<cData>().itemdata.itmeType == "Helmet")
            saveItemData_Helmet.Add(itemdata);
        else if (item.GetComponent<cData>().itemdata.itmeType == "Armor")
            saveItemData_Armor.Add(itemdata);
    }

    public void SetButton()
    {
        // audio
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.EQUIP);

        GameObject obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        cItemData objData = obj.GetComponent<cData>().itemdata;
        if (preObj.GetComponent<cData>().itemdata.itemName == objData.itemName)
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
