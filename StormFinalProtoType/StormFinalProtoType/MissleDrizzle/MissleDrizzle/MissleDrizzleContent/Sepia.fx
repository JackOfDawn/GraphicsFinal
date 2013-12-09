uniform extern texture ScreenTexture;

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;
};

float4 SepiaFunction(float2 texCoord: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(ScreenS, texCoord);

	float redOutput = ((color[0] * .393) + (color[1] * .796) + (color[2] * .189)) / 1.5;
	if(redOutput > 255)
	{
		redOutput = 255;
	}

	float greenOutput = ((color[0] * .349) + (color[1] *.686) + (color[2] * .168)) / 1.5;
	if(greenOutput > 255)
	{
		greenOutput = 255;
	}

	float blueOutput = ((color[0] * .272) + (color[1] * .534) + (color[2] * .131)) / 1.5;
	if(blueOutput > 255)
	{
		blueOutput = 255;
	}

	color[0] = redOutput;
	color[1] = greenOutput;
	color[2] = blueOutput;

	return color;
}

technique Technique1
{
    pass PO
    {
        PixelShader = compile ps_2_0 SepiaFunction();
    }
}
