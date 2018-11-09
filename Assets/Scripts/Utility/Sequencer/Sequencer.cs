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


namespace PostIllusions.Audio.Sequencer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using PostIllusions.Audio.Music;
    using UnityEngine;

    public class Sequencer : MonoBehaviour
    {
        [SerializeField]
        private MusicRegion[] regions;

        private Measure measure;
        private MusicalAudioTime musicTime;
        private bool isPlaying;

        private void Awake()
        {
            Playback.PlaybackStateSet += HandlePlaybackStateSet;
        }

        private void OnDestroy()
        {
            Playback.PlaybackStateSet -= HandlePlaybackStateSet;
        }

        public static event Action<MusicalAudioTime> UpdateBeat;

        private void Update()
        {
            if (isPlaying)
            {
                musicTime.AddTime(Time.deltaTime);
                SetCurrentRegion();
            }
        }

        private void SetCurrentRegion()
        {
            throw new NotImplementedException();
        }

        private void HandlePlaybackStateSet(PlaybackState playbackEvent)
        {
            switch (playbackEvent)
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
                    throw new NotSupportedException("Undefined playback state: " + playbackEvent);
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
            musicTime.ResetToBeginning();
        }

        private void SetBeats()
        {
            float currentBeatTime = ((musicTime.Bar - 1) * musicTime.Measure.BeatCount + musicTime.Beat) * musicTime.BeatDuration;

#if UNITY_EDITOR
            // Adjust music time to playback speed.
            currentBeatTime -= musicTime.BeatDuration * (1f - 1f / Time.timeScale);
#endif // UNITY_EDITOR

            if (musicTime.Time > currentBeatTime)
            {
                if (musicTime.Beat < musicTime.Measure.BeatCount)
                {
                    musicTime.IncrementBeat();
                }
                else
                {
                    musicTime.SetBeat(1);
                    musicTime.AddBar(1);
                }

#if UNITY_EDITOR
                if (Time.timeScale != 1)
                {
                    // Adjust music time to playback speed.
                    musicTime.SetTime((currentBeatTime + musicTime.BeatDuration * (1f - 1f / Time.timeScale)));
                }
#endif // UNITY_EDITOR

                if (UpdateBeat != null)
                {
                    UpdateBeat(musicTime);
                }
            }
        }
    }
}