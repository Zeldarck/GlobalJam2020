// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "M_GroundPaint"
{
	Properties
	{
		_ColorPaint("ColorPaint", Color) = (0,0,0,0)
		_Base_Texture("Base_Texture", 2D) = "white" {}
		_Tiling("Tiling", Float) = 1
		_Ground_Game_normal("Ground_Game_normal", 2D) = "bump" {}
		_Normal_ColorPaint("Normal_ColorPaint", 2D) = "white" {}
		_Ground_Game_roughness("Ground_Game_roughness", 2D) = "white" {}
		_AO_ColorPaint("AO_ColorPaint", 2D) = "white" {}
		_Smoothness_ColorPaint("Smoothness_ColorPaint", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform sampler2D _Ground_Game_normal;
		uniform float _Tiling;
		uniform sampler2D _Normal_ColorPaint;
		uniform sampler2D _Base_Texture;
		uniform float4 _ColorPaint;
		uniform sampler2D _Ground_Game_roughness;
		uniform float _Smoothness_ColorPaint;
		uniform sampler2D _AO_ColorPaint;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float Tiles13 = _Tiling;
			float2 temp_cast_0 = (Tiles13).xx;
			float2 temp_cast_2 = (Tiles13).xx;
			float4 lerpResult11 = lerp( float4( UnpackNormal( tex2D( _Ground_Game_normal, temp_cast_0 ) ) , 0.0 ) , tex2D( _Normal_ColorPaint, temp_cast_2 ) , i.vertexColor);
			o.Normal = lerpResult11.rgb;
			float2 temp_cast_4 = (Tiles13).xx;
			float2 uv_TexCoord7 = i.uv_texcoord * temp_cast_4;
			float4 lerpResult3 = lerp( tex2D( _Base_Texture, uv_TexCoord7 ) , _ColorPaint , i.vertexColor);
			o.Albedo = lerpResult3.rgb;
			float2 temp_cast_6 = (Tiles13).xx;
			float4 temp_cast_7 = (_Smoothness_ColorPaint).xxxx;
			float4 lerpResult21 = lerp( tex2D( _Ground_Game_roughness, temp_cast_6 ) , temp_cast_7 , i.vertexColor);
			o.Smoothness = lerpResult21.r;
			float2 temp_cast_9 = (Tiles13).xx;
			o.Occlusion = tex2D( _AO_ColorPaint, temp_cast_9 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17600
-1366;326;1366;707;851.5438;333.2483;1.306583;True;True
Node;AmplifyShaderEditor.RangedFloatNode;9;-1703.076,-328.8538;Inherit;False;Property;_Tiling;Tiling;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-1477.303,-326.1902;Inherit;False;Tiles;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;14;-1366.619,-62.56314;Inherit;False;13;Tiles;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-997.4516,-76.39455;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;25;-545.5573,-251.6488;Inherit;False;13;Tiles;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;15;-1045.692,573.268;Inherit;False;13;Tiles;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;-761.51,776.9272;Inherit;True;Property;_Normal_ColorPaint;Normal_ColorPaint;4;0;Create;True;0;0;False;0;-1;02271bbcf45a7d04e9214b7cf18d2227;02271bbcf45a7d04e9214b7cf18d2227;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;24;-243.7677,757.2481;Inherit;False;13;Tiles;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-329.1732,-103.2538;Inherit;True;Property;_Ground_Game_roughness;Ground_Game_roughness;5;0;Create;True;0;0;False;0;-1;7be03089d2cd4b24192179243033a3f2;7be03089d2cd4b24192179243033a3f2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-457.7617,551.5679;Inherit;True;Property;_Ground_Game_normal;Ground_Game_normal;3;0;Create;True;0;0;False;0;-1;ffc47f899221b38478d0948086ff2b88;ffc47f899221b38478d0948086ff2b88;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-1002.195,277.8351;Inherit;False;Property;_ColorPaint;ColorPaint;0;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;2;-707.5953,407.835;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-731.9281,-104.8615;Inherit;True;Property;_Base_Texture;Base_Texture;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-116.3286,126.6628;Inherit;False;Property;_Smoothness_ColorPaint;Smoothness_ColorPaint;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;11;-115.4302,356.3475;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;3;-408.5956,257.035;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;20;-43.48077,582.7539;Inherit;True;Property;_AO_ColorPaint;AO_ColorPaint;6;0;Create;True;0;0;False;0;-1;9114aceaf3ad84a4a9794bc1e3a13e59;9114aceaf3ad84a4a9794bc1e3a13e59;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;21;121.9901,-63.52515;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;357.9491,7.298383;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;M_GroundPaint;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;9;0
WireConnection;7;0;14;0
WireConnection;16;1;15;0
WireConnection;19;1;25;0
WireConnection;10;1;15;0
WireConnection;8;1;7;0
WireConnection;11;0;10;0
WireConnection;11;1;16;0
WireConnection;11;2;2;0
WireConnection;3;0;8;0
WireConnection;3;1;5;0
WireConnection;3;2;2;0
WireConnection;20;1;24;0
WireConnection;21;0;19;0
WireConnection;21;1;23;0
WireConnection;21;2;2;0
WireConnection;0;0;3;0
WireConnection;0;1;11;0
WireConnection;0;4;21;0
WireConnection;0;5;20;0
ASEEND*/
//CHKSM=EA08086747A628AEA1DD288307EDF27784E366FD