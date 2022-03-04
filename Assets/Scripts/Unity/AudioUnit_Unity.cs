using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioUnit_Unity
{
    public string name;
    public enum sfxEnums { uiClick, uiStart, Jump, Interact, Collect }
    public enum bgmEnums { track1, track2, track3, track4 }
    public AudioClip clip;
}

[System.Serializable]
public class UnityAudioBGM : AudioUnit_Unity
{
    public bgmEnums musicTracks;
}
[System.Serializable]
public class UnityAudioSfx : AudioUnit_Unity
{
    public sfxEnums sfx ;
    public AudioSource audioSource;


    //attach each sound audio source to different position in space for spatial audio
    protected void AttachAudioSourceToPosition(Transform position)
    {
        audioSource.transform.position = position.position;
    }

    //Attach the audiosource that is now newly positioned a clip to play and play it
    public void PlayAndAttachOneShot(Transform transform)
    {
        AttachAudioSourceToPosition(transform);
        audioSource.PlayOneShot(clip);
    }

}


