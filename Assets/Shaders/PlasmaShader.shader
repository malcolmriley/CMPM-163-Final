Shader "Custom/PlasmaShader" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_ColorGrade("Color Grade", 2D) = "white" {}
		_RimIntensity("Rim Intensity", Range(1.0, 8.0)) = 1.0
	}

	SubShader {
	Tags { "RenderType"="Opaque" }

	Pass {
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

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
			sampler2D _ColorGrade;
			float4 _MainTex_ST;
			
			uniform float _RimIntensity;

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
				float4 incidence = pow(dot(input.normal, input.view), _RimIntensity);
				
				fixed4 rimColor = tex2D(_ColorGrade, 1 - incidence);
				
				return color * rimColor;
			}
		ENDCG
		}
	}
}