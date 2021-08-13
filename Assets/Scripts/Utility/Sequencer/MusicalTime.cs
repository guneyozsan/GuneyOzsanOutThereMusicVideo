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
using UnityEngine;

namespace PostIllusions.Audio.Music
{
    /// <summary>
    /// Represents a moment in music domain with given measure in terms of bars and beats.
    /// </summary>
    [Serializable]
    public class MusicalTime
    {
        [SerializeField] protected int bar;
        [SerializeField] protected int beat;

        /// <summary>
        /// Creates a musical time with given measure.
        /// </summary>
        /// <param name="measure"></param>
        public MusicalTime(Measure measure)
        {
            Measure = measure;
            bar = 1;
            beat = 1;
        }

        public Measure Measure { get; protected set; }
        public int Bar { get { return bar; } }
        public int Beat { get { return beat; } }

        /// <summary>
        /// Increments beat number by 1.
        /// </summary>
        public void IncrementBeat()
        {
            beat = beat == Measure.BeatCount ? 1 : beat + 1;
        }

        /// <summary>
        /// Decrements beat number by 1.
        /// </summary>
        public void DecrementBeat()
        {
            beat = beat == 1 ? Measure.BeatCount : beat - 1;
        }

        /// <summary>
        /// Sets the current bar.
        /// </summary>
        /// <param name="value"></param>
        public void SetBar(int value)
        {
            bar = value;
        }

        /// <summary>
        /// Sets the current beat.
        /// </summary>
        /// <param name="value"></param>
        public void SetBeat(int value)
        {
            beat = value;
        }

        /// <summary>
        /// Adds time in bars.
        /// </summary>
        /// <param name="value"></param>
        public void AddBar(int value)
        {
            bar += value;
        }

        /// <summary>
        /// <para>Compares musical times down to Beat being the lowest resolution.</para>
        /// </summary>
        /// <param name="musicalTime">Musical Time to compare.</param>
        /// <returns><para>Greater than zero: This instance time is larger.</para>
        /// <para>Less than zero: Obj time is larger.</para>
        /// <para>Zero: Equal down to Beat.</para></returns>
        public int CompareTo(MusicalTime musicalTime)
        {
            if (bar > musicalTime.Bar)
            {
                return 1;
            }

            if (bar < musicalTime.Bar)
            {
                return -1;
            }

            if (beat > musicalTime.Beat)
            {
                return 1;
            }

            if (beat < musicalTime.Beat)
            {
                return -1;
            }

            return 0;
        }
    }
}