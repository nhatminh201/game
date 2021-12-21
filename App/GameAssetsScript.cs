using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsScript : MonoBehaviour
{
    private static GameAssetsScript _i;
    public static GameAssetsScript i
    {
        get
        {
            if (_i == null) _i =
                     Instantiate(Resources.Load<GameAssetsScript>("GameAssets"));
            return _i;
        }
    }

    public Transform[] enemyPrefab;
    public Transform damagePopup;
    public Transform slashEffect;
    public Transform slashWave;
    public Transform slashWaveGold;
    public Transform bubble;
    public Transform item;
    public Transform levelup;
    public Transform skillMenu;
    public Transform strenghSkill;
    public Transform agilitySkill;
    public Transform defenseSkill;
    public Transform aura;
    public Transform droploot;
    //public AudioClip backgroundMusic;

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
