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
    /// Represents a moment in music domain with given measure in terms of bars and beats.
    /// </summary>
    public class MusicalTime
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="measure"></param>
        public MusicalTime(Measure measure)
        {
            Measure = measure;
            Bar = 1;
            Beat = 1;
        }

        public Measure Measure { get; protected set; }
        public int Bar { get; protected set; }
        public int Beat { get; protected set; }

        /// <summary>
        /// Increases beat number by 1.
        /// </summary>
        public void IncrementBeat()
        {
            if (Beat == Measure.BeatCount)
            {
                Beat = 1;
            }
            else
            {
                Beat++;
            }
        }

        /// <summary>
        /// Decreases beat number by 1.
        /// </summary>
        public void DecrementBeat()
        {
            if (Beat == 1)
            {
                Beat = Measure.BeatCount;
            }
            else
            {
                Beat--;
            }
        }

        /// <summary>
        /// Sets the current bar.
        /// </summary>
        /// <param name="value"></param>
        public void SetBar(int value)
        {
            Bar = value;
        }

        /// <summary>
        /// Sets the current beat.
        /// </summary>
        /// <param name="value"></param>
        public void SetBeat(int value)
        {
            Beat = value;
        }

        /// <summary>
        /// Adds time in bars.
        /// </summary>
        /// <param name="value"></param>
        public void AddBar(int value)
        {
            Bar += value;
        }

        /// <summary>
        /// <para>Compares musical times down to Beat being the lowest resolution.</para>
        /// </summary>
        /// <param name="obj">Musical Time to compareç</param>
        /// <returns><para>Greater than zero: This instance time is larger.</para>
        /// <para>Less than zero: Obj time is larger.</para>
        /// <para>Zero: Equal down to Beat.</para></returns>
        public int CompareTo(MusicalTime obj)
        {
            if (Bar > obj.Bar)
            {
                return 1;
            }
            else if (Bar < obj.Bar)
            {
                return -1;
            }
            else
            {
                if (Beat > obj.Beat)
                {
                    return 1;
                }
                else if (Beat < obj.Beat)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}