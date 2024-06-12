using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cWindowActive : MonoBehaviour
{
    public GameObject Window_UI;

    public void Cancel_Window()
    {
        if (Window_UI.activeSelf == false)
            return;

        Window_UI.SetActive(false);
    }

    public void Open_Window()
    {
        if (Window_UI.activeSelf == true)
            return;

        Window_UI.SetActive(true);
    }
}
