// Frame Recorder - A tool to render frames of a Unity 3D camera view as series of images.
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

// This file incorporates work covered by the following copyright and
// permission notice:  

// Frame Capture from https://github.com/Chman/FrameCapture

// Copyright (c) 2017 Thomas Hourdel

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// ---------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

// Based mostly on:
// https://github.com/Chman/FrameCapture/
// and also:
// https://docs.unity3d.com/ScriptReference/Time-captureFramerate.html

namespace PostIllusions.Utility
{
    /// <summary>
    /// Captures frames as a PNG screenshot sequence.
    /// </summary>
    public class FrameRecorder : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        private         Resolution          targetRes;

        private const   int                 minFrameRate = 1;
        private const   int                 maxFrameRate = 120;
        [SerializeField, Range(minFrameRate, maxFrameRate)]
        private         int                 frameRate;

        private const   int                 minSamples = 1;
        private const   int                 maxSamples = 16;
        [Space]
        [SerializeField, Range(minSamples, maxSamples)]
        private         int                 samples = 1;

        [Space]
        [SerializeField]
        private         bool                supersample = false;

        [Space]
        [Header("Display")]
        [SerializeField]
        private         bool                displayRender;

        [Space]
        [Header("Setup")]
        [SerializeField]
        private         Camera              renderCamera;
        [SerializeField]
        private         RawImage            renderDisplay;

        private         int                 frameCount;
        private         Material            resolverMaterial;
        private         Texture2D           outputTexture;
        private         DirectoryInfo       outputFolder;
        private         RectTransform       displayRectTransform;
        private         Color               transparentColor = new Color(0, 0, 0, 0);

        private         WaitForEndOfFrame   waitForEndOfFrame;

        [Serializable]
        private struct Resolution
        {
            [SerializeField]
            private int     width;
            public  int     Width { get { return width; } private set { width = value; } }
            [SerializeField]
            private int     height;
            public  int     Height { get { return height; } private set { height = value; } }

            public  Vector2 Size { get { return new Vector2(width, height); } }

            public Resolution(int width, int height)
            {
                this.width  = width;
                this.height = height;
            }

            public static Resolution operator *(Resolution resolution, float t)
            {
                return t * resolution;
            }

            public static Resolution operator *(float t, Resolution resolution)
            {
                return new Resolution((int)(t * resolution.Width), (int)(t * resolution.Height));
            }
        }

        void Start()
        {
            displayRectTransform = renderDisplay.GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            frameCount              = 0;
            Time.captureFramerate   = Mathf.Clamp(frameRate, minFrameRate, maxFrameRate);

            resolverMaterial        = new Material(Shader.Find("Hidden/Tools/Resolve"));
            samples                 = Mathf.Clamp(samples, minSamples, maxSamples);
            resolverMaterial.SetFloat("_Samples", samples);

            {
                string  outputRoot      = new DirectoryInfo(Application.dataPath).Parent.Parent.FullName;
                string  date            = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff");
                        outputFolder    = new DirectoryInfo(Path.Combine(outputRoot, date));

                try
                {
                    Directory.CreateDirectory(outputFolder.FullName);
                    Debug.Log("Output folder: " + outputFolder.FullName);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    enabled = false;
                }
            }
        }

        void OnDisable()
        {
            Time.captureFramerate   = 0;
            frameCount              = 0;

            renderDisplay.color = new Color(0, 0, 0, 0);

            Destroy(resolverMaterial);
            Destroy(outputTexture);
        }

        void Update()
        {
            StartCoroutine(CaptureFrame());
        }

        IEnumerator CaptureFrame()
        {
            yield return waitForEndOfFrame;

            {
                RenderTexture originalRenderTexture = RenderTexture.active;
                RenderTexture originalTargetTexture = renderCamera.targetTexture;

                RenderTexture[] renderTargets;

                {
                    RenderTextureFormat outputFormat = RenderTextureFormat.ARGB32;
                    RenderTextureFormat renderFormat = renderCamera.allowHDR
                        ? RenderTextureFormat.ARGBHalf
                        : RenderTextureFormat.ARGB32;

                    Resolution superRes;

                    if (supersample)
                        superRes = 2f * targetRes;
                    else
                        superRes = targetRes;

                    renderTargets = new[]
                    {
                        RenderTexture.GetTemporary(superRes.Width, superRes.Height, 24, renderFormat),
                        RenderTexture.GetTemporary(superRes.Width, superRes.Height, 0, outputFormat),
                        RenderTexture.GetTemporary(superRes.Width, superRes.Height, 0, outputFormat),
                        RenderTexture.GetTemporary(targetRes.Width, targetRes.Height, 0, outputFormat)
                    };
                }

                for (int sample = 0; sample < samples; sample++)
                {
                    renderCamera.targetTexture = renderTargets[0];

                    // Only jitters if we're actually using the temporal filter
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
                        RenderTexture   tempRenderTexture   = renderTargets[1];
                                        renderTargets[1]    = renderTargets[2];
                                        renderTargets[2]    = tempRenderTexture;
                    }
                }

                RenderTexture outputRenderTexture = renderTargets[1];

                if (supersample)
                {
                    Graphics.Blit(renderTargets[1], renderTargets[3], resolverMaterial, 1);
                    outputRenderTexture = renderTargets[3];
                }

                renderCamera.ResetProjectionMatrix();
                renderCamera.targetTexture  = originalTargetTexture;

                RenderTexture.active        = outputRenderTexture;

                FormatOutputTexture(ref outputTexture, targetRes);
                outputTexture.ReadPixels(new Rect(0, 0, targetRes.Width, targetRes.Height), 0, 0);
                outputTexture.Apply();

                RenderTexture.active        = originalRenderTexture;

                foreach (RenderTexture renderTarget in renderTargets)
                    RenderTexture.ReleaseTemporary(renderTarget);
            }

            try
            {
                byte[]  frameBytes  = outputTexture.EncodeToPNG();
                string  path        = Path.Combine(outputFolder.FullName, string.Format("Frame-{0:D06}.png", frameCount));
                File.WriteAllBytes(path, frameBytes);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                enabled = false;
            }

            frameCount++;

            if (displayRender)
            {
                if (displayRectTransform.sizeDelta != targetRes.Size)
                    displayRectTransform.sizeDelta = targetRes.Size;

                renderDisplay.color     = Color.white;
                renderDisplay.texture   = outputTexture;
            }
            else
            {
                if (renderDisplay.color != transparentColor)
                    renderDisplay.color = transparentColor;
            }
        }

        void SetProjectionMatrix(Camera cam, int sample)
        {
            const   float   kJitterScale    = 1f;
                    Vector2 jitter          = kJitterScale * new Vector2(
                        HaltonSeq(sample & 1023, 2),
                        HaltonSeq(sample & 1023, 3)
                    );

            cam.nonJitteredProjectionMatrix = cam.projectionMatrix;

            if (cam.orthographic)
                cam.projectionMatrix = GetOrthoProjectionMatrix(cam, jitter);
            else
                cam.projectionMatrix = GetPerspProjectionMatrix(cam, jitter);

            cam.useJitteredProjectionMatrixForTransparentRendering = false;
        }

        float HaltonSeq(int index, int radix)
        {
            float result    = 0f;
            float fraction  = 1f / (float)radix;

            while (index > 0)
            {
                result      += (float)(index % radix) * fraction;

                index       /= radix;
                fraction    /= (float)radix;
            }

            return result;
        }

        Matrix4x4 GetPerspProjectionMatrix(Camera cam, Vector2 offset)
        {
            float       vertical        =   Mathf.Tan(0.5f * Mathf.Deg2Rad * cam.fieldOfView);
            float       horizontal      =   vertical * cam.aspect;
            float       near            =   cam.nearClipPlane;
            float       far             =   cam.farClipPlane;

                        offset.x        *=  horizontal / (0.5f * cam.pixelWidth);
                        offset.y        *=  vertical / (0.5f * cam.pixelHeight);

            float       left            =   (offset.x - horizontal) * near;
            float       right           =   (offset.x + horizontal) * near;
            float       top             =   (offset.y + vertical) * near;
            float       bottom          =   (offset.y - vertical) * near;

            Matrix4x4   matrix          =   new Matrix4x4();

                        matrix[0, 0]    =   (2f * near) / (right - left);
                        matrix[0, 1]    =   0f;
                        matrix[0, 2]    =   (right + left) / (right - left);
                        matrix[0, 3]    =   0f;

                        matrix[1, 0]    =   0f;
                        matrix[1, 1]    =   (2f * near) / (top - bottom);
                        matrix[1, 2]    =   (top + bottom) / (top - bottom);
                        matrix[1, 3]    =   0f;

                        matrix[2, 0]    =   0f;
                        matrix[2, 1]    =   0f;
                        matrix[2, 2]    =   -(far + near) / (far - near);
                        matrix[2, 3]    =   -(2f * far * near) / (far - near);

                        matrix[3, 0]    =   0f;
                        matrix[3, 1]    =   0f;
                        matrix[3, 2]    =   -1f;
                        matrix[3, 3]    =   0f;

            return matrix;
        }

        Matrix4x4 GetOrthoProjectionMatrix(Camera cam, Vector2 offset)
        {
            float   vertical    =   cam.orthographicSize;
            float   horizontal  =   vertical * cam.aspect;

                    offset.x    *=  horizontal / (0.5f * cam.pixelWidth);
                    offset.y    *=  vertical / (0.5f * cam.pixelHeight);

            float   left        =   offset.x - horizontal;
            float   right       =   offset.x + horizontal;
            float   top         =   offset.y + vertical;
            float   bottom      =   offset.y - vertical;

            return Matrix4x4.Ortho(left, right, bottom, top, cam.nearClipPlane, cam.farClipPlane);
        }

        void FormatOutputTexture(ref Texture2D texture, Resolution resolution)
        {
            if ((texture != null) && (texture.width == resolution.Width) && (texture.height == resolution.Height))
                return;

            Destroy(texture);
            texture = new Texture2D(resolution.Width, resolution.Height, TextureFormat.ARGB32, false);
        }
    }
}
