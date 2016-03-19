using UnityEngine;
using System.Collections;

public class SoundGenerator : MonoBehaviour 
{
	private float startFrequency = 0f;
	private SfxrSynth synth;

	void Start () 
	{
		synth = new SfxrSynth();
		synth.parameters.SetSettingsString("0,,0.032,0.4138,0.4365," + startFrequency + ",,,,,,0.3117,0.6925,,,,,,1,,,,,0.5");
		synth.Play();
	}
	
	void Update () 
	{
		if (Input.GetKey(KeyCode.LeftArrow)) {
			startFrequency = Mathf.Clamp(startFrequency - Time.deltaTime, 0f, 1f);
			synth.parameters.SetSettingsString("0,,0.032,0.4138,0.4365," + startFrequency + ",,,,,,0.3117,0.6925,,,,,,1,,,,,0.5");
			synth.Play();
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			startFrequency = Mathf.Clamp(startFrequency + Time.deltaTime, 0f, 1f);
			synth.parameters.SetSettingsString("0,,0.032,0.4138,0.4365," + startFrequency + ",,,,,,0.3117,0.6925,,,,,,1,,,,,0.5");
			synth.Play();
		}
	}
}
