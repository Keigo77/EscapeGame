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

    public void SetBGM()
    {
        _audioMixer.SetFloat("BGM", _bgmSlider.value);
    }

    public void SetSe()
    {
        _audioMixer.SetFloat("SE", _seSlider.value);
    }

    public void PlaySe()
    {
        _sePreAudio.PlayOneShot(_preSe);
    }
    
}
