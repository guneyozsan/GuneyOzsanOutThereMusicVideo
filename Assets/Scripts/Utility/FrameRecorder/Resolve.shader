// From https://github.com/Chman/FrameCapture
//
//MIT License
//
//Copyright (c) 2017 Thomas Hourdel
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

Shader "Hidden/Tools/Resolve"
{
    Properties
    {
        _MainTex ("", 2D) = "white" {}
        _HistoryTex ("", 2D) = "white" {}
    }

    CGINCLUDE
            
        #pragma target 3.0
        #include "UnityCG.cginc"

        struct Attributes
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
        };

        struct Varyings
        {
            float4 vertex : SV_POSITION;
            float2 texcoord : TEXCOORD0;
        };

        Varyings Vert(Attributes v)
        {
            Varyings o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.texcoord = v.texcoord;
            return o;
        }
        
        sampler2D _MainTex;
        float4 _MainTex_TexelSize;
        sampler2D _HistoryTex;
        float _Samples;

        // Very basic temporal resolve filter using moving averages, we don't have to deal with
        // ghosting so who cares
        float4 Frag(Varyings i) : SV_Target
        {
            float4 color = tex2D(_MainTex, i.texcoord);
            float4 history = tex2D(_HistoryTex, i.texcoord);
            return ((history * _Samples) + color) / (_Samples + 1);
        }

        float4 FragDownscaling(Varyings i) : SV_Target
        {
            // Standard bilinear (using hardware filtering)
            return tex2D(_MainTex, i.texcoord);

            // Standard bilinear (manual)
            //float4 a = tex2D(_MainTex, i.texcoord + _MainTex_TexelSize.xy * float2(-0.5, -0.5));
            //float4 b = tex2D(_MainTex, i.texcoord + _MainTex_TexelSize.xy * float2(-0.5,  0.5));
            //float4 c = tex2D(_MainTex, i.texcoord + _MainTex_TexelSize.xy * float2( 0.5,  0.5));
            //float4 d = tex2D(_MainTex, i.texcoord + _MainTex_TexelSize.xy * float2( 0.5, -0.5));
            //return (a + b + c + d) / 4;
        }

    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM

                #pragma vertex Vert
                #pragma fragment Frag

            ENDCG
        }

        Pass
        {
            CGPROGRAM

                #pragma vertex Vert
                #pragma fragment FragDownscaling

            ENDCG
        }
    }
}
