using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChromaDetector : MonoBehaviour 
{
	Texture2D texture2D;
	RenderTexture renderTexture;
	Color[] colorArray;
	Rect rect;
	Camera cameraUI;

	public Light[] lightArray;

	void Start ()
	{
		cameraUI = GameObject.FindObjectOfType<UI>().GetComponent<Camera>();
		renderTexture = GetComponent<Camera>().targetTexture;
		rect = new Rect(0f, 0f, Master.width, Master.height);
		texture2D = new Texture2D(Master.width, Master.height);
		colorArray = new Color[Master.width * Master.height];

		lightArray = GameObject.FindObjectsOfType<Light>();

		if (lightArray.Length > 0) {
			lightArray[0].colorIntegerCode = new Color(0,0,1,1);
			if (lightArray.Length > 1) {
				lightArray[1].colorIntegerCode = new Color(0,1,0,1);
				if (lightArray.Length > 2) {
					lightArray[2].colorIntegerCode = new Color(1,0,0,1);
					if (lightArray.Length > 3) {
						lightArray[3].colorIntegerCode = new Color(1,0,1,1);
					}
				}
			}
		}

		for (int i = 0; i < lightArray.Length; ++i)
		{
			Light light = lightArray[i];
			Shader.SetGlobalColor("_Color" + (i + 1), light.color);
			Shader.SetGlobalColor("_ColorIntegerCode" + (i + 1), light.colorIntegerCode);
			Shader.SetGlobalFloat("_ColorTreshold" + (i + 1), light.treshold);
		}
	}

	void Update () 
	{
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(rect, 0, 0, false);
		texture2D.Apply(false);

		colorArray = texture2D.GetPixels();
		int index = 0;

		foreach (Light light in lightArray)
		{
			light.targetGlobal = Vector2.zero;
			light.pixelCount = 0;
		}

		foreach (Color pixelColor in colorArray) 
		{
			foreach (Light light in lightArray)
			{
				if ( pixelColor.r == light.colorIntegerCode.r 
					&& pixelColor.g == light.colorIntegerCode.g 
					&& pixelColor.b == light.colorIntegerCode.b) 
				{
					light.targetGlobal.x += (index % Master.width);
					light.targetGlobal.y += Mathf.Floor(index / Master.width);
					++light.pixelCount;
					break;
				}
			}
			++index;
		}

		for (int i = 0; i < lightArray.Length; ++i)
		{
			Light light = lightArray[i];

			if (light.pixelCount > 0) 
			{
				light.targetGlobal.x = (light.targetGlobal.x / light.pixelCount) / (float)Master.width;
				light.targetGlobal.y = (light.targetGlobal.y / light.pixelCount) / (float)Master.height;

				light.worldPosition = cameraUI.ViewportToWorldPoint(light.targetGlobal);
				light.worldPosition.z = 0.5f;

				light.UpdateHeadPosition();
			}

			Shader.SetGlobalColor("_Color" + (i + 1), light.color);
			Shader.SetGlobalFloat("_ColorTreshold" + (i + 1), light.treshold);
		}
	}
}