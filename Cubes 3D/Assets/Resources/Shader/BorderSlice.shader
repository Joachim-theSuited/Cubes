Shader "Custom/BorderSlice" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
        _BorderColor ("Border", Color) = (1,1,1,1)
        // y-value in model-space above which the model will not be rendered
        threshold ("Threshold", float) = 0
        // the y-slice [threshold, threshold-border] will be rendered in _BorderColor
        border ("Border", float) = 0.1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull off
        
		CGPROGRAM
		// Physically based Standard lighting model
		#pragma surface surf Standard vertex:vert finalcolor:mycolor

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
            float3 viewDir;
            float3 worldNormal;
            float3 modelPos;
		};

		uniform fixed4 _Color;
        uniform fixed4 _BorderColor;
        uniform float threshold;
        uniform float border;

        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input,o);
            o.modelPos = v.vertex;
        }

        void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color) {
            // if in the border slice, or the now visible back of the model draw in _BorderColor
            if(IN.modelPos.y > threshold-border || dot(IN.viewDir, IN.worldNormal) < -0.1) {
                color = _BorderColor;
            }
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
            if(IN.modelPos.y > threshold)
                discard;
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
