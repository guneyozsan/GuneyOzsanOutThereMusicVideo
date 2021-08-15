// Guney Ozsan - Out There (Music Video) - Real time procedural music video in demoscene style for Unity 3D.
// Copyright (C) 2017 Guney Ozsan

// This program is free software: you can redistribute it and/or modify
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

/// <summary>
/// Displays the current time, measure and song region for tracking current position in the song.
/// </summary>
public class SequencerEditorUi : MonoBehaviour
{
    [SerializeField] private bool displayGui;

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (displayGui)
        {
            GUI.Label(
                new Rect(10f, 10f, 200f, 100f),
                "Bar:   " + Sequencer.CurrentBar + ":" + Sequencer.CurrentBeat
                + "      Time:   " + (int)(Sequencer.Time * 1000f) + " ms" + Environment.NewLine
                + "-------------------------------------------" + Environment.NewLine
                + "Part:       " + Sequencer.CurrentPart + Environment.NewLine
                + Sequencer.CurrentRegionDescription
            );
        }
    }
#endif
}
