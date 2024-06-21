using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSKillInfo : MonoBehaviour
{
    public float damege;
    public float skillDamage;

    void Start()
    {
        damege += Random.Range(5f, 10f);
        skillDamage += Random.Range(20f, 25f);
    }
}
