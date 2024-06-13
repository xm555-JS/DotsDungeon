using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCheckBox : MonoBehaviour
{
    GameObject checkBox;
    private void Start()
    {
        checkBox = GameObject.Find("PurchaseCheckBox(Clone)");
    }
    public void Purchase_Yes()
    {
        checkBox.GetComponentInParent<cPurchase>().CheckBox_Yes();
        Destroy(checkBox);
    }
    public void Purchase_No()
    {
        checkBox.GetComponentInParent<cPurchase>().CheckBox_No();
        Destroy(checkBox);
    }
}
