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
        private Region[] regions;

        private Measure measure;
        private MusicalTime musicTime;
        private bool isPlaying;

        private void Awake()
        {
            Playback.PlaybackStateSet += HandlePlaybackStateSet;
        }

        private void OnDestroy()
        {
            Playback.PlaybackStateSet -= HandlePlaybackStateSet;
        }

        public static event Action<MusicAudioTime> UpdateBeat;

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
            foreach (Region region in regions)
            {
                if (region.StartBar > musicTime.)
            }
        }

        private void HandlePlaybackStateSet(Playback.State playbackEvent)
        {
            switch (playbackEvent)
            {
                case Playback.State.Play:
                    Play();
                    break;
                case Playback.State.Pause:
                    Pause();
                    break;
                case Playback.State.Stop:
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
            musicTime.Reset();
        }

        private void SetBeats()
        {
            double timeOfCurrentBeat = ((musicTime.Bar - 1) * 4 + musicTime.Beat) * musicTime.BeatDuration;

#if UNITY_EDITOR
            timeOfCurrentBeat -= musicTime.BeatDuration* (1d - 1d / Time.timeScale);
#endif // UNITY_EDITOR

            if (musicTime.Miliseconds > timeOfCurrentBeat)
            {
                if (musicTime.Beat < 4)
                {
                    musicTime.AddBeat(1);
                }
                else
                {
                    musicTime.SetBeat(1);
                    musicTime.AddBar(1);
                }

#if UNITY_EDITOR
                if (Time.timeScale != 1)
                {
                    musicTime.SetTime((float)(timeOfCurrentBeat + musicTime.BeatDuration* (1f - 1f / Time.timeScale)));
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