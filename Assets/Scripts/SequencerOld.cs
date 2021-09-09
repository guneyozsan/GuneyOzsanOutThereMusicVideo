// // Guney Ozsan - Out There (Music Video) - Real time procedural music video in demoscene style for Unity 3D.
// // Copyright (C) 2017 Guney Ozsan
//
// // This program is free software: you can redistribute it and/or modify
// // it under the terms of the GNU General Public License as published by
// // the Free Software Foundation, either version 3 of the License, or
// // (at your option) any later version.
//
// // This program is distributed in the hope that it will be useful,
// // but WITHOUT ANY WARRANTY; without even the implied warranty of
// // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// // GNU General Public License for more details.
//
// // You should have received a copy of the GNU General Public License
// // along with this program.  If not, see <http://www.gnu.org/licenses/>.
// // ---------------------------------------------------------------------
//
// using UnityEngine;
//
// public class SequencerOld : MonoBehaviour
// {
//     public static float Time { get; private set; }
//
// #if UNITY_EDITOR
//     public static Part CurrentPart { get; private set; }
//     public static string CurrentRegionDescription { get; private set; }
// #endif // UNITY_EDITOR
//
// #if UNITY_EDITOR
//     public enum Part
//     {
//         Intro,
//         Part1Approach,
//         Part2Probe
//     };
// #endif // UNITY_EDITOR
//
//     public static int CurrentBeat { get; private set; }
//     public static int CurrentBar { get; private set; }
//
//     public static double BeatDuration { get; private set; }
//     public static float BarDuration
//     {
//         get
//         {
//             return 4 * (float)BeatDuration;
//         }
//     }
//
//     int bpm;
//     int loopToBar;
//
//     void Start()
//     {
//         bpm = 77;
//         BeatDuration = 60d / bpm;
//
//         loopToBar = 60;
//
//         Time = 0f;
//
//         CurrentBar = 1;
//         CurrentBeat = 1;
//     }
//
//     void Update()
//     {
//         // Syncs time with music time.
//         //if (UnityEngine.Time.frameCount != 1 && UnityEngine.Time.frameCount != 2)
//         //    Time += UnityEngine.Time.deltaTime;
//
// #if UNITY_EDITOR
//         //AdjustPlaybackSpeed();
//         SetCurrentRegion();
// #endif // UNITY_EDITOR
//
//         //SetBeats();
//         //LoopMusicTo(loopToBar);
//     }
//     
// // #if UNITY_EDITOR
// //     void AdjustPlaybackSpeed()
// //     {
// //         if (CurrentBar < SequencerEditorController.FastForwardToBar
// //             && SequencerEditorController.FastForwardSpeed != 1)
// //         {
// //             music.volume = 0;
// //             UnityEngine.Time.timeScale = SequencerEditorController.FastForwardSpeed;
// //         }
// //         else
// //         {
// //             music.volume = 1;
// //             UnityEngine.Time.timeScale = SequencerEditorController.PlaybackSpeed;
// //         }
// //     }
// // #endif // UNITY_EDITOR
//
// #if UNITY_EDITOR
//     void SetCurrentRegion()
//     {
//         if (Time < 9.350)
//         {
//             CurrentPart = Part.Intro;
//             CurrentRegionDescription = "wind intro";
//         }
//         else if (Time < 15.584)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "explosion";
//         }
//         else if (Time < 21.818)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "ping sound!";
//         }
//         else if (Time < 46.753)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "musical base";
//         }
//         else if (Time < 96.623)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "melody";
//         }
//         else if (Time < 121.558)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "bass";
//         }
//         else if (Time < 146.493)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "hihat and full bass";
//         }
//         else if (Time < 171.428)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "bass syncopation";
//         }
//         else if (Time < 183.896)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "Part 1 to 2 bridge";
//         }
//         else if (Time < 233.766)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "A: musical base";
//         }
//         else if (Time < 258.701)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "A: melody";
//         }
//         else if (Time < 283.636)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "AB bridge";
//         }
//         else if (Time < 308.571)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "B: musical base";
//         }
//         else if (Time < 333.506)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "B: melody";
//         }
//         else if (Time < 358.441)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "AB bridge";
//         }
//         else if (Time < 383.376)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "B: melody";
//         }
//         else if (Time < 408.311)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "AB bridge";
//         }
//         else if (Time < 433.246)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "A: melody";
//         }
//         else if (Time < 458.181)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "AB bridge";
//         }
//         else if (Time < 483.116)
//         {
//             CurrentPart = Part.Part2Probe;
//             CurrentRegionDescription = "Part 2 to 1 bridge";
//         }
//         else if (Time < 508.051)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "Part 1 rhythm + melody + hihat";
//         }
//         else if (Time < 532.986)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "Part 1 rhythm + melody";
//         }
//         else if (Time < 557.922)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "Part 1 melody + bass";
//         }
//         else if (Time < 582.857)
//         {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "Part 1 melody + bass + hihat";
//         }
//         else
//         { // if (time < 595.324) {
//             CurrentPart = Part.Part1Approach;
//             CurrentRegionDescription = "Part 1 to 2 bridge";
//         }
//     }
// #endif // UNITY_EDITOR
//
// //     void SetBeats()
// //     {
// //         double timeOfCurrentBeat = ((CurrentBar - 1) * 4 + CurrentBeat) * BeatDuration;
// //
// // #if UNITY_EDITOR
// //         timeOfCurrentBeat -= BeatDuration * (1 - 1/ UnityEngine.Time.timeScale);
// // #endif // UNITY_EDITOR
// //
// //         if (Time > timeOfCurrentBeat)
// //         {
// //             if (CurrentBeat < 4)
// //             {
// //                 CurrentBeat++;
// //             }
// //             else
// //             {
// //                 CurrentBeat = 1;
// //                 CurrentBar++;
// //             }
// //
// // #if UNITY_EDITOR
// //             if (UnityEngine.Time.timeScale != 1)
// //                 Time = (float)(timeOfCurrentBeat + BeatDuration * (1 - 1 / UnityEngine.Time.timeScale));
// // #endif // UNITY_EDITOR
// //
// //         }
// //     }
//
//     // void LoopMusicTo(int loopToBar)
//     // {
//     //     if (Time >= 595.324)
//     //     {
//     //         music.time = (float)((loopToBar - 1d) * 4d * BeatDuration);
//     //         Time = music.time;
//     //         music.Play();
//     //         CurrentBar = loopToBar;
//     //
//     //         CurrentBeat = 1;
//     //     }
//     // }
// }
