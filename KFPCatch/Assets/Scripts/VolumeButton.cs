using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{

    public Sprite VolumeOn;
    public Sprite VolumeOff;

    private void Start()
    {
        GetComponent<Image>().sprite = Settings.Instance.Volume ? VolumeOn : VolumeOff;
        Debug.Log(Settings.Instance.Volume);
    }

    public void ButtonPress ()
    {
        Settings.Instance.SetVolume(!Settings.Instance.Volume);
        GetComponent<Image>().sprite = Settings.Instance.Volume ? VolumeOn : VolumeOff;
    }
}
