Shader "Custom/DiffuseVertexShader" {
	Properties
	{
		_Diffuse("Diffuse", Color) = (1.0, 1.0, 1.0, 1.0)
	}
		SubShader
	{
		Pass
		{
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"

			fixed4 _Diffuse;

			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float3 color : COLOR;
			};


			v2f vert(a2v a)
			{
				v2f v;
				v.position = UnityObjectToClipPos(a.vertex);

				//环境光
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

				fixed3 worldNormal = normalize(mul(a.normal, (float3x3)unity_WorldToObject));

				fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

				v.color = ambient + diffuse;

				return v;
			}

			fixed4 frag(v2f a) : SV_Target
			{
				return fixed4(a.color, 1.0);
			}
			ENDCG
		}
	}
}