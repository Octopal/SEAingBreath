// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Background" {
	Properties {
		_Counter("Counter", float) = 0
		_BgColor("BgColor", Color) = (0, 0, 0, 1)
		_PlayerPos("PlayerPosition", Vector) = (0, 0, 0, 0)
		_Velocity("Velocity", Vector) = (0, 0, 0, 0)
	}
	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Transparent"  }
		Blend SrcAlpha One
		Pass{
			CGPROGRAM
			#pragma vertex MyVertexProgram
			#pragma target 4.0
			#pragma fragment MyFragmentProgram
			#include "UnityCG.cginc"

			#define NUM_LINES 20
			uniform fixed _Counter;
			uniform fixed4 _PlayerPos;
			uniform fixed4 _Velocity;
			uniform fixed4 _BgColor;

			fixed bump(fixed d) {
    			return 0.496/(0.344 + sqrt(d));
			}

			fixed2x2 rot(fixed angle) {
				fixed s = sin(angle);
				fixed c = cos(angle);
				return fixed2x2(c,-s, s, c);
			}

			struct Interpolators {
				fixed4 position : POSITION ;
				fixed2 uv : TEXCOORD0 ;
			};

			struct VertexData {
				fixed4 position : POSITION ;
				fixed2 uv : TEXCOORD0;
				fixed3 normal : NORMAL;
            	fixed4 tangent : TANGENT;
			};

			Interpolators MyVertexProgram ( VertexData v ) {
				Interpolators i;
				i.uv = v.uv;
				v.normal = v.position.xyz;
				i.position = UnityObjectToClipPos(v.position);
				return i;
			}

			fixed4 MyFragmentProgram (Interpolators i) : SV_TARGET {
				fixed3 col = 0;
				i.uv *= 2;
				i.uv -= 1;
				const fixed tau =2.0*atan2(10.0, 12.0);
			    fixed c = length(i.uv-_PlayerPos.xy/10.+fixed2(23, 3.5));
//			    c = smoothstep(frac(c*10.0), 0.4, 0.1);
				fixed vx = 2.2;
				fixed vy = 2.;
			    for(int j = 0; j < NUM_LINES; j++) {
			        fixed ifl = fixed(j) / fixed( NUM_LINES )*10.0;
			        fixed angle = sin(ifl*tau*5.2 + _Counter + cos(j)*5.)* 0.2;//(vx*0.3+0.7);
			        fixed offs = cos(ifl*tau + _Counter)* 5.2;//(vy*0.5+4.2);
			        fixed2x2 a1 = rot(angle);
			        fixed2 a2 = mul(i.uv-fixed2(0, 1.6), a1);
			        fixed b = bump(abs(a2 - offs*.1)*1.924);

			    	col += smoothstep(.98*b, .16, .56) ;
			    }

			    // for lerping
				fixed4 black = fixed4(0, 0, 0, 0);

			    // bottom gradient
				fixed2 st2 = i.uv+fixed2(0, 1);
				fixed4 bottomGrad = st2.y;
				bottomGrad = lerp(black, fixed4(1, 1, 1, 1), bottomGrad);

			    col *= 0.8;
			    col *= _BgColor.xyz;
			    fixed test = cos(_Time*50.);
			    fixed3 color = col;

				color = abs(color * bottomGrad) ;

				return fixed4(color, 1.0);


			};
			ENDCG
		}

	}
	FallBack "Diffuse"
}
