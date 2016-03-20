using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChromaDetector : MonoBehaviour 
{
	// SoundGenerator soundGenerator;
	UI ui;
	Texture2D texture2D;
	RenderTexture renderTexture;
	Color[] colorArray;
	Rect rect;
	Vector2 position;
	public Vector2 targetPosition1;
	public Vector2 targetPosition2;
	public Vector2 targetPosition3;

	void Start ()
	{
		// soundGenerator = GameObject.FindObjectOfType<SoundGenerator>();
		ui = GameObject.FindObjectOfType<UI>();
		renderTexture = GetComponent<Camera>().targetTexture;

		rect = new Rect(0f, 0f, Master.width, Master.height);
		texture2D = new Texture2D(Master.width, Master.height);
		colorArray = new Color[Master.width * Master.height];
		position = Vector2.zero;
	}

	void Update () 
	{
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
			position.x = (index % Master.width);
			position.y = Mathf.Floor(index / Master.width);
			if (color.r == 1f && color.g == 0f && color.b == 0f) {
				target1.x += position.x;
				target1.y += position.y;
				++count1;
			} else if (color.r == 0f && color.g == 1f && color.b == 0f) {
				target2.x += position.x;
				target2.y += position.y;
				++count2;
			} else if (color.r == 0f && color.g == 0f && color.b == 1f) {
				target3.x += position.x;
				target3.y += position.y;
				++count3;
			}
			++index;
		}

		if (count1 > 0) {
			target1.x = (target1.x / count1) / (float)Master.width;
			target1.y = (target1.y / count1) / (float)Master.height;
			targetPosition1 = target1;

			// soundGenerator.SetStartFrequency(target1.x);
		}

		if (count2 > 0) {
			target2.x = (target2.x / count2) / (float)Master.width;
			target2.y = (target2.y / count2) / (float)Master.height;
			targetPosition2 = target2;

			// soundGenerator.SetStartFrequency(target2.x);
		}

		if (count3 > 0) {
			target3.x = (target3.x / count3) / (float)Master.width;
			target3.y = (target3.y / count3) / (float)Master.height;
			targetPosition3 = target3;

			// soundGenerator.SetStartFrequency(target3.x);
		}
	}

	public void UpdateResolution ()
	{
		rect = new Rect(0f, 0f, Master.width, Master.height);
		texture2D = new Texture2D(Master.width, Master.height);
		colorArray = new Color[Master.width * Master.height];
	}
}