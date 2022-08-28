// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Unlit shader. Simplest possible colored shader.
// - no lighting
// - no lightmap support
// - no texture

Shader "Stencil/Unlit-Color" {
Properties {
    [IntRange] _StencilComp ("Stencil Comp Value", Range(0,255)) = 0
    _Color ("Main Color", Color) = (1,1,1,1)
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
    LOD 100

    Cull off

    ZWrite Off
    Lighting Off
    Fog { Mode Off }

    //stencil operation
        Stencil{
            Ref 1
            Comp [_StencilComp]
        }

    Blend SrcAlpha OneMinusSrcAlpha 

    Pass {
        Color [_Color]
        SetTexture [_MainTex] { combine texture * primary } 
    }
}

}
