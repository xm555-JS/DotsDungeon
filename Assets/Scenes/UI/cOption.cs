using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cOption : MonoBehaviour
{
    public GameObject option_Window;

    public void Open_Option_Window()
    {
        option_Window.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Close_Option_Window()
    {
        option_Window.transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
