Shader "Custom/Trail"
{
	Properties {
        _OffsetX ("X Offset", float) = 0.2
        _ScaleY ("Y Scale", float) = 4
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent"  }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;            
                float4 vertexColor : COLOR;
			};
                     
            float _ScaleY;
            float _OffsetX;
			
			void vert (inout appdata v) {
				v.vertex = UnityObjectToClipPos(v.vertex);
			}

            fixed4 frag (appdata i) : SV_Target {
                float4 c = i.vertexColor;
                float2 pq = i.uv - float2(_OffsetX, 0.5);
                pq.y *= _ScaleY;
                if(pq.x < 0)
                    pq.x /= _OffsetX;
                else
                    pq.x /= 1 - _OffsetX;
                float d = length(pq);
                c.a -= d;
				return c;
			}
			ENDCG
		}
	}
}
