// Audio Sequencer - An audio sequencer tool for Unity 3D
// Copyright (C) 2018 Guney Ozsan

// This file is part of Audio Sequencer.

// Audio Sequencer is free software: you can redistribute it and/or modify
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PostIllusions.Audio
{
    public partial class Sequencer : MonoBehaviour
    {
        public static   MusicTime   MusicTime { get; private set; }
        
        [SerializeField]
        private         float       bpm;
        public          float       Bpm { get { return bpm; } }

        private         bool        isPlaying;

        private void Update()
        {
            if (isPlaying)
                MusicTime.AddTime(Time.deltaTime);
        }

        public void Play()
        {
            isPlaying = true;
        }

        public void Pause()
        {
            isPlaying = false;
        }

        public void Stop()
        {
            isPlaying = false;
            MusicTime.Reset();
        }

        void SetBeats()
        {
            double timeOfCurrentBeat = ((MusicTime.Bar - 1) * 4 + MusicTime.Beat) * BeatDuration;

#if UNITY_EDITOR
            timeOfCurrentBeat -= BeatDuration * (1 - 1 / Time.timeScale);
#endif // UNITY_EDITOR

            if (MusicTime.Miliseconds > timeOfCurrentBeat)
            {
                if (MusicTime.Beat < 4)
                {
                    MusicTime.AddBeat(1);
                }
                else
                {
                    MusicTime.SetBeat(1);
                    MusicTime.AddBar(1);
                }

#if UNITY_EDITOR
                if (UnityEngine.Time.timeScale != 1)
                    MusicTime.SetMiliseconds((float)(timeOfCurrentBeat + BeatDuration * (1 - 1 / Time.timeScale)));
#endif // UNITY_EDITOR

            }
        }
    }
}
