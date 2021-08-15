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
    [Serializable]
    public struct Music
    {
        [SerializeField] private string name;
        [SerializeField] private float bpm;
        [SerializeField] private Measure measure;
        [SerializeField] private MusicalTime length;
        [SerializeField] private MusicPart[] parts;

        public string Name { get { return name; } }
        public float Bpm { get { return bpm; } }
        public Measure Measure { get { return measure; } }
        public MusicalTime Length { get { return length; } }
        public MusicPart[] Parts { get { return parts; } }
    }
    [Serializable]
    public struct MusicPart
    {
        [SerializeField] private string name;
        [SerializeField] private MusicRegion[] regions;

        public string Name { get { return name; } }
        public MusicRegion[] Regions { get { return regions; } }
    }
    [Serializable]
    public struct MusicRegion
    {
        [SerializeField] private string name;
        [SerializeField] private MusicalTime start;

        public string Name { get { return name; } }
        public MusicalTime Start { get { return start; } }
    }
}