// PI Sequencer - Audio sequencer for Unity 3D
// Copyright (C) 2018 Soyut-Lama Medya Yapim Ltd. Sti.

// This file is part of PI Sequencer.

// PI Sequencer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// ---------------------------------------------------------------------

using System;
using PostIllusions.Audio.Music;
using UnityEngine;

namespace PostIllusions.Audio.Sequencer
{
    [RequireComponent(typeof(PlaybackController))]
    public class Sequencer : MonoBehaviour
    {
        [SerializeField] private Music music;

        private PlaybackController playbackController;
        private MusicalAudioTime musicalTime;
        private bool isPlaying;

        public static event Action<MusicalAudioTime> BeatUpdated;

        private void Awake()
        {
            playbackController = GetComponent<PlaybackController>();
            playbackController.PlaybackStateSet += PlaybackController_PlaybackStateSet;

            musicalTime = new MusicalAudioTime(music.Measure, music.Bpm);
        }

        private void OnDestroy()
        {
            playbackController.PlaybackStateSet -= PlaybackController_PlaybackStateSet;
        }

        private void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            musicalTime.AddTime(Time.deltaTime);
            UpdateCurrentRegion();
        }

        private void UpdateCurrentRegion()
        {
            foreach (MusicPart musicPart in music.Parts)
            {
                foreach (MusicRegion region in musicPart.Regions)
                {
                    //if (musicTime.TimeSeconds < region.End.Bar)
                }
            }
        }

        private void PlaybackController_PlaybackStateSet(PlaybackState playbackState)
        {
            switch (playbackState)
            {
                case PlaybackState.Play:
                    Play();
                    break;
                case PlaybackState.Pause:
                    Pause();
                    break;
                case PlaybackState.Stop:
                    Stop();
                    break;
                default:
                    throw new NotSupportedException(Enum.GetName(typeof(PlaybackState), playbackState));
            }
        }

        private void Play()
        {
            isPlaying = true;
        }

        private void Pause()
        {
            isPlaying = false;
        }

        private void Stop()
        {
            isPlaying = false;
            musicalTime.SetTime(0f);
        }

        private void SetBeats()
        {
            float currentBeatTime = ((musicalTime.Bar - 1) * musicalTime.Measure.BeatCount + musicalTime.Beat) * musicalTime.BeatDurationSeconds;

#if UNITY_EDITOR
            // Adjust music time to playback speed.
            currentBeatTime -= musicalTime.BeatDurationSeconds * (1f - 1f / Time.timeScale);
#endif // UNITY_EDITOR

            if (musicalTime.TimeSeconds <= currentBeatTime)
            {
                return;
            }

            musicalTime.IncrementBeat();

#if UNITY_EDITOR
            if (Time.timeScale != 1)
            {
                // Adjust music time to playback speed.
                musicalTime.SetTime((currentBeatTime + musicalTime.BeatDurationSeconds * (1f - 1f / Time.timeScale)));
            }
#endif // UNITY_EDITOR

            if (BeatUpdated != null)
            {
                BeatUpdated(musicalTime);
            }
        }
    }
}