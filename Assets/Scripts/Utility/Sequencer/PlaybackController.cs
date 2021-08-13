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

namespace PostIllusions.Audio.Sequencer
{
    public class PlaybackController : MonoBehaviour
    {
        public event Action<PlaybackState> PlaybackStateSet;

        public void Play()
        {
            OnSetPlaybackState(PlaybackState.Play);
        }

        public void Pause()
        {
            OnSetPlaybackState(PlaybackState.Pause);
        }

        public void Stop()
        {
            OnSetPlaybackState(PlaybackState.Stop);
        }

        private void OnSetPlaybackState(PlaybackState state)
        {
            if (PlaybackStateSet != null) PlaybackStateSet(state);
        }
    }
}