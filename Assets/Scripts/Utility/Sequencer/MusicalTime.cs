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
    public struct MusicalTime
    {
        [SerializeField] private int bar;
        [SerializeField] private int beat;

        public MusicalTime(int bar, int beat)
        {
            this.bar = bar;
            this.beat = beat;
        }

        public int Bar { get { return bar; } }
        public int Beat { get { return beat; } }

        // TODO: Replace with <, =, > operator overloads.
        /// <summary>
        /// <para>Compares musical times down to Beat being the lowest resolution.</para>
        /// </summary>
        /// <param name="targetTime">Musical Time to compare.</param>
        /// <returns><para>Greater than zero: This instance time is larger.</para>
        /// <para>Less than zero: Target time is larger.</para>
        /// <para>Zero: Equal down to Beat.</para></returns>
        public int CompareTo(MusicalTime targetTime)
        {
            if (bar > targetTime.Bar)
            {
                return 1;
            }

            if (bar < targetTime.Bar)
            {
                return -1;
            }

            if (beat > targetTime.Beat)
            {
                return 1;
            }

            if (beat < targetTime.Beat)
            {
                return -1;
            }

            return 0;
        }
    }
}