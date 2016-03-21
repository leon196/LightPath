Shader "Hidden/Game" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_ShowWebcam ("Show Webcam", Float) = 0
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _WebcamTexture;
			sampler2D _UITexture;
			sampler2D _LightTexture;
			sampler2D _FrameBuffer;

			fixed4 _Color1;
			fixed4 _Color2;
			fixed4 _Color3;
			fixed4 _Color4;
			fixed4 _ColorIntegerCode1;
			fixed4 _ColorIntegerCode2;
			fixed4 _ColorIntegerCode3;
			fixed4 _ColorIntegerCode4;
			float _Color1Treshold;
			float _Color2Treshold;
			float _Color3Treshold;
			float _Color4Treshold;

			float _ShowWebcam;

			float distanceColor (fixed4 colorA, fixed4 colorB)
			{
				return abs(colorA.r - colorB.r) + abs(colorA.g - colorB.g) + abs(colorA.b - colorB.b);
			}

			fixed4 frag (v2f_img i) : SV_Target 
			{
				float2 uv = i.uv.xy;
				fixed4 light = tex2D(_LightTexture, uv);
				fixed4 gui = tex2D(_UITexture, uv);

				fixed4 webcam = tex2D(_WebcamTexture, uv);
				fixed4 buffer = tex2D(_FrameBuffer, uv);
				buffer.rgb *= 0.98;

				// fixed4 col = webcam;
				fixed4 col = light;
				// fixed4 col = buffer;//float4(0,0,0,1);

				col.rgb = lerp(col.rgb, _Color1, step(distanceColor(light, _ColorIntegerCode1), 0.5));
				col.rgb = lerp(col.rgb, _Color2, step(distanceColor(light, _ColorIntegerCode2), 0.5));
				col.rgb = lerp(col.rgb, _Color3, step(distanceColor(light, _ColorIntegerCode3), 0.5));
				col.rgb = lerp(col.rgb, _Color4, step(distanceColor(light, _ColorIntegerCode4), 0.5));

				// col.rgb = lerp(col.rgb, light.rgb, step(0.1, Luminance(light)));
				// col.rgb = saturate(col.rgb);

				// float t = _Time * 20.;
				// col = lerp(buffer, col, step(0.5, Luminance(abs(col.rgb - buffer.rgb))));
				// col = lerp(buffer, col, step(0.9, distance(col, buffer)));
				col = max(col, buffer);
				col = lerp(col, gui, gui.a);

				return lerp(col, webcam, _ShowWebcam);
			}
			ENDCG
		}
	}
}
