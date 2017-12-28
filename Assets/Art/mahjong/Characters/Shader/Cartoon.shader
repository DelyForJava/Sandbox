Shader "Custom/Cartoon" {
	Properties {
		//Material Properties
		_Color("Color", Color) = (0.931, 0.931, 0.931, 0.95)
		_MainTex("Diffuse", 2D) = "white" {}
		_LightInfo("LightInfo", 2D) = "white"{}
		//ShadowProperties
		_LightArea("LightArea", Range(0,1)) = 0.5
		_FirstShadowMultiColor("FirstShadowMultiColor", Color) = (0.6,0.6,0.6,0.0)
		_SecondShadow("SecondShadow", Range(0,1)) = 0.5
		_SecondShadowMultiColor("SecondShadowMultiColor", Color) = (0.7, 0.7,0.7,0.0)
		_Shininess("Shininess",Float) = 10
		_SpecularColor("SpecularColor", Color) = (1,1,1,0)
		_SpecularMulti("SpecularMulti",Float) = 0.2
		_BloomFactor("BloomFactor", Float) = 1

		_OutlineScale("Scale", Float) = 0.01
		_OutlineZOffset("OutlineZOffset", Float) = 1.0
		_OutlineThickness ("Outline Thickness", Float) = 0.037
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
	}
	SubShader {
		Tags {"RenderType"="Opaque"}
		UsePass "Custom/CartoonLighting/LIGHTING"
		UsePass "Custom/CartoonOutline/OUTLINE"
	}
	Fallback "Diffuse"
}
