using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttachObject : MonoBehaviour
{
     public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;

        this.transform.position = target.transform.position;
    }
}
