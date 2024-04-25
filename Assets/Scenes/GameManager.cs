using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public cPlayer player;

    //[Header("Common")]

    //[Header("Player")]

    //[Header("GameInfo")]

    void Awake()
    {
        instance = this;
    }
}
