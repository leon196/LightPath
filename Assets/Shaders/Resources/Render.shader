Shader "Hidden/Render" {
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
			
			sampler2D _FrameBuffer;
			float _MirrorX;
			float _MirrorY;

			fixed4 frag (v2f_img i) : SV_Target 
			{
				float2 uv = i.uv.xy;
				uv.x = lerp(uv.x, 1. - uv.x, _MirrorX);
				uv.y = lerp(uv.y, 1. - uv.y, _MirrorY);
				return tex2D(_FrameBuffer, uv);
			}
			ENDCG
		}
	}
}
