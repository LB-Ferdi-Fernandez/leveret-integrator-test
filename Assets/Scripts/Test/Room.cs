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

        private PlayableDirector _puzzlePlayable_T1;
        private PlayableDirector _puzzlePlayable_T2;
        private PlayableDirector _puzzlePlayable_T3;

        private void Start()
        {
            if (Puzzle_T1 != null && Puzzle_T2 != null && Puzzle_T3 != null)
            {
                _puzzlePlayable_T1 = Puzzle_T1.GetComponent<PlayableDirector>();
                _puzzlePlayable_T2 = Puzzle_T2.GetComponent<PlayableDirector>();
                _puzzlePlayable_T3 = Puzzle_T3.GetComponent<PlayableDirector>();

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
            if (_puzzlePlayable_T1 == null && _puzzlePlayable_T2 == null && _puzzlePlayable_T3 == null)
            {
                return;
            }

            bool canPlay = _puzzlePlayable_T1.state != PlayState.Playing && _puzzlePlayable_T2.state != PlayState.Playing && _puzzlePlayable_T3.state != PlayState.Playing;
            string buttonLabelT1 = canPlay ? "Start MPA T1" : "MPA Playing...";
            if (GUI.Button(new Rect(10, 10, 200, 50), buttonLabelT1) && canPlay)
            {
                _puzzlePlayable_T1.time = 0;
                _puzzlePlayable_T1.Play();
            }

            string buttonLabelT2 = canPlay ? "Start MPA T2" : "MPA Playing...";
            if (GUI.Button(new Rect(300, 10, 200, 50), buttonLabelT2) && canPlay)
            {
                _puzzlePlayable_T2.time = 0;
                _puzzlePlayable_T2.Play();
            }

            string buttonLabelT3 = canPlay ? "Start MPA T3" : "MPA Playing...";
            if (GUI.Button(new Rect(600, 10, 200, 50), buttonLabelT3) && canPlay)
            {
                _puzzlePlayable_T3.time = 0;
                _puzzlePlayable_T3.Play();
            }
        }
    }
}
