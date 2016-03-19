Shader "Hidden/Webcam" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_MirrorX ("Mirror X", Float) = 0
		_MirrorY ("Mirror Y", Float) = 0
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _TextureWebcam;
			sampler2D _GUITexture;

			float _MirrorX;
			float _MirrorY;
			float4 _Color1;
			float _Color1Treshold;
			float4 _Color2;
			float _Color2Treshold;
			float4 _Color3;
			float _Color3Treshold;

			float distanceBetweenColors (float3 colorA, float3 colorB)
			{
				return sqrt((colorA.r - colorB.r)*(colorA.r - colorB.r)+(colorA.g - colorB.g)*(colorA.g - colorB.g)+(colorA.b - colorB.b)*(colorA.b - colorB.b));
			}

			fixed4 frag (v2f_img i) : SV_Target 
			{
				float2 uv = i.uv.xy;
				uv.x = lerp(uv.x, 1. - uv.x, _MirrorX);
				uv.y = lerp(uv.y, 1. - uv.y, _MirrorY);
				fixed4 col = tex2D(_TextureWebcam, uv);

				col.rgb = lerp(col.rgb, float3(1,0,0), step(distanceBetweenColors(col, _Color1), _Color1Treshold));
				col.rgb = lerp(col.rgb, float3(0,1,0), step(distanceBetweenColors(col, _Color2), _Color2Treshold));
				col.rgb = lerp(col.rgb, float3(0,0,1), step(distanceBetweenColors(col, _Color3), _Color3Treshold));

				return col;
			}
			ENDCG
		}
	}
}
