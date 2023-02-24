using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : Manager<SoundManager>
{
    public SoundAudioClip[] SoundAudioClipArray;
    public AudioMixerSnapshot snapShotMain, snapShotPause;

    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioMixerGroup mixer;
        public AudioClip audioClip;
    }

    // enumeration of all sounds, added in GameAssets
    public enum Sound
    {
        // music
        MusicExploration,
        MusicInteraction,
        MusicBattle,

        //// player
        //Jetpack,
        //BuildBlock,
        //DeleteBlock, // blank
        //BuildFactory,
        //CollectResource,

        //// game
        //UIClick,
        //UISelect, 
    }

    public static GameObject BGM_loop;

    // last time the soundclip is played
    private static Dictionary<Sound, float> soundTimerDic;
    // soundclips play minimum interval
    private static Dictionary<Sound, float> soundMaxTimerDic;

    private new void Awake()
    {
        base.Awake();
        soundTimerDic = new() {};
        soundMaxTimerDic = new() { };
        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundTimerDic[sound] = 0;
            soundMaxTimerDic[sound] = 0;
        }
        //soundMaxTimerDic[Sound.ShellExplosion] = 0.05f;
        //soundMaxTimerDic[Sound.BlockExplostion] = 0.05f;
        //soundMaxTimerDic[Sound.BombExplosion] = 0.05f;
        //soundMaxTimerDic[Sound.MachineGunFire] = 0.05f;
    }

    // play one shot
    public static GameObject PlaySound(Sound sound, float volume = 1f)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new("Sound");
            soundGameObject.transform.SetParent(Instance.transform);
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.volume = volume;
            SoundAudioClip soundAudioClip = GetSoundAudioClip(sound);
            audioSource.outputAudioMixerGroup = soundAudioClip.mixer;
            audioSource.PlayOneShot(soundAudioClip.audioClip);

            Destroy(soundGameObject, soundAudioClip.audioClip.length); // destroy after length of soundclip
            return soundGameObject;
        }
        return null;
    }

    public static void StartBGM(Sound loop)
    {
        if (BGM_loop) Destroy(BGM_loop);

        BGM_loop = new("BGMLoop");
        BGM_loop.transform.SetParent(Instance.transform);
        AudioSource loopAudioSource = BGM_loop.AddComponent<AudioSource>();
        SoundAudioClip loopClip = GetSoundAudioClip(loop);

        loopAudioSource.outputAudioMixerGroup = loopClip.mixer;

        loopAudioSource.clip = loopClip.audioClip;
        loopAudioSource.loop = true;
        loopAudioSource.Play();
    }

    public void FadeAudio(GameObject audioGameObject, float duration, float targetVolume)
    {
        if (audioGameObject)
            StartCoroutine(StartFade(audioGameObject.GetComponent<AudioSource>(), duration, targetVolume));
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration && audioSource)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void lowPassBGM(bool on)
    {
        if (on)
            snapShotPause.TransitionTo(.001f);
        else
            snapShotMain.TransitionTo(.001f);
        print($"low pass transition to {on}");
    }

    public void pauseBGM(float duration = 0)
    {
        FadeAudio(BGM_loop, duration, 0f);
    }

    public void restartBGM(float duration = 0)
    {
        FadeAudio(BGM_loop, duration, 1f);
    }

    // check minimum interval
    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDic.ContainsKey(sound))
        {
            float lastTimePlayed = soundTimerDic[sound];
            float timerMax = soundMaxTimerDic[sound];
            if (lastTimePlayed + timerMax <= Time.time)
            {
                soundTimerDic[sound] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private static SoundAudioClip GetSoundAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in Instance.SoundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip;
            }
        }
        Debug.LogError("Sound not found");
        return null;
    }
}

