using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cClothWindow : MonoBehaviour
{
    public GameObject purchaseItem;

    public void Purchase_Item(cItemData itemdata)
    {
        // purchase item prepab�� �����.
        GameObject item = Instantiate(purchaseItem, this.transform);
        // ��ư �̹���
        Image itemImage = item.GetComponent<Image>();
        itemImage.sprite = itemdata.itemSprite;
        // ������ ����
        Text itemDesc = item.GetComponentsInChildren<Text>()[0];
        //itemDesc.ToString() = itemdata.itemDesc;
        // ������ �̸�
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
