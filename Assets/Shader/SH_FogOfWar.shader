Shader "Custom/SH_FogOfWar"
{
     Properties
    {
        _Color ("Fog Color", Color) = (0,0,0,1)
        _Center ("Center (UV)", Vector) = (0.5, 0.5, 0, 0)
        _Radius ("Radius", Range(0,1)) = 0.25
        _Softness ("Edge Softness", Range(0.001,0.2)) = 0.05
        _ViewScale ("View Scale (X,Y)", Vector) = (1, 0.7, 0, 0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            float4 _Center;
            float _Radius;
            float _Softness;
            float4 _ViewScale;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Offset dari center
                float2 delta = i.uv - _Center.xy;

                delta *= _ViewScale.xy;

                // Jarak elips
                float dist = length(delta);

                // Fog alpha (luar hitam, dalam transparan)
                float alpha = smoothstep(_Radius - _Softness, _Radius, dist);

                return fixed4(_Color.rgb, alpha * _Color.a);
            }
            ENDCG
        }
    }
}
