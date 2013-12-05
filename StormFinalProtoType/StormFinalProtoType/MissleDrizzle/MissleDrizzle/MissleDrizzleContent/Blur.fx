uniform extern texture ScreenTexture;

sampler ScreenS = sampler_state
{
	Texture = <ScreenTexture>;
};

float4 BlurFunction (float2 curCoord: TEXCOORD0) : COLOR
{
	float2 center = {0.5f, 0.5f};
	float maxDistSQR = 0.7071f;

	float2 diff = abs(curCoord - center);
	float distSQR = length(diff);

	float blurAmount = (distSQR / maxDistSQR) / 100.0f;

	float2 prevCoord = curCoord;
	prevCoord[0] -= blurAmount;

	float2 nextCoord = curCoord;
	nextCoord += blurAmount;

	float4 color = ((tex2D(ScreenS, curCoord)
					+ tex2D(ScreenS, prevCoord)
					+ tex2D(ScreenS, nextCoord)) / 3.0f);

	return color;
}

float4 SepiaFunction(float2 texCoord: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(ScreenS, texCoord);

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

technique
{
	pass PO
	{
		PixelShader = compile ps_2_0 BlurFunction();
	}

	pass P1
	{
		PixelShader = compile ps_2_0 SepiaFunction();
	}
}