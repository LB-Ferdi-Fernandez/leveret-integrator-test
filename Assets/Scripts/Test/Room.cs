// Copyright (c) 2023 Limit Break Inc. All rights reserved

using UnityEngine;
using UnityEngine.Playables;

namespace Test
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private GameObject Puzzle_T1;
        [SerializeField]
        private GameObject Puzzle_T2;
        [SerializeField]
        private GameObject Puzzle_T3;
        [SerializeField]
        private GameObject VirtualCamera;

        private PlayableDirector _puzzlePlayable_t1;
        private PlayableDirector _puzzlePlayable_t2;
        private PlayableDirector _puzzlePlayable_t3;

        private void Start()
        {
            if (Puzzle_T1 != null || Puzzle_T2 != null || Puzzle_T3 != null)
            {
                _puzzlePlayable_t1 = Puzzle_T1.GetComponent<PlayableDirector>();
                _puzzlePlayable_t2 = Puzzle_T2.GetComponent<PlayableDirector>();
                _puzzlePlayable_t3 = Puzzle_T3.GetComponent<PlayableDirector>();

                var vCam = VirtualCamera.GetComponent<VirtualCameraDummy>();
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
            if (_puzzlePlayable_t1 == null || _puzzlePlayable_t2 == null || _puzzlePlayable_t3 == null)
            {
                return;
            }

            bool canPlay = (_puzzlePlayable_t1.state != PlayState.Playing) && (_puzzlePlayable_t2.state != PlayState.Playing) && (_puzzlePlayable_t3.state != PlayState.Playing);
            string buttonLabel_t1 = canPlay ? "Start MPA T1" : "MPA Playing...";
            string buttonLabel_t2 = canPlay ? "Start MPA T2" : "MPA Playing...";
            string buttonLabel_t3 = canPlay ? "Start MPA T3" : "MPA Playing...";
            if (GUI.Button(new Rect(10, 10, 200, 50), buttonLabel_t1) && canPlay)
            {
                _puzzlePlayable_t1.time = 0;
                _puzzlePlayable_t1.Play();
            }

            if (GUI.Button(new Rect(10, 60, 200, 50), buttonLabel_t2) && canPlay)
            {
                _puzzlePlayable_t2.time = 0;
                _puzzlePlayable_t2.Play();
            }
            if (GUI.Button(new Rect(10, 110, 200, 50), buttonLabel_t3) && canPlay)
            {
                _puzzlePlayable_t3.time = 0;
                _puzzlePlayable_t3.Play();
            }
        }
    }
}
