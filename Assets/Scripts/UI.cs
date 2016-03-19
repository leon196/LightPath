using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	ChromaDetector chromaDetector;
	Camera cam;
	public Transform point;

	// Use this for initialization
	void Start () {
		chromaDetector = GameObject.FindObjectOfType<ChromaDetector>();	
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = cam.ViewportToWorldPoint(chromaDetector.targetPosition);
		pos.z = 0.5f;
		point.position = pos;
	}
}
