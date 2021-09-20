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
    public class Sequencer : MonoBehaviour
    {
        public static float BarDuration;
        public static float BeatDuration;

        [SerializeField] private SongMeta songMeta;

        // TODO: Check if these needs to be double to keep sync in long runs.
        private float nextBeatTime;
        private float loopPointTime;

        public static event Action Updated;

        public static AudioSource Music { get; private set; }
        
        public static MusicalTime MusicalTime { get; private set; }

        private void Awake()
        {
            BeatDuration = 60f / songMeta.Bpm;
            BarDuration = BeatDuration * songMeta.Measure.BeatCount;
                
            loopPointTime = (songMeta.LoopAt.Bar * songMeta.Measure.BeatCount + songMeta.LoopAt.Beat) * BeatDuration;
            
            // TODO: Ensure only one object exists in scene.
            Music = FindObjectOfType<AudioSource>();
            Music.time = 0f;
            MusicalTime = new MusicalTime(1, 1);
            nextBeatTime = BeatDuration;
        }

        private void Start()
        {
            // TODO: Check if this needs to be in awake.
            Music.Play();
        }

        private void Update()
        {
#if UNITY_EDITOR
            // TODO: Implement editor playback speed.
            //AdjustPlaybackSpeed();
#endif
            UpdateMusicalTime();
            LoopMusicTo(songMeta.LoopTo);

            if (MusicalTime.CompareTo(new MusicalTime(3, 1)) == 0)
            {
                Debug.LogWarning(Music.time);
            }
            
            if (Updated != null)
            {
                Updated.Invoke();
            }
        }
        
        private void UpdateMusicalTime()
        {
#if UNITY_EDITOR
            // TODO: Playback speed
            // Adjust music time to playback speed.
            // if (Time.timeScale != 1)
            // {
            //     currentBeatTime -= BeatDuration * (1f - 1f / Time.timeScale);
            // }
#endif // UNITY_EDITOR

            if (Music.time < nextBeatTime)
            {
                return;
            }

            int measureBeatCount = songMeta.Measure.BeatCount;
            int beat = MusicalTime.Beat + 1;
            MusicalTime = beat > measureBeatCount ?
                new MusicalTime(MusicalTime.Bar + 1, 1) :
                new MusicalTime(MusicalTime.Bar, beat);
            nextBeatTime = ((MusicalTime.Bar - 1) * measureBeatCount + (MusicalTime.Beat - 1) + 1) * BeatDuration;

#if UNITY_EDITOR
            // TODO: Playback speed.
            // Adjust music time to playback speed.
            // if (Time.timeScale != 1f)
            // {
            //     audioTime.SetTime((currentBeatTime + audioTime.BeatDuration * (1f - 1f / Time.timeScale)));
            // }
#endif // UNITY_EDITOR
        }

        private void LoopMusicTo(MusicalTime loopToTime)
        {
            if (MusicalTime.CompareTo(songMeta.LoopAt) < 0)
            {
                return;
            }

            Debug.LogWarning("From: " + Music.time + " to: " + ((loopToTime.Bar - 1f) * songMeta.Measure.BeatCount + loopToTime.Beat - 1f) * BeatDuration);
            Music.time = ((loopToTime.Bar - 1f) * songMeta.Measure.BeatCount + loopToTime.Beat - 1f) * BeatDuration;
            MusicalTime = new MusicalTime(loopToTime.Bar, loopToTime.Beat);
            Debug.LogWarning(" next: " + ((MusicalTime.Bar - 1) * songMeta.Measure.BeatCount + (MusicalTime.Beat - 1) + 1) * BeatDuration);
            nextBeatTime = ((MusicalTime.Bar - 1) * songMeta.Measure.BeatCount + (MusicalTime.Beat - 1) + 1) * BeatDuration;
            // TODO: Check if this is necessary (i.e. if playback stops when music.time is updated.)
            Music.Play();
        }
        
#if UNITY_EDITOR
        private void AdjustPlaybackSpeed()
        {
            // TODO: Ensure it does not break after looping.
            if (MusicalTime.Bar < SequencerEditorController.FastForwardToBar
                && SequencerEditorController.FastForwardSpeed != 1f)
            {
                Music.volume = 0;
                Time.timeScale = SequencerEditorController.FastForwardSpeed;
            }
            else
            {
                Music.volume = 1;
                Time.timeScale = SequencerEditorController.PlaybackSpeed;
            }
        }
#endif // UNITY_EDITOR
    }
}