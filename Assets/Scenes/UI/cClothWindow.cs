using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cClothWindow : MonoBehaviour
{
    public GameObject purchaseItem;

    public void Purchase_Item(cItemData itemdata)
    {
        // purchase item prepab을 만든다.
        GameObject item = Instantiate(purchaseItem, this.transform);
        // 버튼 이미지
        Image itemImage = item.GetComponent<Image>();
        itemImage.sprite = itemdata.itemSprite;
        // 아이템 설명
        Text itemDesc = item.GetComponentsInChildren<Text>()[0];
        //itemDesc.ToString() = itemdata.itemDesc;
        // 아이템 이름
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
