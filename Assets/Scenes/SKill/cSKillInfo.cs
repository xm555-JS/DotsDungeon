using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSKillInfo : MonoBehaviour
{
    public float damege;
    public float skillDamage;

    void Start()
    {
        damege += Random.Range(0f, 5f);
        skillDamage += Random.Range(10f, 15f);
    }
}
