using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cStoreItem : MonoBehaviour
{
    public cItemData itemData;

    private Image buttonImage;
    private Button btn;
    private Text text;
    private Text priceText;
    private Text name;

    // Start is called before the first frame update
    void Start()
    { 
        // ��ư �̹��� ����
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = itemData.itemSprite;

        // ������ ����
        text = GetComponentsInChildren<Text>()[0];
        text.text = itemData.itemDesc;
        // ������ ����
        priceText = GetComponentsInChildren<Text>()[1];
        priceText.text = itemData.itemPrice;
        // ������ �̸�
        name = GetComponentsInChildren<Text>()[2];
        name.text = itemData.itemName;

        btn = GetComponent<Button>();
        if (itemData.itmeType == "Helmet")
        {
            for (int i = 0; i < 4; i++)
            {
                string purchaseItem = PlayerPrefs.GetString("PurchaseItem_Helmet" + i);

                if (purchaseItem == this.gameObject.name)
                    btn.interactable = false;
            }
        }
        else if (itemData.itmeType == "Armor")
        {
            for (int i = 0; i < 4; i++)
            {
                string purchaseItem = PlayerPrefs.GetString("PurchaseItem_Armor" + i);

                if (purchaseItem == this.gameObject.name)
                    btn.interactable = false;
            }
        }
    }
}
