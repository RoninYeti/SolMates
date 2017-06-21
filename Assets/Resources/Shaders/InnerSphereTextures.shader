Shader "Flip Normals" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Transparency("Transparency", Range(0.0, 1.0)) = 1.0
	}
		SubShader{

		Tags{ "RenderType" = "Transparent" }

		Cull Front

		CGPROGRAM

#pragma surface surf Lambert vertex:vert alpha:fade
		sampler2D _MainTex;
		float _Transparency;

	struct Input {
		float2 uv_MainTex;
		float4 color : COLOR;
	};


	void vert(inout appdata_full v)
	{
		v.normal.xyz = v.normal * -1;
	}

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 result = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = result.rgb;
		o.Alpha = result.a * _Transparency;
	}

	ENDCG

	}

		Fallback "Diffuse"
}