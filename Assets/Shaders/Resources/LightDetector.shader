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
			fixed4 _Color4;
			fixed4 _ColorIntegerCode1;
			fixed4 _ColorIntegerCode2;
			fixed4 _ColorIntegerCode3;
			fixed4 _ColorIntegerCode4;
			float _ColorTreshold1;
			float _ColorTreshold2;
			float _ColorTreshold3;
			float _ColorTreshold4;

			float distanceColor (fixed4 colorA, fixed4 colorB)
			{
				return abs(colorA.r - colorB.r) + abs(colorA.g - colorB.g) + abs(colorA.b - colorB.b);
			}

			fixed4 frag (v2f_img i) : SV_Target 
			{
				float2 uv = i.uv.xy;
				fixed4 webcam = tex2D(_WebcamTexture, uv);
				fixed4 col = fixed4(0,0,0,1);

				// Extract color marker
				col.rgb = lerp(col.rgb, _ColorIntegerCode1, step(distanceColor(webcam, _Color1), _ColorTreshold1));
				col.rgb = lerp(col.rgb, _ColorIntegerCode2, step(distanceColor(webcam, _Color2), _ColorTreshold2));
				col.rgb = lerp(col.rgb, _ColorIntegerCode3, step(distanceColor(webcam, _Color3), _ColorTreshold3));
				col.rgb = lerp(col.rgb, _ColorIntegerCode4, step(distanceColor(webcam, _Color4), _ColorTreshold4));

				return col;
			}
			ENDCG
		}
	}
}
