using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RenderFilter : Filter 
{
	void Awake () {
		material = new Material( Shader.Find("Hidden/Render") );
	}
}