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
            public GameObject preObj; // ���ӿ�����Ʈ�� �ȵȴ��;; �̰͵� ���ľ��ؿ�
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

        // �������� ������ ����
        Using_ItemData_Save();

        Debug.Log("Saving...");
    }

    public void Load()
    {
        Debug.Log("Loading...");
        List<ItemLoad> itemToLoad = CustomJSON.FromJson<ItemLoad>(File.ReadAllText(Application.persistentDataPath + transform.name));

        Debug.Log(itemToLoad.Count);

        // �����°� Ȯ�������ϱ� save�� itemdata�� ���� �θ�, �̹���, ����, �̸� ��� �ʿ��Ѱ� ä���� ����� ��Ե� �ɰ� ������...
        for (int i = 0; i < itemToLoad.Count; i++)
        {
            // purchase item prepab�� �����Ѵ�.
            GameObject item = Instantiate(prefab);

            // �θ� ����, ��ư ����
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

            // ��ư �̹���
            Image itemImage = item.GetComponent<Image>();
            itemImage.sprite = itemToLoad[i].itemData.itemSprite;

            // ������ ����
            Text itemDesc = item.GetComponentsInChildren<Text>()[0];
            itemDesc.text = itemToLoad[i].itemData.itemDesc;

            // ������ �̸�
            Text itemName = item.GetComponentsInChildren<Text>()[1];
            itemName.text = itemToLoad[i].itemData.itemName;

            // ������ ������
            item.GetComponent<cData>().itemdata = itemToLoad[i].itemData;
        }
    }

    void Using_ItemData_Save()
    {
        // ������� ��� ������ ����
        cClothWindow helmet = cloth_Helmet.GetComponent<cClothWindow>();
        if (helmet.preObj)
            PlayerPrefs.SetString("Using_Helmet_Name", helmet.preObj.GetComponent<cData>().itemdata.itemName);
        PlayerPrefs.SetFloat("Using_Helmet_Str", helmet.preStr);
        PlayerPrefs.SetFloat("Using_Helmet_Spell", helmet.preSpell);
        PlayerPrefs.SetFloat("Using_Helmet_Defen", helmet.preDefen);
        PlayerPrefs.SetFloat("Using_Helmet_Hp", helmet.preHp);
        PlayerPrefs.SetFloat("Using_Helmet_CoolTime", helmet.preCoolTime);

        // ������� ���� ������ ����
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
