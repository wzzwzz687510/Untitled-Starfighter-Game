using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Reference")]
    public AudioSource bgm;
    public AudioSource se;
    public AudioSource machineGun;
    public AudioSource mininglaser;
    public AudioSource engine;

    [Header("Audio Clip")]
    public AudioClip weaponReleaseClip;
    public AudioClip weaponReloadClip;
    public AudioClip weaponReloadCompleteClip;
    public AudioClip upgradeClip;
    public AudioClip explosionClip;
    public AudioClip missonAppearClip;
    public AudioClip missonCompleteClip;
    public AudioClip battleNoDrums;
    public AudioClip battleWithDrums;
    public AudioClip victoryClip;
    public AudioClip gameoverClip;
    public AudioClip engineBoostClip;
    public AudioClip engineLoopClip;
    public AudioClip[] beepClips;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void SwitchBGM(AudioClip clip)
    {
        bgm.Stop();
        bgm.clip = clip;
        bgm.Play();
    }

    public void PlayBattleBGM()
    {
        SwitchBGM(battleNoDrums);
    }

    public void BattleUpgrade()
    {
        float time = bgm.time;
        SwitchBGM(battleWithDrums);
        bgm.time = time;
    }

    public void PlayVictoryBGM()
    {
        engine.Stop();
        machineGun.Stop();
        mininglaser.Stop();
        SwitchBGM(victoryClip);
    }

    public void PlayGameoverClip()
    {
        StopAllAudioSource();
        se.PlayOneShot(gameoverClip);
    }


    public void PlayBeepClip()
    {
        se.PlayOneShot(beepClips[Random.Range(0, beepClips.Length)]);
    }

    public void PlayUpgradeClip()
    {
        se.PlayOneShot(upgradeClip);
    }

    public void PlayExplosionClip()
    {
        se.PlayOneShot(explosionClip);
    }

    public void PlayMissionAppearClip()
    {
        se.PlayOneShot(missonAppearClip);
    }

    public void PlayMissionCompleteClip()
    {
        se.PlayOneShot(missonCompleteClip);
    }

    public void PlayReloadClip()
    {
        se.PlayOneShot(weaponReloadClip);
    }

    public void PlayReloadCompleteClip()
    {
        se.PlayOneShot(weaponReloadCompleteClip);
    }

    public void PlayWeaponReleaseClip()
    {
        se.PlayOneShot(weaponReloadClip);
    }

    public void SetMGClip(bool bl)
    {
        if (bl && !machineGun.isPlaying) machineGun.Play();
        else if(!bl && machineGun.isPlaying) {
            machineGun.Stop();
            se.PlayOneShot(weaponReleaseClip);
        }
    }

    public void SetMLClip(bool bl)
    {
        if (bl && !mininglaser.isPlaying) {
            mininglaser.Play();
        }
        else if (!bl && mininglaser.isPlaying) mininglaser.Stop();
    }

    public void SetEngine(bool bl)
    {
        if (bl && !engine.isPlaying) engine.Play();
        else if(!bl && engine.isPlaying)engine.Stop();
    }

    public void SetBoost(bool bl)
    {
        if (bl) engine.clip = engineBoostClip;
        else engine.clip = engineLoopClip;
    }

    public void StopAllAudioSource()
    {
        bgm.Stop();
        engine.Stop();
        machineGun.Stop();
        mininglaser.Stop();

    }
}
