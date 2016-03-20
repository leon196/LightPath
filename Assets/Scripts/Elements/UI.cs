using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	ChromaDetector chromaDetector;
	Camera cam;
	public Color color1;
	public float color1Treshold;
	public Color color2;
	public float color2Treshold;
	public Color color3;
	public float color3Treshold;
	public LineRenderer line1;
	public LineRenderer line2;
	public LineRenderer line3;
	Vector2 lastPos1;
	Vector2 lastPos2;
	Vector2 lastPos3;
	float dist;
	int vertexCount1;
	int vertexCount2;
	int vertexCount3;
	Vector3[] positionArray1;
	Vector3[] positionArray2;
	Vector3[] positionArray3;
	const int maxVertex = 50;

	// Use this for initialization
	void Start () {
		chromaDetector = GameObject.FindObjectOfType<ChromaDetector>();	
		cam = GetComponent<Camera>();
		line1.SetVertexCount(1);
		line2.SetVertexCount(1);
		line3.SetVertexCount(1);
		Vector3 pos = cam.ViewportToWorldPoint(Vector2.one * 0.5f);
		pos.z = 0.5f;
		line1.SetPosition(0, pos);
		line2.SetPosition(0, pos);
		line3.SetPosition(0, pos);

		lastPos1 = Vector2.one * 0.5f;
		lastPos2 = Vector2.one * 0.5f;
		lastPos3 = Vector2.one * 0.5f;
		vertexCount1 = 1;
		vertexCount2 = 1;
		vertexCount3 = 1;
		positionArray1 = new Vector3[maxVertex];
		positionArray1[0] = pos;
		positionArray2 = new Vector3[maxVertex];
		positionArray2[0] = pos;
		positionArray3 = new Vector3[maxVertex];
		positionArray3[0] = pos;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = cam.ViewportToWorldPoint(chromaDetector.targetPosition1);
		pos.z = 0.5f;

		Shader.SetGlobalColor("_Color1", color1);
		Shader.SetGlobalFloat("_Color1Treshold", color1Treshold);
		Shader.SetGlobalColor("_Color2", color2);
		Shader.SetGlobalFloat("_Color2Treshold", color2Treshold);
		Shader.SetGlobalColor("_Color3", color3);
		Shader.SetGlobalFloat("_Color3Treshold", color3Treshold);

		if (Vector2.Distance(lastPos1, chromaDetector.targetPosition1) > 0.01f) {
			lastPos1 = chromaDetector.targetPosition1;
			if (vertexCount1 < maxVertex) {
				vertexCount1 += 1;
				line1.SetVertexCount(vertexCount1);
			} else {
				for (int i = 0; i < vertexCount1 - 1; ++i) {
					positionArray1[i] = positionArray1[i + 1];
					line1.SetPosition(i, positionArray1[i]);
				}
			}
		}
		positionArray1[vertexCount1 - 1] = pos;
		line1.SetPosition(vertexCount1 - 1, pos);


		pos = cam.ViewportToWorldPoint(chromaDetector.targetPosition2);
		pos.z = 0.5f;
		if (Vector2.Distance(lastPos2, chromaDetector.targetPosition2) > 0.01f) {
			lastPos2 = chromaDetector.targetPosition2;
			if (vertexCount2 < maxVertex) {
				vertexCount2 += 1;
				line2.SetVertexCount(vertexCount2);
				positionArray2[vertexCount2 - 1] = pos;
			} else {
				for (int i = 0; i < vertexCount2 - 1; ++i) {
					positionArray2[i] = positionArray2[i + 1];
					line2.SetPosition(i, positionArray2[i]);
				}
				positionArray2[vertexCount2 - 1] = pos;
			}
		}
		line2.SetPosition(vertexCount2 - 1, positionArray2[vertexCount2 - 1]);


		pos = cam.ViewportToWorldPoint(chromaDetector.targetPosition3);
		pos.z = 0.5f;
		if (Vector2.Distance(lastPos3, chromaDetector.targetPosition3) > 0.01f) {
			lastPos3 = chromaDetector.targetPosition3;
			if (vertexCount3 < maxVertex) {
				vertexCount3 += 1;
				line3.SetVertexCount(vertexCount3);
				positionArray3[vertexCount3 - 1] = pos;
			} else {
				for (int i = 0; i < vertexCount3 - 1; ++i) {
					positionArray3[i] = positionArray3[i + 1];
					line3.SetPosition(i, positionArray3[i]);
				}
				positionArray3[vertexCount3 - 1] = pos;
			}
		}

		line3.SetPosition(vertexCount3 - 1, positionArray3[vertexCount3 - 1]);
	}
}
