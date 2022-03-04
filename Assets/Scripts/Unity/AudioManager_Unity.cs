using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager_Unity : MonoBehaviour
{
    public static AudioManager_Unity instance;
    //What kind of audio sources I have?
    public AudioSource bgm, ambiance , staticSfx;
    [SerializeField]
    private float fadeDuration = 5;
    //Data storage of my audio types
    [SerializeField]
    public List<UnityAudioSfx> oneShotList = new List<UnityAudioSfx>();
    [Space][Space]
    public UnityAudioBGM.bgmEnums curBgm;
    public List<UnityAudioBGM> bgmList = new List<UnityAudioBGM>();
    [Space][Space]
    public List<AudioUnit_Unity> ambianceList = new List<AudioUnit_Unity>();

    private void Awake()
    {
        instance = this;
        foreach(var sfx in oneShotList)
        {
            if (sfx.audioSource == null) sfx.audioSource = staticSfx;
        }
    }
    private void OnValidate()
    {
        foreach (var oneShot in oneShotList)
        {
            oneShot.name = oneShot.sfx.ToString();
        }
        foreach(var bgm in bgmList)
        {
            bgm.name = bgm.musicTracks.ToString();
        }
    }
    //Play bgm according to bgm enum
    private void PlayBGM(UnityAudioBGM.bgmEnums bgmEnum)
    {
        UnityAudioBGM bgmToPlay = bgmList.Find(num => num.musicTracks == bgmEnum);
        bgm.clip = bgmToPlay.clip;
        bgm.Play();
    }
    private void PlayCurBGM()
    {
        PlayBGM(curBgm);
    }


    //Play or stop bgm if needed , fade demo.
    //Play one shot sound and place it in the world 
    public void PlayAndAttachOneShot(AudioUnit_Unity.sfxEnums sfx, Transform transform)
    {
        UnityAudioSfx sfxToPlay = oneShotList.Find(num => num.sfx == sfx);
        sfxToPlay.PlayAndAttachOneShot(transform);
    }


    //Fade out code , passing in audioSource, fadeDuration and final volume.
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}


