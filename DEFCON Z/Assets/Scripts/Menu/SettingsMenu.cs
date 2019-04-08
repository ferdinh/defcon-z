using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;

    // Start is called before the first frame update
    // Still need to complete volume bar within Unity scene
    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Volume", volume);
    }

    public void setQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
