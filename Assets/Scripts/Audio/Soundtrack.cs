using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Audio
{
  [RequireComponent(typeof(AudioSource))]
  public class Soundtrack : MonoBehaviour
  {
    public static Soundtrack Instance { get; private set; }

    [SerializeField] private AudioMixer _musicMixer;
    
    [Space]
    [SerializeField] private AudioClip[] _soundtrackClips;

    private AudioSource _audioSource;

    private void Awake() {
      _audioSource = GetComponent<AudioSource>();

      if (Instance == null) {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        PlayRandomSoundtrack();
      }
      else {
        Destroy(gameObject);
      }
    }

    private void PlayRandomSoundtrack() {
      int randomIndex = Random.Range(0, _soundtrackClips.Length);

      _audioSource.clip = _soundtrackClips[randomIndex];
      _audioSource.Play();

      Invoke(nameof(OnTrackEnd), _audioSource.clip.length);
    }

    private void OnTrackEnd() {
      PlayRandomSoundtrack();
    }

    private void OnApplicationPause(bool pauseStatus) {
      if (pauseStatus) {
        _audioSource.Pause();

        CancelInvoke(nameof(OnTrackEnd));
      }
      else {
        _audioSource.UnPause();

        Invoke(nameof(OnTrackEnd), _audioSource.clip.length - _audioSource.time);
      }
    }
  }
}