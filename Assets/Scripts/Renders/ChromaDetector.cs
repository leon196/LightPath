using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (FrameBuffer))]
public class ChromaDetector : MonoBehaviour 
{
	FrameBuffer frameBuffer;
	Texture2D texture2D;
	RenderTexture renderTexture;
	Color[] colorArray;
	Rect rect;
	Vector2 position;
	int width = 256;
	int height = 256;
	public Vector2 targetPosition;

	void Start ()
	{
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
		Vector2 target = Vector2.zero;
		int count = 0;
		foreach (Color color in colorArray) {
			position.x = (index % width);
			position.y = Mathf.Floor(index / width);
			// if (color.r + color.g + color.b >= 2f) {
			if (color.r - color.g - color.b >= 1f) {
			// if (color.g - color.r - color.b <= 0f) {
				target.x += position.x;
				target.y += position.y;
				++count;
			}
			++index;
		}

		if (count > 0) {
			target.x = (target.x / count) / (float)width;
			target.y = (target.y / count) / (float)height;
			targetPosition = target;
		}
	}

	public void UpdateResolution ()
	{
		rect = new Rect(0f, 0f, width, height);
		texture2D = new Texture2D(width, height);
		colorArray = new Color[width * height];
	}
}