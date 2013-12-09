uniform extern texture ScreenTexture;

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;
};

texture2D noisefilter;
sampler NoiseS = sampler_state
{
	Texture = <noisefilter>;
};

float blkLine;

float4 PixelShaderFunction(float2 texCoord: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(ScreenS, texCoord);
	float4 noiseColor =  tex2D(NoiseS, texCoord);

	float average = (noiseColor[0] + noiseColor[1] + noiseColor[2]) / 3;

	if(average > .5)
	{
		color[0] = 0;
		color[1] = 0;
		color[2] = 0;
	}

	if((texCoord[0] - blkLine) < 0.001f)
	{
		if((texCoord[0] - blkLine) > -0.001f)
		{
			color[0] = 0;
			color[1] = 0;
			color[2] = 0;
		}
	}

    return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
