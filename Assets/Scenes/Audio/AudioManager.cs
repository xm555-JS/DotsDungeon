using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    public enum Bgm { START, LOBBY, STAGE, BOSS };

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channellIndex;

    public enum Sfx { BUY, CLICK, CONFIRM, DENIED, EQUIP, OPENWINDOW, ATTACK, DEAD, STEP, HEAL, FIRE, FIREBALL, ICE, POISION, HIT };

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        if (bgmPlayer.isPlaying)
            StartCoroutine("ChangeBgm");

        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.Play();
    }

    IEnumerator ChangeBgm()
    {
        float time = 0f;
        float fadeTime = 1f;
        while (bgmPlayer.volume <= 0f)
        {
            time += Time.deltaTime;
            float volume = Mathf.Lerp(0f, bgmVolume, time / fadeTime);
            bgmPlayer.volume = volume;
            yield return null;
        }
    }

    public void PlayerSfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channellIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channellIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void SetBgmVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = volume;
        }
    }
}
