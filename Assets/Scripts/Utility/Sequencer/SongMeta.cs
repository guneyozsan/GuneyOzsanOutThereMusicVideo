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
using System.Collections.Generic;
using PostIllusions.Audio.Music;
using UnityEngine;

namespace PostIllusions.Audio.Sequencer
{
    [CreateAssetMenu(fileName = "SongMeta", menuName = "ScriptableObjects/SongMeta")]
    [Serializable]
    public class SongMeta : ScriptableObject
    {
        [SerializeField] private string songName;
        [SerializeField] private float bpm;
        [SerializeField] private Measure measure;
        [SerializeField] private MusicalTime length;
        [SerializeField] private List<SongPartMeta> parts;

        public string SongName { get { return songName; } }
        public float Bpm { get { return bpm; } }
        public Measure Measure { get { return measure; } }
        public MusicalTime Length { get { return length; } }
        public List<SongPartMeta> Parts { get { return parts; } }
    }
}