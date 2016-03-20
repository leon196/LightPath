using UnityEngine;
using System.Collections;

public class Light : MonoBehaviour 
{
	public Color color;
	public float treshold;
	
	[HideInInspector] public int pixelCount;
	[HideInInspector] public Vector2 targetGlobal;
	[HideInInspector] public Vector3 worldPosition;
	[HideInInspector] public Color colorIntegerCode;

	Line line;

	void Start () 
	{
		line = GetComponent<Line>();
		line.SetColor(color);
	}

	public void UpdateHeadPosition ()
	{
		line.UpdateHeadPosition(worldPosition);
	}

  void OnDrawGizmos() 
  {
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, 1f + treshold);
  }
}