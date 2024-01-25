using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationRandomizer : MonoBehaviour
{
    public AnimationClip[] idleAnims;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        PlayRandomIdleAnimation();
    }

    public void PlayRandomIdleAnimation()
    {
        int rnd = Random.Range(0, idleAnims.Length-1);
        AnimationClip randomAnimation = idleAnims[rnd];
        anim.Play(randomAnimation.name,0);
        Debug.Log("Random Animation Played: " + rnd + randomAnimation.name);
    }

    public void OnEndIdleAnim()
    {
        PlayRandomIdleAnimation();
    }
}
