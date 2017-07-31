// Guney Ozsan - Out There (Music Video) - Real time music video in demoscene style for Unity 3D.
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    AudioSource music;
#if UNITY_EDITOR
    [NonSerialized]
    public AudioSource musicDebug;
#endif

    [NonSerialized]
    public int currentRegionId;
#if UNITY_EDITOR
    [NonSerialized]
    public CurrentPart currentPart;
    [NonSerialized]
    public string currentRegionDescription;

    public enum CurrentPart { Intro, Part1Probe, Part2Approach };

    [NonSerialized]
    public int currentBeat;
#endif
    [NonSerialized]
    public int currentBar;

    int BPM;
    double beatDuration;

    int loopToBar;

#if UNITY_EDITOR
    public int fastForwardToBar;
    int fastForwardSpeed;
    bool doFastForward;
#endif


    void Start()
    {
#if UNITY_EDITOR
        // InitializeFastForward();
#endif
        BPM = 77;
        beatDuration = 60d / BPM;

        loopToBar = 60;

        music = GetComponent<AudioSource>();
        //music.time = (fastForwardToBar - 1)*4*beatDuration;
        music.time = 0;
        music.Play();
#if UNITY_EDITOR
        musicDebug = music;
#endif

        currentBar = 1;
        currentBeat = 1;
    }


    void Update()
    {
        if (doFastForward)
        {
            FastForward();
        }

        SetCurrentRegion();
        SetBeats();
        LoopMusicTo(loopToBar);
    }

#if UNITY_EDITOR
    void InitializeFastForward()
    {
        throw new NotImplementedException();
        doFastForward = true;

        fastForwardToBar = 4;
        fastForwardSpeed = 3;

        if (fastForwardToBar <= 1 || fastForwardToBar > 191)
        {
            fastForwardToBar = 1;
            doFastForward = false;
        }
    }


    void FastForward()
    {
        throw new NotImplementedException();
        //Debug.Log(Time.timeScale + " " + doFastForward);
        if (currentBar < fastForwardToBar && Time.timeScale != fastForwardSpeed)
        {
            Time.timeScale = fastForwardSpeed;
        }
        else
        {
            doFastForward = false;
            Time.timeScale = 1;
        }
    }
#endif


    void SetCurrentRegion()
    {
        if (music.time < 9.350)
        {
            currentRegionId = 1;
#if UNITY_EDITOR
            currentPart = CurrentPart.Intro;
            currentRegionDescription = "wind intro";
#endif
        }
        else if (music.time < 15.584)
        {
            currentRegionId = 2;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "explosion";
#endif
        }
        else if (music.time < 21.818)
        {
            currentRegionId = 3;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "ping sound!";
#endif
        }
        else if (music.time < 46.753)
        {
            currentRegionId = 4;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "musical base";
#endif
        }
        else if (music.time < 96.623)
        {
            currentRegionId = 5;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "melody";
#endif
        }
        else if (music.time < 121.558)
        {
            currentRegionId = 6;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "bass";
#endif
        }
        else if (music.time < 146.493)
        {
            currentRegionId = 7;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "hihat and full bass";
#endif
        }
        else if (music.time < 171.428)
        {
            currentRegionId = 8;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "bass syncopation";
#endif
        }
        else if (music.time < 183.896)
        {
            currentRegionId = 9;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "Part 1 to 2 bridge";
#endif
        }
        else if (music.time < 233.766)
        {
            currentRegionId = 10;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "A: musical base";
#endif
        }
        else if (music.time < 258.701)
        {
            currentRegionId = 11;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "A: melody";
#endif
        }
        else if (music.time < 283.636)
        {
            currentRegionId = 12;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 308.571)
        {
            currentRegionId = 13;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "B: musical base";
#endif
        }
        else if (music.time < 333.506)
        {
            currentRegionId = 14;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "B: melody";
#endif
        }
        else if (music.time < 358.441)
        {
            currentRegionId = 15;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 383.376)
        {
            currentRegionId = 16;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "B: melody";
#endif
        }
        else if (music.time < 408.311)
        {
            currentRegionId = 17;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 433.246)
        {
            currentRegionId = 18;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "A: melody";
#endif
        }
        else if (music.time < 458.181)
        {
            currentRegionId = 19;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 483.116)
        {
            currentRegionId = 20;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part2Approach;
            currentRegionDescription = "Part 2 to 1 bridge";
#endif
        }
        else if (music.time < 508.051)
        {
            currentRegionId = 21;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "Part 1 rhythm + melody + hihat";
#endif
        }
        else if (music.time < 532.986)
        {
            currentRegionId = 22;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "Part 1 rhythm + melody";
#endif
        }
        else if (music.time < 557.922)
        {
            currentRegionId = 23;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "Part 1 melody + bass";
#endif
        }
        else if (music.time < 582.857)
        {
            currentRegionId = 24;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "Part 1 melody + bass + hihat";
#endif
        }
        else
        { // if (music.time < 595.324) {
            currentRegionId = 25;
#if UNITY_EDITOR
            currentPart = CurrentPart.Part1Probe;
            currentRegionDescription = "Part 1 to 2 bridge";
#endif
        }
    }


#if UNITY_EDITOR
    void SetBeats()
    {
        if (music.time > ((currentBar - 1) * 4 + currentBeat) * beatDuration)
        {
            if (currentBeat < 4)
            {
                currentBeat++;
            }
            else
            {
                currentBeat = 1;
                currentBar++;
            }
        }
    }
#endif


    void LoopMusicTo(int loopToBar)
    {
        if (music.time >= 595.324)
        {
            music.time = (float)((loopToBar - 1d) * 4d * beatDuration);
            music.Play();
            currentBar = this.loopToBar;

#if UNITY_EDITOR
            currentBeat = 1;
#endif
        }
    }
}
