Shader "Custom/Portal"
{
	SubShader
	{
        Tags{ "RenderType"="Opaque" }

		Zwrite off
		ColorMask 0
		Cull off

		Stencil{
			Ref 1
            Comp Always
			Pass replace
		}

		Pass
		{
            Blend Zero One
            ZWrite Off
		}
	}
}
