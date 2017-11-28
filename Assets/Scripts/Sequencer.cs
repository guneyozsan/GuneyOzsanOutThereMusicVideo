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

    public static Part CurrentPart { get; private set; }
    public static string CurrentRegionDescription { get; private set; }

    public enum Part { Intro, Part1Approach, Part2Probe };

#endif
    public static int CurrentBeat { get; private set; }
    public static int CurrentBar { get; private set; }

    int BPM;
    public static double BeatDuration { get; private set; }
    public static double BarDuration {
        get
        {
            return 4 * BeatDuration;
        }
    }
    public static float BarDurationF
    {
        get
        {
            return 4 * (float)BeatDuration;
        }
    }

    int loopToBar;

    void Start()
    {
        BPM = 77;
        BeatDuration = 60d / BPM;

        loopToBar = 60;

        music = GetComponent<AudioSource>();
        music.time = 0;
        music.Play();
#if UNITY_EDITOR
        MusicDebug = music;
#endif

        CurrentBar = 1;
        CurrentBeat = 1;
    }



    void Update()
    {
#if UNITY_EDITOR
        AdjustPlaybackSpeed();
        SetCurrentRegion();
#endif
        SetBeats();
        LoopMusicTo(loopToBar);
    }


#if UNITY_EDITOR
    void AdjustPlaybackSpeed()
    {
        if (CurrentBar < Debugging.FastForwardToBar && Debugging.FastForwardSpeed != 1)
        {
            music.volume = 0;
            Time.timeScale = Debugging.FastForwardSpeed;
        }
        else
        {
            music.volume = 1;
            Time.timeScale = Debugging.PlaybackSpeed;
        }

        float musicTimeCorrection = Time.deltaTime * (Time.timeScale - 1);
        bool firstCorrectionResultsInNegativeTime = music.time + musicTimeCorrection < 0;
        if (Time.timeScale != 1 && !firstCorrectionResultsInNegativeTime)
        {
            music.time += musicTimeCorrection;
        }
    }
#endif


#if UNITY_EDITOR
    void SetCurrentRegion()
    {
        if (music.time < 9.350)
        {
            CurrentPart = Part.Intro;
            CurrentRegionDescription = "wind intro";
        }
        else if (music.time < 15.584)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "explosion";
        }
        else if (music.time < 21.818)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "ping sound!";
        }
        else if (music.time < 46.753)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "musical base";
        }
        else if (music.time < 96.623)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "melody";
        }
        else if (music.time < 121.558)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "bass";
        }
        else if (music.time < 146.493)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "hihat and full bass";
        }
        else if (music.time < 171.428)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "bass syncopation";
        }
        else if (music.time < 183.896)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "Part 1 to 2 bridge";
        }
        else if (music.time < 233.766)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "A: musical base";
        }
        else if (music.time < 258.701)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "A: melody";
        }
        else if (music.time < 283.636)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "AB bridge";
        }
        else if (music.time < 308.571)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "B: musical base";
        }
        else if (music.time < 333.506)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "B: melody";
        }
        else if (music.time < 358.441)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "AB bridge";
        }
        else if (music.time < 383.376)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "B: melody";
        }
        else if (music.time < 408.311)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "AB bridge";
        }
        else if (music.time < 433.246)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "A: melody";
        }
        else if (music.time < 458.181)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "AB bridge";
        }
        else if (music.time < 483.116)
        {
            CurrentPart = Part.Part2Probe;
            CurrentRegionDescription = "Part 2 to 1 bridge";
        }
        else if (music.time < 508.051)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "Part 1 rhythm + melody + hihat";
        }
        else if (music.time < 532.986)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "Part 1 rhythm + melody";
        }
        else if (music.time < 557.922)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "Part 1 melody + bass";
        }
        else if (music.time < 582.857)
        {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "Part 1 melody + bass + hihat";
        }
        else
        { // if (music.time < 595.324) {
            CurrentPart = Part.Part1Approach;
            CurrentRegionDescription = "Part 1 to 2 bridge";
        }
    }
#endif


    void SetBeats()
    {
        if (music.time > ((CurrentBar - 1) * 4 + CurrentBeat) * BeatDuration)
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


    void LoopMusicTo(int loopToBar)
    {
        if (music.time >= 595.324)
        {
            music.time = (float)((loopToBar - 1d) * 4d * BeatDuration);
            music.Play();
            CurrentBar = loopToBar;

            CurrentBeat = 1;
        }
    }
}
