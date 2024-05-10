using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSKillInfo : MonoBehaviour
{
    public float damege;

    void Start()
    {
        damege += Random.Range(-3f, 10f);
    }
}
