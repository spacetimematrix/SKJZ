Shader "SKJZ/Unity_4LightPosX0.x0" {
	Properties {
		_Color ("Base Color", Color) = (1,1,1,1)
	}
	SubShader {
		Pass{
			Tags {"LightMode"="Vertex"}
			Blend Zero One
			CGPROGRAM
			#pragam vertex vert
			#pragam fragment frag
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			uniform float4 _Color;

			struct vertOut{
				float4 pos:SV_POSITION;
				float4 color:COLOR;
			};

			vertOut vert(appdata_base v){
				vertOut o;
				o.pos=mul(UNITY_MATRIX_MVP, v.vertex);
				o.color=unity_4LightPosX0[0];

				return o;
			}

			float4 frag(vertOut i):COLOR{
				return i.color;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
