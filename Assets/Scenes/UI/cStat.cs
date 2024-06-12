using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cStat : MonoBehaviour
{
    public enum StatType
    { 
        STR,
        SPELL,
        HP,
        DEFEN
    }

    public StatType statType;
    public Text text;

    private float stat;
    private float preStat;

    void Start()
    {
        Classification_StatType();
        text.text = stat.ToString();
        preStat = stat;
    }

    // Update is called once per frame
    void Update()
    {
        Classification_StatType();

        if (preStat != stat)
        {
            text.text = stat.ToString();
            preStat = stat;
        }
    }

    void Classification_StatType()
    {
        switch (statType)
        {
            case StatType.STR:
                stat = GameManager.instance.str;
                break;
            case StatType.SPELL:
                stat = GameManager.instance.spell;
                break;
            case StatType.HP:
                stat = GameManager.instance.hp;
                break;
            case StatType.DEFEN:
                stat = GameManager.instance.defen;
                break;
        }
    }
}
