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
        // 저장된 데이터가 있으면 적용
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
