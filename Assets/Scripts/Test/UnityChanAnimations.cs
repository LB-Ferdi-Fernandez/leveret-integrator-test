using System.Collections;
using UnityEngine;

public class UnityChanAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private float minInterval = 0f;
    [SerializeField] private float maxInterval = 0f;
    private int currentRandom = 0;
    private bool isMPAPlaying = false;

    private void Start()
    {
        StartCoroutine(RandomizeIdleAnimation());
    }

    private IEnumerator RandomizeIdleAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            if (!isMPAPlaying)
            {
                int random = Random.Range(1, 5);

                while (currentRandom == random)
                {
                    random = Random.Range(1, 5);
                }

                currentRandom = random;
                animator.SetInteger("Idle", currentRandom);
                animator.SetTrigger("Animate");
            }
        }
    }

    public void SetMPAState(bool isPlaying)
    {
        isMPAPlaying = isPlaying;

        if (isMPAPlaying)
        {
            animator.SetInteger("Idle", 0);
            animator.SetTrigger("Animate");
        }
    }
}