using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cButtonSound : MonoBehaviour
{
    public void OnPointerEnter()
    {
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.CLICK);
    }
}
