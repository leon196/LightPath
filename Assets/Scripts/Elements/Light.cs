using UnityEngine;
using System.Collections;

public class Light : MonoBehaviour 
{
	public Color color;
	public float treshold;
	
	[HideInInspector] public int pixelCount;
	[HideInInspector] public float brightness;
	[HideInInspector] public Vector2 targetGlobal;
	[HideInInspector] public Vector3 worldPosition;
	[HideInInspector] public Color colorIntegerCode;
	[HideInInspector] public AudioSource audioSource;

	// Line line;

	void Start () 
	{
		// line = GetComponent<Line>();
		// line.SetColor(color);
		audioSource = GetComponent<AudioSource>();
	}

	public void UpdateHeadPosition ()
	{
		// line.UpdateHeadPosition(worldPosition);
	}

	public void UpdateVolume (float volume)
	{
		audioSource.volume = volume;
	}

  void OnDrawGizmos() 
  {
		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, 1f + treshold);
  }
}