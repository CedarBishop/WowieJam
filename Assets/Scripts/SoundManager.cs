using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Hit, Shoot, Move, Transistion;
    static AudioSource audioSrc;
    void Start()
    {
        Hit = Resources.Load<AudioClip>("Hit");
        Shoot = Resources.Load<AudioClip>("Shoot");
        Move = Resources.Load<AudioClip>("Move");
        Transistion = Resources.Load<AudioClip>("Transistion");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Hit":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(Hit);
                audioSrc.volume = 1f;
                break;
            case "Shoot":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(Shoot);
                audioSrc.volume = 1f;
                break;
            case "Move":
                audioSrc.volume = 1f;
                audioSrc.PlayOneShot(Move);
                audioSrc.volume = 1f;
                break;
            case "Transistion":
                audioSrc.volume = 0.3f;
                audioSrc.PlayOneShot(Transistion);
                audioSrc.volume = 1f;
                break;
        }
    }
}
