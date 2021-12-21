using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public enum Sound
    {
        UIClick,
        PlayerAttack1,
        PlayerAttack2,
        PlayerAttack3,
        SleepDestroy,
        SuccessUpgrade,
        Fail,
        LevelUp,
        Sheep1,
        Sheep2,
        Sheep3,
        BackgroundMusic,
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sound sound, bool isLoop, float volume, bool isOn)
    {
        if(oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One Shot Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        oneShotAudioSource.loop = isLoop;
        if (isOn)
            oneShotAudioSource.volume = volume;
        else
            oneShotAudioSource.volume = 0;
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssetsScript.SoundAudioClip soundAudioClip in GameAssetsScript.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }
}
