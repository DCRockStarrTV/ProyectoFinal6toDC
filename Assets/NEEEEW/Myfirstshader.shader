Shader "Misshaders/Myfirstshader"
{
    Properties
    {
        MyColor("QUE CHUCHAS COLOR QUIERES", Color) = (1,1,1,1)
        MyEmission("El otro color supongo", Color) = (1,1,1,1)
        MyNormal("El tercero xd", Color) = (1,1,1,1)
        MyMulty("Multy", int) = 2
       
          

        
    }
        SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
             float2 uvMainTex;
        };
        
        fixed4 MyColor;
        fixed4 MyEmission;
        fixed4 MyNormal;
        fixed MyMulty;

        void surf(Input IN, inout SurfaceOutput o) 
        {
            o.Albedo = MyColor.rgb * MyMulty;
            o.Emission = MyEmission.xyz;
            o.Normal = MyNormal.rgb;

        }

        ENDCG
    }
    FallBack "Diffuse"
}
