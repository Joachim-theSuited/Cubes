Shader "UI/SemiCircle"
{
	Properties {
		[MaterialToggle] _CenterRight ("Center Right", float) = 0
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
                     
			float _CenterRight;
			
			void vert (inout appdata v) {
				v.vertex = UnityObjectToClipPos(v.vertex);
			}

            fixed4 frag (appdata i) : SV_Target {
                float2 pq = i.uv;
                if(_CenterRight)
                    pq -= float2(1.0, 0.5);
                else
                    pq -= float2(0.0, 0.5);
                pq.y *= 2.0;
                float4 c  = i.vertexColor;
                c.a = 1.0 - step(1.0, length(pq));
				return c;
			}
			ENDCG
		}
	}
}
