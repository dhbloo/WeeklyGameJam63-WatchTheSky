Shader "HightlightEffect/DrawShader"
{
	Properties
	{
		_Color ("Highlight Color", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		 Pass {
            Tags {"RenderType"="Opaque"}
            ZWrite On ZTest Always Fog { Mode Off }
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            fixed4 _Color;
            float4 vert(float4 v:POSITION) : POSITION {
                return UnityObjectToClipPos (v);
            }
            fixed4 frag() : SV_Target {
                return fixed4(_Color.rgb * _Color.a, 1.0);
            }
            ENDCG
        }    
	}
}
