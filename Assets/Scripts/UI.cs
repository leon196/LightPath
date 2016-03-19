using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	ChromaDetector chromaDetector;
	Camera cam;
	public LineRenderer line;
	public Transform point;
	Vector2 lastPos;
	float dist;
	int vertexCount;
	Vector3[] positionArray;
	const int maxVertex = 50;

	// Use this for initialization
	void Start () {
		chromaDetector = GameObject.FindObjectOfType<ChromaDetector>();	
		cam = GetComponent<Camera>();
		line.SetVertexCount(1);
		Vector3 pos = cam.ViewportToWorldPoint(Vector2.one * 0.5f);
		pos.z = 0.5f;
		line.SetPosition(0, pos);

		lastPos = Vector2.one * 0.5f;
		vertexCount = 1;
		positionArray = new Vector3[maxVertex];
		positionArray[0] = pos;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = cam.ViewportToWorldPoint(chromaDetector.targetPosition);
		pos.z = 0.5f;
		point.position = pos;

		if (Vector2.Distance(lastPos, chromaDetector.targetPosition) > 0.01f) {
			lastPos = chromaDetector.targetPosition;
			if (vertexCount < maxVertex) {
				vertexCount += 1;
				line.SetVertexCount(vertexCount);
				positionArray[vertexCount - 1] = pos;
			} else {
				for (int i = 0; i < vertexCount - 1; ++i) {
					positionArray[i] = positionArray[i + 1];
					line.SetPosition(i, positionArray[i]);
				}
				positionArray[vertexCount - 1] = pos;
			}
		}

		line.SetPosition(vertexCount - 1, positionArray[vertexCount - 1]);
	}
}
