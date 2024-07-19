using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Idle : MonoBehaviour
{
    [SerializeField] private PlayableDirector _mpa;
    [SerializeField] private float _idleTime = 3f;

    private Animator _animator;
    private float _currentIdleTime = 0f;
    private bool _isMPAPlaying = false;

    private const string _defaultIdleTrigger = "DefaultIdle";
    private const string _defaultIdleState = "Idle";
    private string[] _idleTriggers = { "Idle1", "Idle2" };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _mpa.played += SetDefaultIdleAnim;
        _mpa.stopped += (PlayableDirector director) => { _isMPAPlaying = false; };
    }

    private void Update()
    {
        if (_isMPAPlaying) return;

        _currentIdleTime += Time.deltaTime;

        if (_currentIdleTime >= _idleTime && _animator.GetCurrentAnimatorStateInfo(0).IsName(_defaultIdleState))
        {
            string randomIdleAnim = _idleTriggers[Random.Range(0, _idleTriggers.Length)];
            _animator.SetTrigger(randomIdleAnim);

            _currentIdleTime = 0;
        }
        else if (_currentIdleTime >= _idleTime && !_animator.GetCurrentAnimatorStateInfo(0).IsName(_defaultIdleState))
        {
            _animator.SetTrigger(_defaultIdleTrigger);

            _currentIdleTime = 0;
        }
    }

    private void SetDefaultIdleAnim(PlayableDirector director)
    {
        _isMPAPlaying = true;
        _animator.Play(_defaultIdleState);
        _currentIdleTime = 0;
    }
}
