using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    AudioSource MpPlayer;
    [SerializeField] private AudioClip[] clips;
    public float volume;
    public bool random = false;

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
        while (true)
        {
            while (MpPlayer.isPlaying)
            {

                yield return new WaitForSeconds(0.01f);

            }
            if (!random)
            {
                clip++;
            }
            else
            {
                clip = Random.Range(0, clips.Length - 1);
            }
            if (clip >= clips.Length)
            {
                clip = 0;
            }
            MpPlayer.clip = clips[clip];
            Debug.Log("Clip is : " + clip);
            MpPlayer.loop = false;
            MpPlayer.volume = volume;
            MpPlayer.Play();
        }
    }
}
