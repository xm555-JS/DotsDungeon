using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMenuButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject other_Menu;

    public void Menu_Active()
    {
        if (menu.activeSelf)
            menu.SetActive(false);
        else
        {
            if (other_Menu.activeSelf)
                other_Menu.SetActive(false);

            menu.SetActive(true);
        }
            
    }
}
