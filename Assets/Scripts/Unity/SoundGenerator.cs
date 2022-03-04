using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundGenerator : MonoBehaviour
{
    public sfxType _soundType;
    public enum sfxType
    {
        bgm,
        collision,
    }
    [Space]
    [Header("BGM Config")]
    public UnityAudioBGM.bgmEnums bgmToPlay;
    public bool playOnStart;
    public float fadeInTime;
    [Space]
    [Header("SFX Config")]
    public UnityAudioSfx.sfxEnums sfxToPlay;
    public Collider2D _colliderGetter;
    public int secondsToDestroy;

    public const string PLAYER_TAG = "Player";
    [SerializeField]

    public void FadeInBGM()
    {
        if(AudioManager_Unity.instance.bgm.isPlaying == false)
        StartCoroutine(AudioManager_Unity.instance.StartFade(AudioManager_Unity.instance.bgm, fadeInTime, 1));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_soundType == sfxType.collision)
        {
            if (collision.collider.tag == "Player")
            {
                print("Collision");
                    Destroy(gameObject, secondsToDestroy);
                    AudioManager_Unity.instance.PlayAndAttachOneShot(this.sfxToPlay, gameObject.transform);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision == _colliderGetter)
        {
            if (_soundType == sfxType.collision)
            {
                print("Trigger");
                  AudioManager_Unity.instance.PlayAndAttachOneShot(this.sfxToPlay, transform);
                  Destroy(gameObject, secondsToDestroy);
                  AudioManager_Unity.instance.PlayAndAttachOneShot(this.sfxToPlay, transform);
                }
        }
    }


    public void ReduceHP() => print("reduce hp");
    public void IncreaseHP() => print("increase hp");
    

}
    
