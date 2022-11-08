Shader "Unlit/Meat"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass{
            Color[_Color]
            SetTexture[_MainTex]{ combine texture * primary }
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
