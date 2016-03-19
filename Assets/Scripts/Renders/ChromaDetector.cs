using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (FrameBuffer))]
public class ChromaDetector : MonoBehaviour 
{
	SoundGenerator soundGenerator;
	FrameBuffer frameBuffer;
	Texture2D texture2D;
	RenderTexture renderTexture;
	Color[] colorArray;
	Rect rect;
	Vector2 position;
	int width = 256;
	int height = 256;
	public Vector2 targetPosition1;
	public Vector2 targetPosition2;
	public Vector2 targetPosition3;

	void Start ()
	{
		soundGenerator = GameObject.FindObjectOfType<SoundGenerator>();
		frameBuffer = GetComponent<FrameBuffer>();
		renderTexture = frameBuffer.GetCurrentTexture();

		rect = new Rect(0f, 0f, width, height);
		texture2D = new Texture2D(width, height);
		colorArray = new Color[width * height];
		position = Vector2.zero;
	}

	void Update () 
	{
		renderTexture = frameBuffer.GetCurrentTexture();
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(rect, 0, 0, false);
		texture2D.Apply(false);
		position = Vector2.zero;

		colorArray = texture2D.GetPixels();
		int index = 0;
		Vector2 target1 = Vector2.zero;
		Vector2 target2 = Vector2.zero;
		Vector2 target3 = Vector2.zero;
		int count1 = 0;
		int count2 = 0;
		int count3 = 0;
		foreach (Color color in colorArray) {
			position.x = (index % width);
			position.y = Mathf.Floor(index / width);
			if (color.r - color.g - color.b >= 1f) {
				target1.x += position.x;
				target1.y += position.y;
				++count1;
			} else if (color.g - color.r - color.b >= 1f) {
				target2.x += position.x;
				target2.y += position.y;
				++count2;
			} else if (color.b - color.r - color.g >= 1f) {
				target3.x += position.x;
				target3.y += position.y;
				++count3;
			}
			++index;
		}

		if (count1 > 30) {
			target1.x = (target1.x / count1) / (float)width;
			target1.y = (target1.y / count1) / (float)height;
			targetPosition1 = target1;

			soundGenerator.SetStartFrequency(target1.x);
		}

		if (count2 > 30) {
			target2.x = (target2.x / count2) / (float)width;
			target2.y = (target2.y / count2) / (float)height;
			targetPosition2 = target2;

			// soundGenerator.SetStartFrequency(target2.x);
		}

		if (count3 > 30) {
			target3.x = (target3.x / count3) / (float)width;
			target3.y = (target3.y / count3) / (float)height;
			targetPosition3 = target3;

			// soundGenerator.SetStartFrequency(target3.x);
		}
	}

	public void UpdateResolution ()
	{
		rect = new Rect(0f, 0f, width, height);
		texture2D = new Texture2D(width, height);
		colorArray = new Color[width * height];
	}
}