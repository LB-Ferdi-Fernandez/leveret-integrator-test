using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public Button playButton;
    public PlayableDirector playableDirector;

    void Start()
    {
        // Hook up the button click event
        playButton.onClick.AddListener(PlayTimeline);
    }

    void PlayTimeline()
    {
        // Play the timeline
        playableDirector.Play();

        // Disable the button during the timeline playback
        playButton.interactable = false;

        // Invoke a method to re-enable the button after the timeline duration
        Invoke("EnableButton", (float)playableDirector.duration);
    }

    void EnableButton()
    {
        // Enable the button after the timeline is finished
        playButton.interactable = true;
    }
}
