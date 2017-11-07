﻿struct VertexInputType
{
	float3 Position : POSITION0;
	float2 UV : TEXCOORD0;
	float3 Normal : NORMAL0;
	float2 Shade : TEXCOORD1;
	float4 Color : COLOR0;
};

struct PixelInputType
{
	float4 Position : SV_POSITION;
	float2 UV : TEXCOORD0;
	float2 Shade : TEXCOORD1;
	float4 Color : TEXCOORD2;
};

float4x4 ModelViewProjection;

float4 Color;
bool TextureEnabled;

Texture2D Texture;
sampler TextureSampler;
float2 ReciprocalTextureSize;

PixelInputType VS(VertexInputType input)
{
	PixelInputType output;
	output.Position = mul(float4(input.Position, 1.0f), ModelViewProjection);
	output.UV = input.UV * ReciprocalTextureSize;
	output.Shade = input.Shade;
	output.Color = Color;
	return output;
}

////////////////////////////////////////////////////////////////////////////////
// Pixel Shader
////////////////////////////////////////////////////////////////////////////////
float4 PS(PixelInputType input) : SV_TARGET
{
	float4 pixel = float4(input.UV.x, input.UV.y, input.UV.x, 1.0f);

	if (TextureEnabled)
		pixel = Texture.Sample(TextureSampler, input.UV);

	float3 colorMul = min(input.Color.xyz, 1.0f);
	pixel.xyz = pixel.xyz * colorMul;
	pixel.w *= input.Color.w;

	return pixel;
}

technique10 Textured
{
	pass P0
	{
		SetVertexShader(CompileShader(vs_4_0, VS()));
		SetGeometryShader(NULL);
		SetPixelShader(CompileShader(ps_4_0, PS()));
	}
}