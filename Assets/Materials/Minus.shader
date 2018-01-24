// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MinusColor"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Alpha Color Key", Color) = (0,0,0,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}
		SubShader
	{
		Tags
	{
		"RenderType" = "Opaque"
		"Queue" = "Transparent"
	}

		Pass
	{
		ZWrite Off
		Blend OneMinusDstColor OneMinusSrcColor
		Cull Off

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON
#include "UnityCG.cginc"

		sampler2D _MainTex;
	float4 _Color;

	struct Vertex
	{
		float4 vertex : POSITION;
		float2 uv_MainTex : TEXCOORD0;
	};

	struct Fragment
	{
		float4 vertex : POSITION;
		float2 uv_MainTex : TEXCOORD0;
	};

	Fragment vert(Vertex v)
	{
		Fragment o;

		o.vertex = UnityObjectToClipPos(v.vertex);
#ifdef PIXELSNAP_ON
		o.vertex = UnityPixelSnap(o.vertex);
#endif
		o.uv_MainTex = v.uv_MainTex;
		o.uv_MainTex = o.uv_MainTex;

		return o;
	}

	float4 frag(Fragment IN) : COLOR
	{
		float4 o = float4(0, 0, 0, 0);

		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.rgb = c.rgb;
		o.a = c.a;
		return o;
	}

		ENDCG
	}
	}
}