Shader "Sprites/Compass Icon"
{
	Properties {
        [HideInInspector] _MainTex ("Texture", 2D) = "" { }
        [HideInInspector] playerDistance ("Player Distance", float) = 1
        [HideInInspector] screenOffset ("Screen Offset",  float) = 0
	}

    SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        // code shared between passes
        CGINCLUDE

            // width/height ratio of target render texture; i.e. 1024/64=16
            // multiplied with camera.size (0.125)
            // multiplied by 2 (because camera extends in both directions?)
            #define TARGET_WIDTH 4

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;            
                float4 vertexColor : COLOR;
            };

            // set by SpriteRenderer
            sampler2D _MainTex;

            // set by script
            float playerDistance;
            float screenOffset;

            fixed4 frag (appdata i) : SV_Target {
                float4 c = i.vertexColor;
                c *= tex2D( _MainTex, i.uv );
                return c;
            }

        ENDCG

        Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
                     
			void vert (inout appdata v) {
                // translate horizontally based on the (normalised) angle
                v.vertex.x -= TARGET_WIDTH * screenOffset;
                // z determines draw ordering; closer icons should be in front
                // negative sign, because we omit the view matrix
                v.vertex.z = -playerDistance;
                // we only apply projection matrix
                // model matrix is replaced with above ops
                // view matrix is discarded, as we want a fixed camera
                v.vertex = mul(UNITY_MATRIX_P, float4(v.vertex.xyz, 1.0f));
			}
   
			ENDCG
		}

        // draw a second time, shifted by 360 degrees
        // this achieves wrap-around at the edges
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            void vert (inout appdata v) {
                v.vertex.x -= TARGET_WIDTH * (screenOffset + 1);
                v.vertex.z = -playerDistance;
                v.vertex = mul(UNITY_MATRIX_P, float4(v.vertex.xyz, 1.0f));
            }

            ENDCG
        }

	}
}
