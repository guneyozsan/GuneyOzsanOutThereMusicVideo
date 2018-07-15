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

// Displays the current time, measure and song part for tracking our place in the song and debugging.
# if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour {

    [SerializeField]
    bool displayGui;
    [SerializeField]
    [Range(0, 5)]
    float playbackSpeed;
    [Header("Fast Forward")]
    [SerializeField]
    bool fastForwardEnabled;
    [SerializeField]
    int fastForwardToBar;
    [SerializeField]
    [Range(0, 5)]
    float fastForwardSpeed;

    public static float PlaybackSpeed { get; private set; }

    public static bool DoFastForward { get; private set; }
    public static float FastForwardToBar { get; private set; }
    public static float FastForwardSpeed { get; private set; }

    void Start()
    {
        FastForwardToBar = fastForwardToBar;
        FastForwardSpeed = fastForwardSpeed;
    }

    void Update()
    {
        PlaybackSpeed = playbackSpeed;

        if (fastForwardEnabled)
            FastForwardSpeed = fastForwardSpeed;
        else
            FastForwardSpeed = 1;
    }

    void OnGUI()
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
}
#endif // UNITY_EDITOR