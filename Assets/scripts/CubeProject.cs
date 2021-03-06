using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// The CubeProject class, an Item
// Represents the "big" cube, made with all the others little cubes
//

public class CubeProject : Item {
	public FaceRotation face1;
	public FaceRotation face2;
	public FaceRotation face3;
	public string state ="";
	public FaceRotation[] cube;
	public float angleX;
	public float angleY;
	public float angleZ;
	private float[,] matrixAngleX;
	private float[,] matrixAngleY;
	private float[,] matrixAngleZ;
	public float x;
	public float y;
	public float z;
	public Vector3 center;


	// Use this for initialization
	void Start () {
		
		center = new Vector3 (x, y, z);
		face1.position = center - new Vector3 (0, 0, 1);
		face1.updatePosition ();
		face2.position = center;
		face2.updatePosition ();
		face3.position = center + new Vector3 (0, 0, 1	);
		face3.updatePosition ();

		cube = new FaceRotation	[3];
		cube [0] = face1;
		cube [1] = face2;
		cube [2] = face3;

		matrixAngleX = new float[3,3];
		matrixAngleY = new float[3,3];
		matrixAngleZ = new float[3,3];

		float cosiX = Mathf.Cos (angleX * Mathf.PI / (180)*Time.deltaTime);
		float sinuX = Mathf.Sin (angleX * Mathf.PI / (180)*Time.deltaTime);

		float cosiY = Mathf.Cos (angleY * Mathf.PI / (180)*Time.deltaTime);
		float sinuY = Mathf.Sin (angleY * Mathf.PI / (180)*Time.deltaTime);

		float cosiZ = Mathf.Cos (angleZ * Mathf.PI / (180)*Time.deltaTime);
		float sinuZ = Mathf.Sin (angleZ * Mathf.PI / (180)*Time.deltaTime);

		matrixAngleX [0, 0] = 1;
		matrixAngleX [0, 1] = 0;
		matrixAngleX [0, 2] = 0;
		matrixAngleX [1, 0] = 0;
		matrixAngleX [1, 1] = cosiX;
		matrixAngleX [1, 2] = sinuX;
		matrixAngleX [2, 0] = 0;
		matrixAngleX [2, 1] = -sinuX;
		matrixAngleX [2, 2] = cosiX;

		matrixAngleY [0, 0] = cosiY;
		matrixAngleY [0, 1] = 0;
		matrixAngleY [0, 2] = -sinuY;
		matrixAngleY [1, 0] = 0;
		matrixAngleY [1, 1] = 1;
		matrixAngleY [1, 2] = 0;
		matrixAngleY [2, 0] = sinuY;
		matrixAngleY [2, 1] = 0;
		matrixAngleY [2, 2] = cosiY;

		matrixAngleZ [0, 0] = cosiZ;
		matrixAngleZ [0, 1] = -sinuZ;
		matrixAngleZ [0, 2] = 0;
		matrixAngleZ [1, 0] = sinuZ;
		matrixAngleZ [1, 1] = cosiZ;
		matrixAngleZ [1, 2] = 0;
		matrixAngleZ [2, 0] = 0;
		matrixAngleZ [2, 1] = 0;
		matrixAngleZ [2, 2] = 1;
	}

	// Update is called once per frame
	override public void update () {
		state = "true";
		foreach (FaceRotation f in cube) {
			state = "true";
			foreach(Item c in f.face) {
				Vector3 newPos = multiplicationMatrix (matrixAngleX, c.transform.position);
				newPos = multiplicationMatrix (matrixAngleY, newPos);
				newPos = multiplicationMatrix (matrixAngleZ, newPos);
				c.transform.position = newPos;
				c.transform.Rotate( new Vector3(-angleX,-angleY,angleZ)*Time.deltaTime);
			}
		}
	}

	public Vector3 multiplicationMatrix (float[,] angle, Vector3 position){
		Vector3 localPos = position - center;
		Vector3 newPosi = new Vector3 (angle [0, 0] * localPos.x + angle [0, 1] * localPos.y + angle [0, 2] * localPos.z,
			                 angle [1, 0] * localPos.x + angle [1, 1] * localPos.y + angle [1, 2] * localPos.z,
			                 angle [2, 0] * localPos.x + angle [2, 1] * localPos.y + angle [2, 2] * localPos.z);
		return newPosi + center;
	}
}
