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

namespace PostIllusions.Audio.Music
{
    /// <summary>
    /// Represents a moment of music with constant measure and BPM
    /// in time domain (seconds) and music domain (bars and beats).
    /// <seealso cref="MusicalTime"/>.
    /// </summary>
    public class MusicalAudioTime : MusicalTime
    {
        private float beatDurationSeconds;
        
        /// <summary>
        /// Creates an AudioMusicTime object in time domain with a constant Measure and BPM.
        /// </summary>
        /// <param name="measure">Measure of the music (Example: 3/4, 4/4...etc.). Constant throughout the audio piece.</param>
        /// <param name="bpm">Bpm of the music. Constant throughout the audio piece.</param>
        public MusicalAudioTime(Measure measure, float bpm) : base(1, 1)
        {
            Bpm = bpm;
            Time = 0f;
        }

        /// <summary>
        /// Bpm of the music. Constant throughout the music.
        /// </summary>
        public float Bpm { get; private set; }

        /// <summary>
        /// Current time of music in seconds.
        /// </summary>
        public float Time { get; private set; }

        /// <summary>
        /// Duration of a single beat in seconds.
        /// </summary>
        public float BeatDuration
        {
            get
            {
                if (beatDurationSeconds > 0)
                {
                    return beatDurationSeconds;
                }
                
                beatDurationSeconds = 60f / Bpm;
                return beatDurationSeconds;
            }
        }

        /// <summary>
        /// Returns the bar number of the given audio time.
        /// </summary>
        private static int GetBar(float time)
        {
            // TODO
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns the beat number of the given audio time.
        /// </summary>
        /// <param name="time">Time in seconds.</param>
        /// <returns></returns>
        private static int GetBeat(float time)
        {
            // TODO
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds time to instance time.
        /// </summary>
        /// <param name="value">Time to add in seconds.</param>
        public void AddTime(float value)
        {
            Time += value;
        }

        /// <summary>
        /// Sets the current time of the instance in seconds.
        /// </summary>
        /// <param name="value">Time to set in seconds.</param>
        public void SetTime(float v)
        {
            Time = v;
        }
    }
}