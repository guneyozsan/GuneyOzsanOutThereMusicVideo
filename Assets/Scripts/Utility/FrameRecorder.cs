using UnityEngine;
using System.Collections;
using System.IO;
using System;

// Based on
// https://docs.unity3d.com/ScriptReference/Time-captureFramerate.html
// https://github.com/Chman/FrameCapture/

/// <summary>
/// Captures frames as a PNG screenshot sequence.
/// </summary>
public class FrameRecorder : MonoBehaviour
{
    // Parameters
    [SerializeField]
    Resolution resolution;
    
    [SerializeField, Range(1, 120)]
    int frameRate;

    [SerializeField, Range(1, 16)]
    int samples;

    [SerializeField]
    bool supersample = false;

    [Space]

    [SerializeField]
    bool updateDisplay;

    [SerializeField]
    Camera renderCamera;

    [SerializeField, HideInInspector]
    Shader resolveShader;
    
    // Members
    bool recordingEnabled = false;

    Material resolverMaterial;

    RenderTexture originalRenderTexture;
    RenderTexture originalTargetTexture;

    int frameCount;

    DirectoryInfo outputFolder;

    // Variable cache
    Resolution superResolution;
    RenderTextureFormat outputFormat;
    RenderTextureFormat renderFormat;
    RenderTexture[] renderTargets;
    RenderTexture tempRenderTexture;

    struct Resolution
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public static Resolution operator *(Resolution resolution, float t)
        {
            Resolution r = resolution;
            r.Width = (int)(t * r.Width);
            r.Height = (int)(t * r.Height);
            return r;
        }
    }

    void Start()
    {
        if (!recordingEnabled)
            enabled = false;
    }

    void OnEnable()
    {
        frameCount = 0;
        int minFrameRate = 1;
        int maxFrameRate = 120;
        Time.captureFramerate = Mathf.Clamp(frameRate, minFrameRate, maxFrameRate);

        resolverMaterial = new Material(resolveShader);
        samples = Mathf.Clamp(samples, 1, 16);
        resolverMaterial.SetFloat("_Samples", samples);

        originalRenderTexture = RenderTexture.active;
        originalTargetTexture = renderCamera.targetTexture;

        outputFormat = RenderTextureFormat.ARGB32;
        renderFormat = renderCamera.allowHDR
            ? RenderTextureFormat.ARGBHalf
            : RenderTextureFormat.ARGB32;

        RenderTexture.active = renderTexture;

        renderTargets = new[]
        {
            RenderTexture.GetTemporary(superResolution.Width, superResolution.Height, 24, renderFormat),
            RenderTexture.GetTemporary(superResolution.Width, superResolution.Height, 0, outputFormat),
            RenderTexture.GetTemporary(superResolution.Width, superResolution.Height, 0, outputFormat),
            RenderTexture.GetTemporary(resolution.Width, resolution.Height, 0, outputFormat)
        };

        if (supersample)
            superResolution *= 2;
        else
            superResolution = resolution;

        string outputRoot = new DirectoryInfo(Application.dataPath).Parent.Parent.FullName;
        string date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff");
        outputFolder = new DirectoryInfo(Path.Combine(outputRoot, date));

        try
        {
            Directory.CreateDirectory(outputFolder.FullName);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            enabled = false;
        }
    }

    void LateUpdate()
    {
        if (recordingEnabled)
        {

            for (int sample = 0; sample < samples; sample++)
            {
                renderCamera.targetTexture = renderTargets[0];
                renderCamera.Render();

                if (samples > 1)
                    SetProjectionMatrix(renderCamera, sample);

                renderCamera.Render();

                if (sample == 0)
                {
                    Graphics.Blit(renderTargets[0], renderTargets[1]);
                }
                else
                {
                    resolverMaterial.SetTexture("_HistoryTex", renderTargets[1]);
                    Graphics.Blit(renderTargets[0], renderTargets[2], resolverMaterial, 0);

                    // Swap history targets
                    tempRenderTexture = renderTargets[1];
                    renderTargets[1] = renderTargets[2];
                    renderTargets[2] = tempRenderTexture;
                }
            }

            outputTexture = renderTargets[1];

            if (supersample)
            {
                Graphics.Blit(renderTargets[1], renderTargets[3], resolverMaterial, 1);
                outputTexture = renderTargets[3];
            }

            renderCamera.ResetProjectionMatrix();
            renderCamera.targetTexture = originalTargetTexture;










            if (updateDisplay)
                RenderTexture.active = renderTexture;

            readTexture.ReadPixels(new Rect(0.0f, 0.0f, (int)resolution.x, (int)resolution.y), 0, 0);
            readTexture.Apply();

            byte[] pngByteArray = readTexture.EncodeToPNG();
            string path = string.Format("{0}/Frame-{0:D06}.png", outputFolder, Time.frameCount);
            File.WriteAllBytes(path, pngByteArray);

            if (updateDisplay)
            {
                RenderTexture.active = null;
                renderCamera.targetTexture = null;
                renderCamera.Render();
            }

            if (Time.frameCount > frameCount)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}
