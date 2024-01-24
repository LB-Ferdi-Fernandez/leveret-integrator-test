using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnimManager : MonoBehaviour
{

    [SerializeField] private Animator animator = null;
    [SerializeField] private float minInterval = 1f;
    [SerializeField] private float maxInterval = 5f;

    private bool isMPAPlayed = false;
    private bool isAnimationDone = false;

    private void Start()
    {
        PlayRandomAnimation();
    }

    public void OnMPAPlaying()
    {
        isMPAPlayed = true;
        animator.SetInteger("AnimationIndex", 0);
        animator.SetTrigger("IdleTrigger");
        Debug.Log("Play MPA");
    }

    public void MPADonePlaying()
    {
        isMPAPlayed = false;
        Debug.Log("Play Animation");
    }

    private void PlayRandomAnimation()
    {
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        while (true)
        {
            float animationInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(animationInterval);
            int animationIndex = Random.Range(1, 4);

            if (!isMPAPlayed)
            {
                animator.SetInteger("AnimationIndex", animationIndex);
                animator.SetTrigger("IdleTrigger");
                yield return new WaitUntil(HandleOnAnimationPlay);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private bool HandleOnAnimationPlay()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE");
    }
}

