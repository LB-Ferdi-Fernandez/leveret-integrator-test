using UnityEngine;
using UnityEngine.Playables;

namespace Test
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private GameObject Puzzle;

        [SerializeField] private PlayableDirector _puzzlePlayable_T1;
        [SerializeField] private PlayableDirector _puzzlePlayable_T2;
        [SerializeField] private PlayableDirector _puzzlePlayable_T3;

        private void Start()
        {
            if (Puzzle != null)
            {
                var vCam = Puzzle.GetComponentInChildren<VirtualCameraDummy>();
                if (vCam != null)
                {
                    var mainCam = Camera.main;
                    if (mainCam != null)
                    {
                        var mainCamTransform = mainCam.transform;
                        mainCamTransform.SetParent(vCam.transform);
                        mainCamTransform.localPosition = Vector3.zero;
                        mainCamTransform.localRotation = Quaternion.identity;
                    }
                }
            }
        }

        private void OnGUI()
        {
            if (_puzzlePlayable_T1 == null && _puzzlePlayable_T2 == null && _puzzlePlayable_T3 == null)
            {
                return;
            }

            bool canPlay = _puzzlePlayable_T1.state != PlayState.Playing && _puzzlePlayable_T2.state != PlayState.Playing && _puzzlePlayable_T3.state != PlayState.Playing;
            string buttonLabel_T1 = canPlay ? "Start MPA_T1" : "MPA Playing...";
            string buttonLabel_T2 = canPlay ? "Start MPA_T2" : "MPA Playing...";
            string buttonLabel_T3 = canPlay ? "Start MPA_T3" : "MPA Playing...";

            if (GUI.Button(new Rect(10, 10, 200, 50), buttonLabel_T1) && canPlay)
            {
                _puzzlePlayable_T1.time = 0;
                _puzzlePlayable_T1.Play();
            }
            else if (GUI.Button(new Rect(10, 70, 200, 50), buttonLabel_T2) && canPlay)
            {
                _puzzlePlayable_T2.time = 0;
                _puzzlePlayable_T2.Play();
            }
            else if (GUI.Button(new Rect(10, 130, 200, 50), buttonLabel_T3) && canPlay)
            {
                _puzzlePlayable_T3.time = 0;
                _puzzlePlayable_T3.Play();
            }
        }
    }
}