// Copyright (c) 2023 Limit Break Inc. All rights reserved

using UnityEngine;
using UnityEngine.Playables;

namespace Test
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private GameObject Puzzle;

        private PlayableDirector _puzzlePlayable;
        private PlayableDirector _puzzlePlayableFail;

        private void Start()
        {
            if (Puzzle != null)
            {
                var playables = Puzzle.GetComponentsInChildren<PlayableDirector>();
                foreach (var t in playables)
                {
                    switch (t.transform.name)
                    {
                        case "A1_P1_ShootingRange_T1":
                            _puzzlePlayable = t;
                            break;
                        case "A1_P1_ShootingRange_Fail":
                            _puzzlePlayableFail = t;
                            break;
                    }
                }

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
            if (_puzzlePlayable == null)
            {
                return;
            }

            var canPlay = _puzzlePlayable.state != PlayState.Playing && _puzzlePlayableFail.state != PlayState.Playing;
            
            if (GUI.Button(new Rect(10, 10, 200, 50), canPlay ? "Start MPA" : "MPA Playing...") && canPlay)
            {
                _puzzlePlayable.time = 0;
                _puzzlePlayable.Play();
            }
            
            if (GUI.Button(new Rect(10, 70, 200, 50), canPlay ? "Start MPA Fail" : "MPA Playing...") && canPlay)
            {
                _puzzlePlayableFail.time = 0;
                _puzzlePlayableFail.Play();
            }
        }
    }
}
