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
        [SerializeField] private SongMeta songMeta;

        private AudioSource music;
        private MusicalAudioTime musicalTime;

        public static event Action<MusicalAudioTime> BeatUpdated;

        private void Awake()
        {
            musicalTime = new MusicalAudioTime(songMeta.Measure, songMeta.Bpm);

            // TODO: Ensure only one object exists in scene.
            music = FindObjectOfType<AudioSource>();
            music.time = 0f;
            music.Play();
        }

        private void Update()
        {
            // TODO: Check if this is still necessary.
            // Syncs time with music time.
            if (Time.frameCount != 1 && Time.frameCount != 2)
                Time += Time.deltaTime;
            
#if UNITY_EDITOR
            AdjustPlaybackSpeed();
#endif
            
            musicalTime.AddTime(Time.deltaTime);
            SetBeats();
            LoopMusicTo(songMeta.LoopTo);
        }
        
        private void SetBeats()
        {
            float currentBeatTime = ((musicalTime.Bar - 1) * songMeta.Measure.BeatCount + 
                musicalTime.Beat) * musicalTime.BeatDuration;

#if UNITY_EDITOR
            // Adjust music time to playback speed.
            if (Time.timeScale != 1)
            {
                currentBeatTime -= musicalTime.BeatDuration * (1f - 1f / Time.timeScale);
            }
#endif // UNITY_EDITOR

            if (musicalTime.Time <= currentBeatTime)
            {
                return;
            }

            musicalTime.IncrementBeat(songMeta.Measure.BeatCount);

#if UNITY_EDITOR
            // Adjust music time to playback speed.
            if (Time.timeScale != 1f)
            {
                musicalTime.SetTime((currentBeatTime + musicalTime.BeatDuration * (1f - 1f / Time.timeScale)));
            }
#endif // UNITY_EDITOR

            if (BeatUpdated != null)
            {
                BeatUpdated(musicalTime);
            }
        }

        private void LoopMusicTo(MusicalTime loopToTime)
        {
            if (musicalTime.Time >= (songMeta.Length.Bar * songMeta.Measure.BeatCount + songMeta.Length.Beat) * musicalTime.BeatDuration)
            {
                music.time = ((loopToTime.Bar - 1f) * songMeta.Measure.BeatCount + loopToTime.Beat) * musicalTime.BeatDuration;
                musicalTime.SetTime(music.time);
                music.Play();
                
                CurrentBar = loopToBar;

                CurrentBeat = 1;
            }
        }
        
#if UNITY_EDITOR
        private void AdjustPlaybackSpeed()
        {
            // TODO: Ensure it does not break after looping.
            if (musicalTime.Bar < SequencerEditorController.FastForwardToBar
                && SequencerEditorController.FastForwardSpeed != 1f)
            {
                music.volume = 0;
                Time.timeScale = SequencerEditorController.FastForwardSpeed;
            }
            else
            {
                music.volume = 1;
                Time.timeScale = SequencerEditorController.PlaybackSpeed;
            }
        }
#endif // UNITY_EDITOR

    }
}