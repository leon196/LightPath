using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour 
{
	LineRenderer renderer;
	Vector2 lastPosition;
	float currentDistance;
	int vertexCount;
	Vector3[] positionArray;

	void Awake ()
	{
		vertexCount = 0;
		lastPosition = Vector2.zero;
		positionArray = new Vector3[Master.lineMaxVertexCount];

		renderer = gameObject.AddComponent<LineRenderer>();
		renderer.material = new Material(Shader.Find("Unlit/Line"));
		renderer.SetWidth(0f, Master.lineEndWidth);
	}

	public void SetColor (Color color)
	{
		renderer.material.color = color;
	}

	public void UpdateHeadPosition (Vector3 position)
	{
		if (Vector2.Distance(lastPosition, position) > Master.lineMaxSegmentDistance) 
		{
			lastPosition = position;
			if (vertexCount < positionArray.Length) 
			{
				++vertexCount;
				renderer.SetVertexCount(vertexCount);
			} 
			else 
			{
				for (int i = 0; i < vertexCount - 1; ++i) 
				{
					positionArray[i] = positionArray[i + 1];
					renderer.SetPosition(i, positionArray[i]);
				}
			}
		}
		positionArray[vertexCount - 1] = position;
		renderer.SetPosition(vertexCount - 1, position);
	}
}