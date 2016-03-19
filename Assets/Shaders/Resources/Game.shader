Shader "Hidden/Game" {
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
			sampler2D _GUITexture;
			sampler2D _ChromaTexture;

			float _MirrorX;
			float _MirrorY;
			float4 _Color1;
			float4 _Color2;
			float4 _Color3;
			float _Color1Treshold;
			float _Color2Treshold;
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
				fixed4 col = tex2D(_ChromaTexture, uv);
				fixed4 gui = tex2D(_GUITexture, uv);

				// col.rgb = lerp(col, _Color1, step(distanceBetweenColors(col, _Color1), _Color1Treshold));
				// col.rgb = lerp(col, _Color2, step(distanceBetweenColors(col, _Color2), _Color2Treshold));
				// col.rgb = lerp(col, _Color3, step(distanceBetweenColors(col, _Color3), _Color3Treshold));

				col = lerp(col, gui, gui.a);

				return col;
			}
			ENDCG
		}
	}
}
