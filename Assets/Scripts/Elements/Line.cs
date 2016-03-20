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
		positionArray = new Vector3[50];
	}

	public void UpdateHeadPosition (Vector3 position)
	{
		renderer.SetPosition(0, position);
	}
}