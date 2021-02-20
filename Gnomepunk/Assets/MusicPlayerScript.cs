using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    AudioSource MpPlayer;
    [SerializeField] private AudioClip[] clips;
    public float volume;

    private int clip = 0;

    void Start()
    {
        MpPlayer = GetComponent<AudioSource>();
        MpPlayer.clip = clips[clip];
        MpPlayer.loop = false;
        MpPlayer.Play();
        MpPlayer.volume = volume;
        StartCoroutine(WaitForTrackToEnd());
    }

    IEnumerator WaitForTrackToEnd()
    {
        while (MpPlayer.isPlaying)
        {

            yield return new WaitForSeconds(0.01f);

        }
        clip++;
        MpPlayer.clip = clips[clip];
        MpPlayer.loop = false;
        MpPlayer.volume = volume;
        MpPlayer.Play();

    }
}
