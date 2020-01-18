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
                audioSrc.PlayOneShot(Hit);
                break;
            case "Shoot":
                audioSrc.PlayOneShot(Shoot);
                break;
            case "Move":
                audioSrc.PlayOneShot(Move);
                break;
            case "Transistion":
                audioSrc.PlayOneShot(Transistion);
                break;
        }
    }
}
