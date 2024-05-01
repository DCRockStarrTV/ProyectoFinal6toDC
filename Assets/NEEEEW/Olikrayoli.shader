Shader "Custom/Olikrayoli"
{
    Properties
    {
        MyColor("Coloritos",color) = (1,1,1,1)
        MyRange("Ranguito", Range(0,10)) = 1
        _MyTex("Texturita",2D) = "white"{}
        MyCube("Cubito",CUBE) = ""{}
        MyFloat("Flotante",Float) = 0.5
        MyVector("Vectooor",Vector) = (1,1,1,1)
    }
        SubShader
        {

            CGPROGRAM
            #pragma surface surf Lambert

            fixed4 MyColor;
            half MyRange;
            sampler2D _MyTex;
            samplerCUBE MyCube;
            float MyFloat;
            float MyVector;

        struct Input {
        float2 uv_MyTex;
        float3 worldRefl;
};
       
        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_MyTex, IN.uv_MyTex).rgb * 5;
            o.Emission = texCUBE(MyCube, IN.worldRefl).rgb;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
