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

        private void Start()
        {
            if (Puzzle != null)
            {
                _puzzlePlayable = Puzzle.GetComponentInChildren<PlayableDirector>();

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

            bool canPlay = _puzzlePlayable.state != PlayState.Playing;
            string buttonLabel = canPlay ? "Start MPA" : "MPA Playing...";
            if (GUI.Button(new Rect(10, 10, 200, 50), buttonLabel) && canPlay)
            {
                _puzzlePlayable.time = 0;
                _puzzlePlayable.Play();
            }
        }
    }
}
