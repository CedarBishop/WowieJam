using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [SerializeField]
    Sound[] sounds;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.parent = transform;
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void Play(string soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                sounds[i].Play();
                return;
            }
        }
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    [Range(0.0f,1.0f)]public float volume = 1;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
    }

    public void Play()
    {
        source.Play();
    }
}
