using System;
using System.Collections.Generic;
using UnityEngine;

namespace PostIllusions.Audio.Sequencer
{
    [Serializable]
    public class SongPartMeta
    {
        [SerializeField] private string name;
        [SerializeField] private List<SongRegionMeta> regions;

        public string Name { get { return name; } }
        public List<SongRegionMeta> Regions { get { return regions; } }
    }
}