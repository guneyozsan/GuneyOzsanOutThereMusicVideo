﻿// PI Sequencer - Audio sequencer for Unity 3D
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
    /// Represents a musical measure (Example: 3/4, 4/4, 6/8...etc.).
    /// </summary>
    public struct Measure
    {
        /// <summary>
        /// Construct a measure in standard musical format ("3/4", "4/4", "6/8"...etc.).
        /// </summary>
        /// <param name="measure">"3/4", "4/4", "6/8"...etc.</param>
        public Measure(string measure) : this()
        {
            int slashIndex = measure.IndexOf("/", StringComparison.Ordinal);
            BeatCount = int.Parse(measure.Substring(0, slashIndex));
            BeatNoteValue = int.Parse(measure.Substring(slashIndex + 1, measure.Length - (slashIndex + 1)));
        }

        /// <summary>
        /// Construct a measure by explicitly defining count and noteValue.
        /// </summary>
        /// <param name="beatCount">Count of notes. For example "3" in standard notation "3/4".</param>
        /// <param name="beatNoteValue">Musical note value where 1 is a whole note. For example "4" in standard notation "3/4".</param>
        public Measure(int beatCount, int beatNoteValue) : this()
        {
            BeatCount = beatCount;
            BeatNoteValue = beatNoteValue;
        }

        /// <summary>
        /// Count of beats with <see cref="BeatNoteValue"/> in a single bar. For example "3" in standard notation "3/4".
        /// </summary>
        public int BeatCount { get; private set; }

        /// <summary>
        /// Note value of the beat where 1 is a whole note. For example "4" in standard notation "3/4".
        /// </summary>
        public int BeatNoteValue { get; private set; }

        public string GetMeasure()
        {
            return BeatCount + "/" + BeatNoteValue;
        }
    }
}