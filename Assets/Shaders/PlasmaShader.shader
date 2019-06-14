Shader "Custom/PlasmaShader" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "black" {}
		_ColorGrade("Color Grade", 2D) = "white" {}
		_RimIntensity("Rim Intensity", Range(1.0, 8.0)) = 1.0
		_RimBias("Rim Color Bias", Range(0.01, 2.0)) = 1.0
		_NoiseIntensity("Noise Intensity", Range(0.01, 1.0)) = 0.5
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

			#include "UnityCG.cginc"
			
			#define ERROR_EPSILON 0.02

			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct VertexOutput {
				float2 uv : TEXCOORD0;
				float2 uvScreen : TEXCOORD1;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 view : DIRECTION;
			};

			sampler2D _MainTex;
			sampler2D _NoiseTex;
			sampler2D _ColorGrade;
			float4 _MainTex_ST;
			
			uniform float _RimIntensity;
			uniform float _RimBias;
			uniform float _NoiseIntensity;
			uniform const float4 ERROR_LIMIT = float4(ERROR_EPSILON, ERROR_EPSILON, ERROR_EPSILON, ERROR_EPSILON);
			
			float luma(float4 color) {
				return (color.r + color.g + color.b) / 3;
			}
			
			float4 correctError(float4 input) {
				return max(input, ERROR_LIMIT);
			}

			VertexOutput vert(VertexInput input) {
				VertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.normal = UnityObjectToWorldNormal(input.normal);
				output.view = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, input.vertex).xyz);
				output.uvScreen = ComputeScreenPos(output.vertex);
				return output;
			}

			fixed4 frag(VertexOutput input) : SV_Target {
				float4 color = tex2D(_MainTex, input.uv);
				float4 noise = tex2D(_NoiseTex, input.uvScreen);
				float4 incidence = correctError(pow(dot(input.normal, input.view), _RimIntensity) * pow(noise, 1 - _NoiseIntensity));
				
				float4 rimColor = tex2D(_ColorGrade, float2(pow(luma(1 - incidence), _RimBias), 0.5));
				
				float4 outColor = color * rimColor;
				outColor.a = luma(incidence);
				
				return outColor;
			}
		ENDCG
		}
	}
}