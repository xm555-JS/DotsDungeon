using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cClothButton : MonoBehaviour
{
    public GameObject helmets;
    public GameObject Armor;

    public void Click_Cloth_Helmet_Button()
    {
        if (helmets.activeSelf)
            return;

        Armor.SetActive(false);
        helmets.SetActive(true);
    }

    public void Click_Cloth_Armor_Button()
    {
        if (Armor.activeSelf)
            return;

        Armor.SetActive(true);
        helmets.SetActive(false);
    }
}
