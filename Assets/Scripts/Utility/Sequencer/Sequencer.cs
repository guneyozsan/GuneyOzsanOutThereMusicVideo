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
        public static int MeasureBeatCount;

        [SerializeField] private SongMeta songMeta;

        // TODO: Check if these needs to be double to keep sync in long runs.
        private float nextBeatTime;

        public static event Action Updated;

        public static AudioSource Music { get; private set; }
        
        public static MusicalTime MusicalTime { get; private set; }

        private void Awake()
        {
            BeatDuration = 60f / songMeta.Bpm;
            MeasureBeatCount = songMeta.Measure.BeatCount;
            BarDuration = BeatDuration * MeasureBeatCount;
            
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
            
#if UNITY_EDITOR
            AdjustPlaybackSpeed();
#endif

            UpdateMusicalTime();
            LoopMusicTo(songMeta.LoopTo);
            
            if (Updated != null)
            {
                Updated.Invoke();
            }
        }
        
        private void UpdateMusicalTime()
        {
            if (Music.time < nextBeatTime)
            {
                return;
            }
            
            int beat = MusicalTime.Beat + 1;
            MusicalTime = beat > MeasureBeatCount ?
                new MusicalTime(MusicalTime.Bar + 1, 1) :
                new MusicalTime(MusicalTime.Bar, beat);
            nextBeatTime = ((MusicalTime.Bar - 1) * MeasureBeatCount + (MusicalTime.Beat - 1) + 1) * BeatDuration;
        }

        private void LoopMusicTo(MusicalTime loopTo)
        {
            if (MusicalTime.CompareTo(songMeta.LoopAt) < 0)
            {
                return;
            }

            int loopToBar = loopTo.Bar;
            int loopToBeat = loopTo.Beat;
            
            MusicalTime = new MusicalTime(loopToBar, loopToBeat);

            float loopToTime = MusicalTimeToTime(loopToBar, loopToBeat, songMeta.Measure.BeatCount, BeatDuration);
            Music.time = loopToTime;
            nextBeatTime = loopToTime + BeatDuration;
        }

        private static float MusicalTimeToTime(int bar, int beat, int beatCount, float beatDuration)
        {
            return ((bar - 1f) * beatCount + beat - 1f) * beatDuration;
        }

        private static bool fastForwarding;

#if UNITY_EDITOR
        private static void AdjustPlaybackSpeed()
        {
            // TODO: Ensure it does not break after looping.
            if (!fastForwarding 
                && MusicalTime.Bar < SequencerEditorController.FastForwardToBar
                && SequencerEditorController.FastForwardSpeed != 1f)
            {
                fastForwarding = true;
                Music.volume = 0.5f;
                Time.timeScale = SequencerEditorController.FastForwardSpeed;
            }
            
            if (fastForwarding && MusicalTime.Bar == SequencerEditorController.FastForwardToBar)
            {
                fastForwarding = false;
                Music.time = MusicalTimeToTime(MusicalTime.Bar, 1, MeasureBeatCount, BeatDuration);
                Music.volume = 1f;
                Time.timeScale = SequencerEditorController.PlaybackSpeed;
            }

            if (fastForwarding)
            {
                xMusic.time += Time.deltaTime * (Time.timeScale - 1f);
            }
        }
#endif
    }
}