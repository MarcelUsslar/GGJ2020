using System;
using UniRx;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioSource _oneShotPrefab;
    [Space]
    [SerializeField] private float _initialStartDelay;
    [SerializeField] private AudioClip _onStart;
    [SerializeField] private float _initialLoopDelay;
    [SerializeField] private AudioClip[] _loopedClips;
    [SerializeField] private float _loopDelay;
    [SerializeField] private AudioClip _onSuccess;

    private SerialDisposable _timerDisposable;
    private int _loopIndex;

    public void OnSuccess()
    {
        _source.Stop();
        _timerDisposable.Dispose();
        PlayClipExternal(_onSuccess);
    }

    private void Awake()
    {
        _timerDisposable = new SerialDisposable().AddTo(gameObject);

        Observable.Timer(TimeSpan.FromSeconds(_initialStartDelay))
            .Subscribe(_ => PlayClip(_onStart, _initialLoopDelay, PlayLoopClip))
            .AddTo(gameObject);
    }
    
    private void PlayLoopClip()
    {
        if (_loopedClips.Length == 0)
            return;

        PlayClip(_loopedClips[_loopIndex], _loopDelay, PlayLoopClip);
        _loopIndex = (_loopIndex + 1) % _loopedClips.Length;
    }

    private void PlayClip(AudioClip clip, float additionalDelay, Action onClipEnded)
    {
        _source.Stop();

        if (clip == null)
        {
            onClipEnded();
            return;
        }

        _source.clip = clip;
        _source.Play();
        _timerDisposable.Disposable = Observable.Timer(TimeSpan.FromSeconds(clip.length + additionalDelay))
            .Subscribe(_ => onClipEnded());
    }

    public void PlayClipExternal(AudioClip clip)
    {
        if (clip == null)
            return;

        var external = Instantiate(_oneShotPrefab, null);
        external.clip = clip;
        external.Play();

        Destroy(external.gameObject, clip.length + 1);
    }
}