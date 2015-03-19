// Shader created with Shader Forge Beta 0.28 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.28;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:True,hqlp:False,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32519,y:32823|diff-111-OUT,emission-19-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:33115,y:32838,ptlb:emissive,ptin:_emissive,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:14,x:33769,y:33212;n:type:ShaderForge.SFN_Multiply,id:16,x:32901,y:33019|A-8-RGB,B-68-RGB;n:type:ShaderForge.SFN_ValueProperty,id:17,x:33223,y:33316,ptlb:power,ptin:_power,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:19,x:32782,y:33185|A-16-OUT,B-38-OUT;n:type:ShaderForge.SFN_Multiply,id:38,x:33023,y:33398|A-17-OUT,B-59-OUT;n:type:ShaderForge.SFN_Clamp,id:45,x:33266,y:33418|MAX-47-OUT;n:type:ShaderForge.SFN_Vector1,id:46,x:33741,y:33404,v1:2;n:type:ShaderForge.SFN_Vector1,id:47,x:33532,y:33508,v1:1;n:type:ShaderForge.SFN_Add,id:59,x:33329,y:33575|A-74-OUT,B-47-OUT;n:type:ShaderForge.SFN_Color,id:68,x:33101,y:33113,ptlb:color,ptin:_color,glob:False,c1:0,c2:1,c3:0.9586205,c4:1;n:type:ShaderForge.SFN_Sin,id:74,x:33433,y:33247|IN-14-TTR;n:type:ShaderForge.SFN_Divide,id:95,x:33582,y:33301|A-14-TTR,B-46-OUT;n:type:ShaderForge.SFN_Multiply,id:111,x:32901,y:32724|A-112-RGB,B-8-RGB;n:type:ShaderForge.SFN_Color,id:112,x:33243,y:32668,ptlb:node_112,ptin:_node_112,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:8-17-68-112;pass:END;sub:END;*/

Shader "Shader Forge/cristaux" {
    Properties {
        _emissive ("emissive", 2D) = "white" {}
        _power ("power", Float ) = 1
        _color ("color", Color) = (0,1,0.9586205,1)
        _node_112 ("node_112", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _emissive; uniform float4 _emissive_ST;
            uniform float _power;
            uniform float4 _color;
            uniform float4 _node_112;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz;
////// Emissive:
                float2 node_136 = i.uv0;
                float4 node_8 = tex2D(_emissive,TRANSFORM_TEX(node_136.rg, _emissive));
                float4 node_14 = _Time + _TimeEditor;
                float node_47 = 1.0;
                float3 emissive = ((node_8.rgb*_color.rgb)*(_power*(sin(node_14.a)+node_47)));
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_node_112.rgb*node_8.rgb);
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _emissive; uniform float4 _emissive_ST;
            uniform float _power;
            uniform float4 _color;
            uniform float4 _node_112;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float2 node_137 = i.uv0;
                float4 node_8 = tex2D(_emissive,TRANSFORM_TEX(node_137.rg, _emissive));
                finalColor += diffuseLight * (_node_112.rgb*node_8.rgb);
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
