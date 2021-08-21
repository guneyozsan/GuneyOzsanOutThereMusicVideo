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
        [SerializeField] private int bar;
        [SerializeField] private int beat;

        /// <summary>
        /// Creates a musical time with given measure.
        /// </summary>
        public MusicalTime(int bar, int beat)
        {
            this.bar = bar;
            this.beat = beat;
        }

        public int Bar { get { return bar; } }
        public int Beat { get { return beat; } }

        /// <summary>
        /// Increments beat number by 1.
        /// </summary>
        public void IncrementBeat(int beatsPerMeasure)
        {
            beat = beat % beatsPerMeasure + 1;
            
            if (beat == 1)
            {
                bar += 1;
            }
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