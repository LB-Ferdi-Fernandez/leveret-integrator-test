using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Linq;

public class RandomIdles : MonoBehaviour
{
    [SerializeField] PlayableDirector unityPlayable;
    [SerializeField] Animator unityAnimator;

    AudioSource unitySource;
    [SerializeField] AudioClip[] unityClip;
    //idle 1 - univ1192
    //idle 2 - univ1207
    //idle 3 - univ0030
    //idle 4 - univ1217

    string idleName = "WAIT00";

    bool playablePlaying = false;
    float idleInterval = 3f;
    float currentTime = 0;
    int[] idleID = new int[4] { 1, 2, 3, 4 };
    int idleIndex = 0;
    float currentVolume;

    void Start()
    {
        unitySource = GetComponent<AudioSource>();
        RandomizeIndex();
        currentVolume = unitySource.volume;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = unityAnimator.GetCurrentAnimatorStateInfo(0);
        if(unityPlayable.state == PlayState.Playing)
        {
            playablePlaying = true;
            StartCoroutine(FadeOut());
            currentTime = 0;
        }
        else
        {
            unitySource.volume = currentVolume;
            playablePlaying = false;
        }

        if (stateInfo.IsName(idleName) && !playablePlaying)
        {
            currentTime += Time.deltaTime;
            if (currentTime > idleInterval)
            {
                currentTime = 0;
                idleInterval = Random.Range(5.0f, 7.0f);

                if (idleIndex < idleID.Length)
                {
                    unityAnimator.SetTrigger("Idle" + idleID[idleIndex]);
                    unitySource.PlayOneShot(unityClip[idleID[idleIndex] - 1]);

                    Debug.Log("Playing Idle" + idleID[idleIndex]);
                    Debug.Log("Next Interval: " + idleInterval);

                    idleIndex++;
                    if (idleIndex >= idleID.Length)
                    {
                        Debug.Log("Resetting Counter");
                        idleIndex = 0;
                        RandomizeIndex();
                    }
                }
            }
        }
    }

    IEnumerator FadeOut()
    {
        while(unitySource.volume > 0)
        {
            unitySource.volume -= 0.0001f;
            yield return null;
        }

        unitySource.volume = 0;
        unitySource.Stop();
    }

    void RandomizeIndex()
    {
        idleID = new int[4] { 1, 2, 3, 4 };
        System.Random rng = new System.Random();
        idleID = idleID.OrderBy(i => rng.Next()).ToArray();
        Debug.Log("Shuffled Idle IDs: " + string.Join(", ", idleID));
    }
}
