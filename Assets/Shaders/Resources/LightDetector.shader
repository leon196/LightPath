Shader "Hidden/LightDetector" {
	Properties {
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _WebcamTexture;
			fixed4 _Color1;
			fixed4 _Color2;
			fixed4 _Color3;
			float _Color1Treshold;
			float _Color2Treshold;
			float _Color3Treshold;

			float distanceColor (fixed4 colorA, fixed4 colorB)
			{
				return abs(colorA.r - colorB.r) + abs(colorA.g - colorB.g) + abs(colorA.b - colorB.b);
			}

			fixed4 frag (v2f_img i) : SV_Target 
			{
				float2 uv = i.uv.xy;
				fixed4 webcam = tex2D(_WebcamTexture, uv);
				fixed4 col = fixed4(0,0,0,1);

				// Extract color marker and mark them for CPU check
				col.rgb = lerp(col.rgb, float3(1,0,0), step(distanceColor(webcam, _Color1), _Color1Treshold));
				col.rgb = lerp(col.rgb, float3(0,1,0), step(distanceColor(webcam, _Color2), _Color2Treshold));
				col.rgb = lerp(col.rgb, float3(0,0,1), step(distanceColor(webcam, _Color3), _Color3Treshold));

				return col;
			}
			ENDCG
		}
	}
}
