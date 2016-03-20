using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LightFilter : Filter 
{
	void Awake () {
		material = new Material( Shader.Find("Hidden/LightDetector") );
	}
}