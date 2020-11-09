using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip Sound;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SoundOnClick);
    }

    void SoundOnClick()
    {
        Settings.Instance.gameObject.GetComponent<AudioSource>().PlayOneShot(Sound);
    }
}
