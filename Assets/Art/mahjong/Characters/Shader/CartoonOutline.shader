Shader "Custom/CartoonOutline" {
	Properties{
		_OutlineScale("Scale", Float) = 0.01
		_OutlineZOffset("OutlineZOffset", Float) = 1.0
		_OutlineThickness("Outline Thickness", Float) = 0.037
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
	}

		SubShader{
			Tags {"RenderType" = "Opaque"}
			Pass{
				NAME "OUTLINE"
				Tags { "LightMode" = "ForwardBase" }

				Cull Front

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#pragma target 3.0

				#include "UnityCG.cginc"

				float _OutlineScale;
				float _OutlineZOffset;
				float _OutlineThickness;
				float4 _OutlineColor;

				struct VertexInput
				{
					float4 vertex:POSITION;
					float3 normal:NORMAL;
					float4 color:COLOR;
				};

				struct v2f
				{
					float4 pos:SV_POSITION;
					UNITY_FOG_COORDS(0)
				};

				v2f vert(VertexInput v)
				{
					v2f o;

					float3 viewPos = UnityObjectToViewPos(v.vertex);

					float z = -viewPos.z / unity_CameraProjection[1].y;

					float3 viewNormal = mul((float3x3)UNITY_MATRIX_MV,v.normal);
					viewNormal.z = 0.01;
					viewNormal = normalize(viewNormal);

					float s = sqrt(z / _OutlineScale);

					float thickness = _OutlineThickness * _OutlineScale;

					s = s  * thickness; //*color.a

					float3 zOffset = normalize(viewPos) * _OutlineZOffset * _OutlineScale;
					//zOffset = zOffset * (v.color.z - 0.5);

					float2 normalOffset = s * viewNormal.xy;

					//viewPos.xy += normalOffset + zOffset.xy;
					viewPos.xy += normalOffset;
					viewPos.xyz += zOffset.xyz;

					float4 projPos = mul(UNITY_MATRIX_P, float4(viewPos,1));

					o.pos = projPos;

					UNITY_TRANSFER_FOG(o,o.pos);

					return o;
				}

				float4 frag(v2f i) :SV_Target
				{
					float4 color = _OutlineColor;
					UNITY_APPLY_FOG(i.fogCoords, color);
					return color;

				}
				ENDCG

			}

	}
		Fallback Off
}
