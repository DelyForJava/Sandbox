Shader "Custom/CartoonLighting"{
    Properties {
		//Material Properties
		_Color("Color", Color) = (0.931, 0.931, 0.931, 0.95)
		_MainTex ("Diffuse", 2D) = "white" {}
		_LightInfo ("LightInfo", 2D) = "white"{}
		//ShadowProperties
		_LightArea("LightArea", Range(0,1)) = 0.51
		_FirstShadowMultiColor("FirstShadowMultiColor", Color) = (0.81,0.72,0.75,0.0)
		_SecondShadow("SecondShadow", Range(0,1)) = 0.51
		_SecondShadowMultiColor("SecondShadowMultiColor", Color) = (0.75, 0.61,0.68,0.0)
		_Shininess("Shininess",Float) = 10
		_SpecularColor("SpecularColor", Color) = (0.5,0.5,0.5,1.0)
		_SpecularMulti("SpecularMulti",Float) = 0.2
		_BloomFactor("BloomFactor", Float) = 1
	}
	SubShader{
		Tags {"RenderType" = "Opaque" }
		Pass{
			NAME "LIGHTING"
			Tags {"LightMode" = "ForwardBase"}


			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#include "Lighting.cginc"

			float4 _Color;
			sampler2D _MainTex;
			sampler2D _LightInfo;
			float4 _MainTex_ST;

			float _LightArea;
			float4 _FirstShadowMultiColor;
			float _SecondShadow;
			float4 _SecondShadowMultiColor;
			float _Shininess;
			float4 _SpecularColor;
			float _SpecularMulti;

			float _BloomFactor;

			struct VertexInput
			{
				float4 vertex:POSITION;
				float4 color:COLOR;
				float3 normal:NORMAL;
				float2 texcoord:TEXCOORD0;
			};

			struct v2f
			{
				float4 pos:SV_POSITION;
				float4 color:TEXCOORD0;
				float3 worldNormal:TEXCOORD1;
				float3 worldPos:TEXCOORD2;
				float2 uv:TEXCOORD3;
				float nl:TEXCOORD4;
				UNITY_FOG_COORDS(5)
			};

			v2f vert(VertexInput v)
			{
				v2f o;
				float4 projPos =  UnityObjectToClipPos(v.vertex);
				

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
				float2 uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);

				float nl = dot(worldNormal, _WorldSpaceLightPos0);
				nl = nl * 0.5 + 0.5;

				o.pos = projPos;
				o.color = float4(1.0,0.5,0.5,0.5);// v.color;
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				o.uv = uv;
				o.nl = nl;

				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
			}

			float4 frag(v2f i):SV_Target
			{ 
				float4 color = _Color;
				float3 worldNormal = i.worldNormal;
				float3 worldPos = i.worldPos;
				float2 uv = i.uv;
				float nl = i.nl;

				float4 diffTex = tex2D(_MainTex, uv);
				float4 lightTex = tex2D(_LightInfo, uv);

				float lightinfo = color.x * lightTex.y;

				float light;
				if (lightinfo < 0.09) {
					light = (nl + lightinfo)* 0.5;
					if (light < _SecondShadow) {
						diffTex *= _SecondShadowMultiColor;
					}
					else {
						diffTex *= _FirstShadowMultiColor;
					}
				}
				else {
					if (lightinfo > 0.5) {
						light = 1.2 * lightinfo - 0.1;
					}
					else {
						lightinfo *= 1.25;
						light = lightinfo - 0.125;
					}
				}
				
				float factor = (nl + light)*0.5;
				if (factor < _LightArea) {
					diffTex *= _FirstShadowMultiColor;
				}

				float3 viewDir = normalize(_WorldSpaceCameraPos - worldPos);
				float3 halfVector = normalize(viewDir + _WorldSpaceLightPos0);
				float nh = clamp(dot(worldNormal, halfVector),0,1);
				float specular = pow(nh, _Shininess);
				float specularInfo = 1.0 - lightTex.z;
				if (specular > specularInfo) {
					specular =  _SpecularColor * _SpecularMulti * lightTex.x;
				}
				else {
					specular = 0;
				}

				return (diffTex + specular)* _BloomFactor * _Color;

				UNITY_APPLY_FOG(i, color);

				return diffTex;
			}

			ENDCG

            
		}
		
	}
    Fallback "Diffuse"
	
}