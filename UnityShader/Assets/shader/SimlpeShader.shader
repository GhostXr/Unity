Shader "Custom/SimpleShader" {
	Properties
	{
		_Color("Color Tint", Color) = (1.0, 1.0, 1.0, 1.0)
		_Vector("Vector Tint", Vector) = (1.0, 1.0, 1.0, 1.0)
	}
		SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;
			float4 _Vector;

			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				fixed3 color : COLOR0;
			};

			v2f vert(a2v v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.position += _Vector;
				o.color = v.normal + fixed3(0.5, 0.5, 0.5);
				return o;
			}

			fixed4 frag(v2f o) : SV_Target
			{
				fixed3 c = o.color;
				c += _Color.rgb;
				return fixed4(c, 1.0);
			}

			ENDCG
		}
	}
}