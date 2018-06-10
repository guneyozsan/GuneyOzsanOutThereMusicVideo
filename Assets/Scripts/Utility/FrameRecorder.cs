using UnityEngine;
using System.Collections;

// Based on https://docs.unity3d.com/ScriptReference/Time-captureFramerate.html

// Capture frames as a screenshot sequence. Images are
// stored as PNG files in a folder - these can be combined into
// a movie using image utility software (eg, QuickTime Pro).

public class FrameRecorder : MonoBehaviour
{
    [SerializeField]
    bool enabled;

    [SerializeField]
    string folder = "CapturedRecordings";

    [SerializeField]
    int frameRate = 30;

    void Start()
    {
        if (enabled)
        {
            // Set the playback framerate (real time will not relate to game time after this).
            Time.captureFramerate = frameRate;
            System.IO.Directory.CreateDirectory(folder);
        }
    }

    void Update()
    {
        if (enabled)
        {
            string name = string.Format("{0}/Fame-{1:D04}.png", folder, Time.frameCount);
            ScreenCapture.CaptureScreenshot(name);
        }
    }
}