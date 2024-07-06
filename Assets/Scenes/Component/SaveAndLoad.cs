using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    public GameObject prefab;
    public GameObject cloth_Helmet;
    public GameObject cloth_Armor;

    [System.Serializable]
    public class ItemLoad
    {
        public cItemData itemData;

        public ItemLoad(cItemData _itemData)
        {
            itemData = _itemData;
        }

        public class DataLoad
        {
            public GameObject preObj; // 게임오브젝트는 안된대용;; 이것도 고쳐야해용
            public float preStr;
            public float preSpell;
            public float preDefen;
            public float preHp;
            public float preCoolTime;

            public DataLoad(GameObject _obj, float _preStr, float _preSpell, float _preDefen, float _preHp, float _preCoolTime)
            {
                preObj = _obj;
                preStr = _preStr;
                preSpell = _preSpell;
                preDefen = _preDefen;
                preHp = _preHp;
                preCoolTime = _preCoolTime;
            }
        }
    }

    List<ItemLoad> itemsToLoad = new List<ItemLoad>();

    public void save()
    {
        foreach (cItemData data in cloth_Helmet.GetComponent<cClothWindow>().saveItemData_Helmet)
        {
            ItemLoad saveItem = new ItemLoad(data);
            itemsToLoad.Add(saveItem);
        }
        foreach (cItemData data in cloth_Armor.GetComponent<cClothWindow>().saveItemData_Armor)
        {
            ItemLoad saveItem = new ItemLoad(data);
            itemsToLoad.Add(saveItem);
        }

        string json = CustomJSON.ToJson(itemsToLoad);

        File.WriteAllText(Application.persistentDataPath + transform.name, json);

        // 착용중인 아이템 저장
        Using_ItemData_Save();

        Debug.Log("Saving...");
    }

    public void Load()
    {
        Debug.Log("Loading...");
        List<ItemLoad> itemToLoad = CustomJSON.FromJson<ItemLoad>(File.ReadAllText(Application.persistentDataPath + transform.name));

        Debug.Log(itemToLoad.Count);

        // 들어오는거 확인했으니까 save된 itemdata로 각각 부모, 이미지, 설명, 이름 등등 필요한거 채워서 만들면 어떻게든 될거 같은데...
        for (int i = 0; i < itemToLoad.Count; i++)
        {
            // purchase item prepab을 복제한다.
            GameObject item = Instantiate(prefab);

            // 부모 설정, 버튼 셋팅
            if (itemToLoad[i].itemData.itmeType == "Armor")
            {
                item.transform.SetParent(cloth_Armor.transform);

                Button itemButton = item.GetComponent<Button>();
                itemButton.onClick.AddListener(cloth_Armor.GetComponent<cClothWindow>().SetButton);
            }
            else if (itemToLoad[i].itemData.itmeType == "Helmet")
            {
                item.transform.SetParent(cloth_Helmet.transform);

                Button itemButton = item.GetComponent<Button>();
                itemButton.onClick.AddListener(cloth_Helmet.GetComponent<cClothWindow>().SetButton);
            }

            // 버튼 이미지
            Image itemImage = item.GetComponent<Image>();
            itemImage.sprite = itemToLoad[i].itemData.itemSprite;

            // 아이템 설명
            Text itemDesc = item.GetComponentsInChildren<Text>()[0];
            itemDesc.text = itemToLoad[i].itemData.itemDesc;

            // 아이템 이름
            Text itemName = item.GetComponentsInChildren<Text>()[1];
            itemName.text = itemToLoad[i].itemData.itemName;

            // 아이템 데이터
            item.GetComponent<cData>().itemdata = itemToLoad[i].itemData;
        }
    }

    void Using_ItemData_Save()
    {
        // 사용중인 헬멧 데이터 저장
        cClothWindow helmet = cloth_Helmet.GetComponent<cClothWindow>();
        if (helmet.preObj)
            PlayerPrefs.SetString("Using_Helmet_Name", helmet.preObj.GetComponent<cData>().itemdata.itemName);
        PlayerPrefs.SetFloat("Using_Helmet_Str", helmet.preStr);
        PlayerPrefs.SetFloat("Using_Helmet_Spell", helmet.preSpell);
        PlayerPrefs.SetFloat("Using_Helmet_Defen", helmet.preDefen);
        PlayerPrefs.SetFloat("Using_Helmet_Hp", helmet.preHp);
        PlayerPrefs.SetFloat("Using_Helmet_CoolTime", helmet.preCoolTime);

        // 사용중인 갑옷 데이터 저장
        cClothWindow armor = cloth_Armor.GetComponent<cClothWindow>();
        if (armor.preObj)
            PlayerPrefs.SetString("Using_Armor_Name", armor.preObj.GetComponent<cData>().itemdata.itemName);
        PlayerPrefs.SetFloat("Using_Armor_Str", armor.preStr);
        PlayerPrefs.SetFloat("Using_Armor_Spell", armor.preSpell);
        PlayerPrefs.SetFloat("Using_Armor_Defen", armor.preDefen);
        PlayerPrefs.SetFloat("Using_Armor_Hp", armor.preHp);
        PlayerPrefs.SetFloat("Using_Armor_CoolTime", armor.preCoolTime);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Key"))
        {
            if (PlayerPrefs.HasKey("itemKey"))
            {
                Load();
                cloth_Helmet.transform.parent.gameObject.SetActive(true);
                cloth_Helmet.SetActive(true);
                cloth_Armor.SetActive(true);
                cloth_Helmet.GetComponent<cClothWindow>().Initialize();
                cloth_Armor.GetComponent<cClothWindow>().Initialize();
                cloth_Helmet.SetActive(false);
                cloth_Helmet.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            cloth_Helmet.GetComponent<cClothWindow>().Initialize();
            cloth_Armor.GetComponent<cClothWindow>().Initialize();
        }
    }
}
