using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    public GameObject obj;

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

    public void save(cItemData _itemData)
    {
        ItemLoad saveItem = new ItemLoad(_itemData);
        itemsToLoad.Add(saveItem);

        string json = CustomJSON.ToJson(itemsToLoad);

        File.WriteAllText(Application.persistentDataPath + transform.name, json);

        Debug.Log("Saving...");
    }

    public void Load()
    {
        Debug.Log("Loading...");
        List<ItemLoad> itemToLoad = CustomJSON.FromJson<ItemLoad>(File.ReadAllText(Application.persistentDataPath + transform.name));


        Debug.Log(itemToLoad.Count);
        Debug.Log(itemToLoad[0].itemData);
        Debug.Log(itemToLoad[1].itemData);
        // �����°� Ȯ�������ϱ� save�� itemdata�� ���� �θ�, �̹���, ����, �̸� ��� �ʿ��Ѱ� ä���� ����� ��Ե� �ɰ� ������...
        //for (int i = 0; i < itemToLoad.Count; i++)
        //{
        //    //Instantiate(itemToLoad[i]);
        //}

    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Key"))
        {
            if (PlayerPrefs.HasKey("itemKey"))
            {
                Load();
            }
        }
    }
}
