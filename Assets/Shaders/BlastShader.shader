Shader "Custom/BlastShader" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_ColorGrade("Color Grade", 2D) = "white" {}
	}

	SubShader {
	Tags { 
		"RenderType"="Transparent"
		"Queue"="Transparent"
	}

	Pass {
		
		ZWrite Off
		Blend SrcAlpha One
		
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
			sampler2D _ColorGrade;
			float4 _MainTex_ST;
			
			float luma(float4 color) {
				return (color.r + color.g + color.b) / 3;
			}

			VertexOutput vert(VertexInput input) {
				VertexOutput output;
				output.vertex = UnityObjectToClipPos(input.vertex);
				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				return output;
			}

			fixed4 frag(VertexOutput input) : SV_Target {
				float4 textureSample = tex2D(_MainTex, input.uv);
				float textureLuma = luma(textureSample);
				float4 color = tex2D(_ColorGrade, float2(1 - textureLuma, 0.5F));
				color.a = textureLuma * textureSample.a;
				return color;
			}
		ENDCG
		}
	}
}