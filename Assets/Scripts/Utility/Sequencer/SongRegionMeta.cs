using System;
using PostIllusions.Audio.Music;
using UnityEngine;

namespace PostIllusions.Audio.Sequencer
{
    [Serializable]
    public class SongRegionMeta
    {
        [SerializeField] private string name;
        [SerializeField] private MusicalTime start;

        public string Name { get { return name; } }
        public MusicalTime Start { get { return start; } }
    }
}