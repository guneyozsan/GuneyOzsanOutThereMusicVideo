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

namespace PostIllusions.Audio.Sequencer
{
    using System;

    [Serializable]
    public struct MusicTime
    {
        public MusicTime(int bpm)
        {
            Bpm         = bpm;
            Bar         = 1;
            Beat        = 1;
            Miliseconds = 0;
        }

        public  int     Bpm { get; private set; }
        public  int     Bar { get; private set; }
        public  int     Beat { get; private set; }
        public  float   Miliseconds { get; private set; }
        public  double  BeatDuration { get { return 60d / Bpm; } }

        public void AddBeat(int value)
        {
            Beat += value;
        }

        public void AddBar(int value)
        {
            Bar += value;
        }

        public void SetBeat(int value)
        {
            Beat = value;
        }

        public void AddTime(float value)
        {
            Miliseconds += value;
        }

        public void SetTime(float value)
        {
            Miliseconds = value;
        }

        public void Reset()
        {
            Bar         = 1;
            Beat        = 1;
            Miliseconds = 0f;
        }
    }
}
