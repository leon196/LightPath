using UnityEngine;
using System.Collections;

public class CameraToTexture : MonoBehaviour
{
	public string textureName = "_CameraTexture";
	RenderTexture buffer;
	
	void Start ()
	{
		buffer = new RenderTexture(Master.width, Master.height, 24, RenderTextureFormat.ARGB32);
		buffer.antiAliasing = 2;
		buffer.Create();
		GetComponent<Camera>().targetTexture = buffer;
		Shader.SetGlobalTexture(textureName, buffer);
	}
}