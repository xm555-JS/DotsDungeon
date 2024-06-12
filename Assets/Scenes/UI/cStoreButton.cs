using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cStoreButton : MonoBehaviour
{
    public GameObject helmets;
    public GameObject Armor;

    public void Click_Store_Helmet_Button()
    {
        if (helmets.activeSelf)
            return;

        Armor.SetActive(false);
        helmets.SetActive(true);
    }

    public void Click_Store_Armor_Button()
    {
        if (Armor.activeSelf)
            return;

        Armor.SetActive(true);
        helmets.SetActive(false);
    }
}
