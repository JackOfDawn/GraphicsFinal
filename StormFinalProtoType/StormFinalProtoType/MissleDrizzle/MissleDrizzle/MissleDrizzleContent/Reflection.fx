uniform extern texture ScreenTexture;

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;
};


float4 SepiaReflect(float2 texCoord: TEXCOORD0) : COLOR0
{

	//Reflects
	float4 color = tex2D(ScreenS, texCoord);
    // TODO: add your pixel shader code here.
	if(texCoord[0] > 0.5f)
	{
		float2 location = texCoord;
		location[0] = 0.5f - (texCoord[0] - 0.5f);
		color = tex2D(ScreenS, location);
	}


	//Sepia
	float redOutput = (color[0] * .393) + (color[1] * .796) + (color[2] * .189);
	if(redOutput > 255)
	{
		redOutput = 255;
	}

	float greenOutput = (color[0] * .349) + (color[1] *.686) + (color[2] * .168);
	if(greenOutput > 255)
	{
		greenOutput = 255;
	}

	float blueOutput = (color[0] * .272) + (color[1] * .534) + (color[2] * .131);
	if(blueOutput > 255)
	{
		blueOutput = 255;
	}

	color[0] = redOutput;
	color[1] = greenOutput;
	color[2] = blueOutput;


	return color;
}

float4 Reflect(float2 texCoord: TEXCOORD0) : COLOR0
{

	//Reflects
	float4 color = tex2D(ScreenS, texCoord);
    // TODO: add your pixel shader code here.
	if(texCoord[0] > 0.5f)
	{
		float2 location = texCoord;
		location[0] = 0.5f - (texCoord[0] - 0.5f);
		color = tex2D(ScreenS, location);
	}

	return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 SepiaReflect();
    }
	    pass Pass2
    {
        PixelShader = compile ps_2_0 Reflect();
    }
}
