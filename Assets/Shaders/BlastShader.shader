Shader "Custom/BlastShader" {
	Properties {
	_MainTex("Texture", 2D) = "white" {}
	}

	SubShader {
	Tags { 
		"RenderType"="Transparent"
		"Queue"="Transparent"
	}

	Pass {
		
		ZWrite Off
		Blend SrcAlpha DstAlpha
		
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			# include "UnityCG.cginc"

			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct VertexOutput {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			VertexOutput vert(VertexInput input) {
				VertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				return output;
			}

			fixed4 frag(VertexOutput input) : SV_Target {
				fixed4 color = tex2D(_MainTex, input.uv);
				return color;
			}
		ENDCG
		}
	}
}