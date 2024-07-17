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

    public void Cancel_Window_Ver_Scale()
    {
        if (Window_UI.activeSelf == false)
            return;

        Window_UI.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void Open_Window()
    {
        if (Window_UI.activeSelf == true)
            return;

        Window_UI.SetActive(true);
    }

    public void Open_Window_Ver_Scale()
    {
        if (Window_UI.activeSelf == false)
            return;

        Window_UI.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
