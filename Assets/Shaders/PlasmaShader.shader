Shader "Custom/PlasmaShader" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader {
	Tags { "RenderType"="Opaque" }

	Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			# include "UnityCG.cginc"

			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct VertexOutput {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 view : DIRECTION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			VertexOutput vert(VertexInput input) {
				VertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.normal = UnityObjectToWorldNormal(input.normal);
				output.view = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, input.vertex).xyz);
				return output;
			}

			fixed4 frag(VertexOutput input) : SV_Target {
				fixed4 color = tex2D(_MainTex, input.uv);
				fixed4 rimColor = fixed4(1.0, 0.0, 0.0, 0.0);
				
				float4 incidence = dot(input.normal, input.view);
				
				return (color * incidence) + (rimColor * (1 - incidence));
			}
		ENDCG
		}
	}
}