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

namespace PostIllusions.Audio.Music
{
    /// <summary>
    /// Musical measure.
    /// </summary>
    public struct Measure
    {
        /// <summary>
        /// Construct a measure in standart musical format ("3/4", "4/4", "6/8"...etc.).
        /// </summary>
        /// <param name="measure">"3/4", "4/4", "6/8"...etc.</param>
        public Measure(string measure)
        {
            int slashIndex = measure.IndexOf("/");
            Count = int.Parse(measure.Substring(0, slashIndex));
            NoteValue = int.Parse(measure.Substring(slashIndex + 1, measure.Length - (slashIndex + 1)));
        }

        /// <summary>
        /// Construct a measure by explicitly defining count and noteValue.
        /// </summary>
        /// <param name="count">Count of notes. For example "3" in standart notation "3/4".</param>
        /// <param name="noteValue">Note value where 1 is a whole note. For example "4" in standart notation "3/4".</param>
        public Measure(int count, int noteValue)
        {
            Count = count;
            NoteValue = noteValue;
        }

        /// <summary>
        /// Count of notes. For example "3" in standart notation "3/4".
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Note value where 1 is a whole note. For example "4" in standart notation "3/4".
        /// </summary>
        public int NoteValue { get; private set; }

        public string GetMeasure()
        {
            return (Count.ToString() + "/" + NoteValue.ToString());
        }
    }
}