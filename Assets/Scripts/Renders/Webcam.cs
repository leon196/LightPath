using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour 
{
	public string webcamName = "";
	public bool mirrorX = false;
	public bool mirrorY = false;
	WebCamTexture texture;
	int currentWebcam;

	void Start () 
	{
		if (WebCamTexture.devices.Length > 0) {

			foreach (WebCamDevice device in WebCamTexture.devices) {
				webcamName = webcamName + device.name + '\n';
			}

			// Setup webcam texture
			texture = new WebCamTexture();
			Shader.SetGlobalTexture("_WebcamTexture", texture);
			texture.Play();

			currentWebcam = 0;

			Shader.SetGlobalFloat("_MirrorX", mirrorX ? 1f: 0f);
			Shader.SetGlobalFloat("_MirrorY", mirrorY ? 1f: 0f);
		}
	}

	void Update ()
	{
		// Switch camera
		if (Input.GetKeyDown(KeyCode.C)) {
			if (WebCamTexture.devices.Length > 1) {
				currentWebcam = (currentWebcam + 1) % WebCamTexture.devices.Length;
				texture.Stop();
				texture = new WebCamTexture(WebCamTexture.devices[currentWebcam].name);
				Shader.SetGlobalTexture("_WebcamTexture", texture);
				texture.Play();
			}
		}

		// Mirror X
		if (Input.GetKeyDown(KeyCode.X))  {
			SetMirrorX(!mirrorX);
		// Mirror Y
		} else if (Input.GetKeyDown(KeyCode.Y)) {
			SetMirrorY(!mirrorY);
		}
	}

	public void SetMirrorX (bool value)
	{
		mirrorX = value;
		Shader.SetGlobalFloat("_MirrorX", mirrorX ? 1f: 0f);
	}

	public void SetMirrorY (bool value)
	{
		mirrorY = value;
		Shader.SetGlobalFloat("_MirrorY", mirrorY ? 1f: 0f);
	}
}