using System.Collections;
using UnityEngine;

public class UnityChanFidgeting : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private int minInterval = 3;
    [SerializeField] private int maxInterval = 5;
    private bool isMPAPlaying = false;

    private void Start()
    {
        StartCoroutine(RandomizeStates());
    }

    private IEnumerator RandomizeStates()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            if (!isMPAPlaying)
            {
                int random = Random.Range(1, 5);
                animator.SetInteger("Animation", random);
                animator.SetTrigger("trigger animation");
            }
        }
    }

    public void SetMPAState(bool isPlaying)
    {
        isMPAPlaying = isPlaying;

        if (isMPAPlaying)
        {
            animator.SetInteger("Animation", 0);
            animator.SetTrigger("trigger animation");
        }
    }
}