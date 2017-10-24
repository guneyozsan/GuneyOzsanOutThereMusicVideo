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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    AudioSource music;
#if UNITY_EDITOR
    public static AudioSource MusicDebug { get; private set; }
#endif

    public static int CurrentRegionId { get; private set; }
#if UNITY_EDITOR
    public static Part CurrentPart { get; private set; }
    public static string CurrentRegionDescription { get; private set; }

    public enum Part { Intro, Part1Probe, Part2Approach };

    public static int CurrentBeat { get; private set; }
#endif
    public static int CurrentBar { get; private set; }

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
        MusicDebug = music;
#endif

        CurrentBar = 1;
#if UNITY_EDITOR
        CurrentBeat = 1;
#endif
    }



    void Update()
    {
#if UNITY_EDITOR
        if (doFastForward)
        {
            FastForward();
        }

        SetBeats();
#endif
        SetCurrentRegion();
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
#endif



#if UNITY_EDITOR
    void FastForward()
    {
        throw new NotImplementedException();
        //Debug.Log(Time.timeScale + " " + doFastForward);
        if (CurrentBar < fastForwardToBar && Time.timeScale != fastForwardSpeed)
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
            CurrentRegionId = 1;
#if UNITY_EDITOR
            CurrentPart = Part.Intro;
            CurrentRegionDescription = "wind intro";
#endif
        }
        else if (music.time < 15.584)
        {
            CurrentRegionId = 2;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "explosion";
#endif
        }
        else if (music.time < 21.818)
        {
            CurrentRegionId = 3;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "ping sound!";
#endif
        }
        else if (music.time < 46.753)
        {
            CurrentRegionId = 4;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "musical base";
#endif
        }
        else if (music.time < 96.623)
        {
            CurrentRegionId = 5;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "melody";
#endif
        }
        else if (music.time < 121.558)
        {
            CurrentRegionId = 6;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "bass";
#endif
        }
        else if (music.time < 146.493)
        {
            CurrentRegionId = 7;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "hihat and full bass";
#endif
        }
        else if (music.time < 171.428)
        {
            CurrentRegionId = 8;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "bass syncopation";
#endif
        }
        else if (music.time < 183.896)
        {
            CurrentRegionId = 9;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "Part 1 to 2 bridge";
#endif
        }
        else if (music.time < 233.766)
        {
            CurrentRegionId = 10;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "A: musical base";
#endif
        }
        else if (music.time < 258.701)
        {
            CurrentRegionId = 11;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "A: melody";
#endif
        }
        else if (music.time < 283.636)
        {
            CurrentRegionId = 12;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 308.571)
        {
            CurrentRegionId = 13;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "B: musical base";
#endif
        }
        else if (music.time < 333.506)
        {
            CurrentRegionId = 14;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "B: melody";
#endif
        }
        else if (music.time < 358.441)
        {
            CurrentRegionId = 15;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 383.376)
        {
            CurrentRegionId = 16;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "B: melody";
#endif
        }
        else if (music.time < 408.311)
        {
            CurrentRegionId = 17;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 433.246)
        {
            CurrentRegionId = 18;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "A: melody";
#endif
        }
        else if (music.time < 458.181)
        {
            CurrentRegionId = 19;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "AB bridge";
#endif
        }
        else if (music.time < 483.116)
        {
            CurrentRegionId = 20;
#if UNITY_EDITOR
            CurrentPart = Part.Part2Approach;
            CurrentRegionDescription = "Part 2 to 1 bridge";
#endif
        }
        else if (music.time < 508.051)
        {
            CurrentRegionId = 21;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "Part 1 rhythm + melody + hihat";
#endif
        }
        else if (music.time < 532.986)
        {
            CurrentRegionId = 22;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "Part 1 rhythm + melody";
#endif
        }
        else if (music.time < 557.922)
        {
            CurrentRegionId = 23;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "Part 1 melody + bass";
#endif
        }
        else if (music.time < 582.857)
        {
            CurrentRegionId = 24;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "Part 1 melody + bass + hihat";
#endif
        }
        else
        { // if (music.time < 595.324) {
            CurrentRegionId = 25;
#if UNITY_EDITOR
            CurrentPart = Part.Part1Probe;
            CurrentRegionDescription = "Part 1 to 2 bridge";
#endif
        }
    }


#if UNITY_EDITOR
    void SetBeats()
    {
        if (music.time > ((CurrentBar - 1) * 4 + CurrentBeat) * beatDuration)
        {
            if (CurrentBeat < 4)
            {
                CurrentBeat++;
            }
            else
            {
                CurrentBeat = 1;
                CurrentBar++;
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
            CurrentBar = this.loopToBar;

#if UNITY_EDITOR
            CurrentBeat = 1;
#endif
        }
    }
}
