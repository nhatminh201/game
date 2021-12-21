using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonScript : MonoBehaviour
{
    [SerializeField] public GameManager game;
    [SerializeField] public GameObject sound;
    [SerializeField] public GameObject music;
    [SerializeField] public GameObject soundOn;
    [SerializeField] public GameObject soundOff;
    [SerializeField] public GameObject musicOn;
    [SerializeField] public GameObject musicOff;

    private bool isOpenSetting, isMusicOn, isSoundOn;
    // Start is called before the first frame update
    void Start()
    {
        isOpenSetting = false;
        isMusicOn = true;
        isSoundOn = true;
    }
    public void pressSetting()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIClick, false, 1, game.getIsSoundOn());
        if (!isOpenSetting)
        {
            sound.SetActive(true);
            music.SetActive(true);
            isOpenSetting = true;
        }
        else
        {
            isOpenSetting = false;
            sound.SetActive(false);
            music.SetActive(false);
        }
    }
    public void pressMusic()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIClick, false, 1, game.getIsSoundOn());
        if (!isMusicOn)
        {
            game.setMusicOnOff(true);
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            isMusicOn = true;
        }
        else
        {
            game.setMusicOnOff(false);
            isMusicOn = false;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
    }
    public void pressSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIClick, false, 1, game.getIsSoundOn());
        if (!isSoundOn)
        {
            game.setSoundOnOff(true);
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            isSoundOn = true;
        }
        else
        {
            game.setSoundOnOff(false);
            isSoundOn = false;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }
}
