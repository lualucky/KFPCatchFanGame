using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool Volume;


    static Settings instance = null;
    public static Settings Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        Volume = true;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void SetVolume(bool volume)
    {
        if (volume != Volume)
        {
            Volume = volume;
            AudioListener.volume = Volume ? 1f : 0f;
        }
    }
}
