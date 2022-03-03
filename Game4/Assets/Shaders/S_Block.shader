Shader "Custom/S_Block"
{
	Properties
	{
		_MainTex("Diffuse", 2D) = "white" {}
		_MaskTex("Mask", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_Color("Tint", Color) = (1, 1, 1, 1)
		_Dither("Dither amount", Range(0,3)) = 0

			// Legacy properties. They're here so that materials using this shader can gracefully fallback to the legacy sprite shader.
		   // [HideInInspector] _Color("Tint", Color) = (1,1,1,1)
			[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
			[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
			[HideInInspector] _AlphaTex("External Alpha", 2D) = "white" {}
			[HideInInspector] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

		SubShader
			{
				Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

				Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
				Cull Off
				ZWrite Off
				ZTest Always

				Pass
				{
					Tags { "LightMode" = "Universal2D" }

					HLSLPROGRAM
					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

					#pragma vertex CombinedShapeLightVertex
					#pragma fragment CombinedShapeLightFragment

					#pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
					#pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
					#pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
					#pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __
					#pragma multi_compile _ DEBUG_DISPLAY

					struct Attributes
					{
						float3 positionOS   : POSITION;
						float4 color        : COLOR;
						float2  uv          : TEXCOORD0;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					struct Varyings
					{
						float4  positionCS  : SV_POSITION;
						half4   color       : COLOR;
						float2  uv          : TEXCOORD0;
						half2   lightingUV  : TEXCOORD1;
						#if defined(DEBUG_DISPLAY)
						float3  positionWS  : TEXCOORD2;
						#endif
						UNITY_VERTEX_OUTPUT_STEREO
					};

					#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

					TEXTURE2D(_MainTex);
					SAMPLER(sampler_MainTex);
					TEXTURE2D(_MaskTex);
					SAMPLER(sampler_MaskTex);
					half4 _MainTex_ST;

					#if USE_SHAPE_LIGHT_TYPE_0
					SHAPE_LIGHT(0)
					#endif

					#if USE_SHAPE_LIGHT_TYPE_1
					SHAPE_LIGHT(1)
					#endif

					#if USE_SHAPE_LIGHT_TYPE_2
					SHAPE_LIGHT(2)
					#endif

					#if USE_SHAPE_LIGHT_TYPE_3
					SHAPE_LIGHT(3)
					#endif

					Varyings CombinedShapeLightVertex(Attributes v)
					{
						Varyings o = (Varyings)0;
						UNITY_SETUP_INSTANCE_ID(v);
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

						o.positionCS = TransformObjectToHClip(v.positionOS);
						#if defined(DEBUG_DISPLAY)
						o.positionWS = TransformObjectToWorld(v.positionOS);
						#endif
						o.uv = TRANSFORM_TEX(v.uv, _MainTex);
						o.lightingUV = half2(ComputeScreenPos(o.positionCS / o.positionCS.w).xy);

						o.color = v.color;
						return o;
					}

					#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

					half4 CombinedShapeLightFragment(Varyings i) : SV_Target
					{
						const half4 main = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
						const half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);
						SurfaceData2D surfaceData;
						InputData2D inputData;

						InitializeSurfaceData(main.rgb, main.a, mask, surfaceData);
						InitializeInputData(i.uv, i.lightingUV, inputData);

						return CombinedShapeLightShared(surfaceData, inputData);
					}
					ENDHLSL
				}
					Pass
					{
						Tags { "LightMode" = "UniversalForward" "Queue" = "Transparent" "RenderType" = "Transparent"}
						CGPROGRAM
						#pragma vertex vert
						#pragma fragment frag

						#include "UnityCG.cginc"

						struct appdata
						{
							float4 vertex : POSITION;
							float2 uv : TEXCOORD0;
						};

						struct v2f
						{
							float2 uv : TEXCOORD0;
							float4 vertex : SV_POSITION;
						};

						v2f vert(appdata v)
						{
							v2f o;
							o.vertex = UnityObjectToClipPos(v.vertex);
							o.uv = v.uv;
							return o;
						}

						sampler2D _MainTex;
						float _Dither;
						float4 _Color;

						float DitherSome(float2 uv)
						{
							int row = (int)(uv.y * 4);
							int col = (int)(uv.x * 4);

							if (col % 2 == 1)
								return 1;
							if (row % 2 == 1)
								return 1;

							return 0.25f;
						}

						float DitherHalf(float2 uv)
						{
							int row = (int)(uv.y * 4);
							int col = (int)(uv.x * 4);
							if ((col + row) % 2 == 0)
								return 0.25f;
							return 1;

						}

						float DitherWhole(float2 uv)
						{
							int row = (int)(uv.y * 5);
							int col = (int)(uv.x * 5);
							if (row == 0 && col == 0 || row == 4 && col == 0 || row == 0 && col == 4 || row == 4 && col == 4)
								return 1;
							if ((col + row) % 2 == 0)
								return 0.25f;
							if (row == 0 || row == 4)
								return 0.25f;
							if (col == 0 || col == 4)
								return 0.25f;
							return 1;

						}

						float4 frag(v2f i) : SV_Target
						{
							fixed4 col = tex2D(_MainTex, i.uv) * _Color;
							if (_Dither > 0 && _Dither <= 1)
								col *= DitherSome(i.uv);
							if (_Dither > 1 && _Dither <= 2)
								col *= DitherHalf(i.uv);
							if (_Dither > 2 && _Dither <= 3)
								col *= (1.25f - DitherSome(i.uv));


							return col;
						}
						ENDCG
					}

				Pass
				{
					Tags { "LightMode" = "NormalsRendering"}

					HLSLPROGRAM
					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

					#pragma vertex NormalsRenderingVertex
					#pragma fragment NormalsRenderingFragment

					struct Attributes
					{
						float3 positionOS   : POSITION;
						float4 color        : COLOR;
						float2 uv           : TEXCOORD0;
						float4 tangent      : TANGENT;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					struct Varyings
					{
						float4  positionCS      : SV_POSITION;
						half4   color           : COLOR;
						float2  uv              : TEXCOORD0;
						half3   normalWS        : TEXCOORD1;
						half3   tangentWS       : TEXCOORD2;
						half3   bitangentWS     : TEXCOORD3;
						UNITY_VERTEX_OUTPUT_STEREO
					};

					TEXTURE2D(_MainTex);
					SAMPLER(sampler_MainTex);
					TEXTURE2D(_NormalMap);
					SAMPLER(sampler_NormalMap);
					half4 _NormalMap_ST;  // Is this the right way to do this?

					Varyings NormalsRenderingVertex(Attributes attributes)
					{
						Varyings o = (Varyings)0;
						UNITY_SETUP_INSTANCE_ID(attributes);
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

						o.positionCS = TransformObjectToHClip(attributes.positionOS);
						o.uv = TRANSFORM_TEX(attributes.uv, _NormalMap);
						o.color = attributes.color;
						o.normalWS = -GetViewForwardDir();
						o.tangentWS = TransformObjectToWorldDir(attributes.tangent.xyz);
						o.bitangentWS = cross(o.normalWS, o.tangentWS) * attributes.tangent.w;
						return o;
					}

					#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"

					half4 NormalsRenderingFragment(Varyings i) : SV_Target
					{
						const half4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
						const half3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv));

						return NormalsRenderingShared(mainTex, normalTS, i.tangentWS.xyz, i.bitangentWS.xyz, i.normalWS.xyz);
					}
					ENDHLSL
				}
					
				Pass
				{
					Tags { "LightMode" = "UniversalForward" "Queue" = "Transparent" "RenderType" = "Transparent"}

					HLSLPROGRAM
					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

					#pragma vertex UnlitVertex
					#pragma fragment UnlitFragment

					struct Attributes
					{
						float3 positionOS   : POSITION;
						float4 color        : COLOR;
						float2 uv           : TEXCOORD0;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					struct Varyings
					{
						float4  positionCS      : SV_POSITION;
						float4  color           : COLOR;
						float2  uv              : TEXCOORD0;
						#if defined(DEBUG_DISPLAY)
						float3  positionWS  : TEXCOORD2;
						#endif
						UNITY_VERTEX_OUTPUT_STEREO
					};

					TEXTURE2D(_MainTex);
					SAMPLER(sampler_MainTex);
					float4 _MainTex_ST;

					Varyings UnlitVertex(Attributes attributes)
					{
						Varyings o = (Varyings)0;
						UNITY_SETUP_INSTANCE_ID(attributes);
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

						o.positionCS = TransformObjectToHClip(attributes.positionOS);
						#if defined(DEBUG_DISPLAY)
						o.positionWS = TransformObjectToWorld(v.positionOS);
						#endif
						o.uv = TRANSFORM_TEX(attributes.uv, _MainTex);
						o.color = attributes.color;
						return o;
					}

					float4 UnlitFragment(Varyings i) : SV_Target
					{
						float4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

						#if defined(DEBUG_DISPLAY)
						SurfaceData2D surfaceData;
						InputData2D inputData;
						half4 debugColor = 0;

						InitializeSurfaceData(mainTex.rgb, mainTex.a, surfaceData);
						InitializeInputData(i.uv, inputData);
						SETUP_DEBUG_DATA_2D(inputData, i.positionWS);

						if (CanDebugOverrideOutputColor(surfaceData, inputData, debugColor))
						{
							return debugColor;
						}
						#endif

						return mainTex;
					}
					ENDHLSL
				}

			}
	Fallback "Sprites/Default"
}
