using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolimeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _sePreAudio;
    [SerializeField] private AudioClip _preSe;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _seSlider;

    private void Start()
    {
        //ミキサーのvolumeにスライダーのvolumeを入れてます。

        //BGM
        _audioMixer.GetFloat("BGM", out float bgmVolume);
        _bgmSlider.value = bgmVolume;
        //SE
        _audioMixer.GetFloat("SE", out float seVolume);
        _seSlider.value = seVolume;
    }
    
    public void SetBGM(float volume)
    {
        _audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        _audioMixer.SetFloat("SE", volume);
    }

    public void PlaySe()
    {
        _sePreAudio.PlayOneShot(_preSe);
    }
    
}
