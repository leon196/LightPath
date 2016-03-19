Shader "Hidden/Webcam" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
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
			sampler2D _ChromaTexture;
			sampler2D _GUITexture;

			float _MirrorX;
			float _MirrorY;
			float4 _Color1;
			float4 _Color2;
			float4 _Color3;
			float _Color1Treshold;
			float _Color2Treshold;
			float _Color3Treshold;

			fixed4 frag (v2f_img i) : SV_Target 
			{
				float2 uv = i.uv.xy;
				// uv.x = lerp(uv.x, 1. - uv.x, _MirrorX);
				// uv.y = lerp(uv.y, 1. - uv.y, _MirrorY);
				fixed4 webcam = tex2D(_TextureWebcam, uv);
				fixed4 buffer = tex2D(_ChromaTexture, uv);
				buffer.rgb *= 0.95;

				fixed4 col = buffer;//float4(0,0,0,0);
				col.rgb = lerp(col.rgb, _Color1, step(distance(webcam, _Color1), _Color1Treshold));
				col.rgb = lerp(col.rgb, _Color2, step(distance(webcam, _Color2), _Color2Treshold));
				col.rgb = lerp(col.rgb, _Color3, step(distance(webcam, _Color3), _Color3Treshold));

				// float t = _Time * 20.;
				col = lerp(col, webcam, step(0.7, distance(col, webcam)));

				return col;
			}
			ENDCG
		}
	}
}
