using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    AudioSource _mainTrack;
    [SerializeField] AudioSource _extFirstTrack;
    [SerializeField] AudioSource _extSecondTrack;
    [SerializeField] float[] _samples = new float[128];

    public void Init()
    {
        _mainTrack = GetComponent<AudioSource>();
    }
    private void Update()
    {
        GetSpectrumAudioSource();

        if (_samples[1] > 0.01f)
        {
            EventManager.SoundHit();
        }
        else if (_samples[1] < 0.01f)
        {
            EventManager.SilentHit();
        }
    }
    void GetSpectrumAudioSource()
    {
        _mainTrack.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
    public void FirstExtendedTrackVolumeUp()
    {
        _extFirstTrack.volume = 0.3f;
    }
    public void SecondExtendedTrackVolumeUp()
    {
        _extSecondTrack.volume = 0.3f;
    }
}
