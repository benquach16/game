Shader "Custom/shaderSelectionCircle" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

	}
	SubShader
	{
		Pass
		{
			ZWrite off
			ColorMask RGB
			Blend One One
		Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f 
			{
				float4 pos : SV_POSITION;
				float2 uvShadow : TEXCOORD0;
				float2 uvFalloff : TEXCOORD1;
			};

			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, vertex);
				o.uvShadow = mul(unity_Projector, vertex);
				o.uvFalloff = mul(unity_ProjectorClip, vertex);
				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
			}

			sampler2D _MainTex;
			fixed4 _Color;
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.uvShadow);
				//tex += _Color;
				//tex *= _Color;
				return tex;
			}
			ENDCG
		}
	}
}
