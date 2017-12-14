using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// the FacceRotation class, an Item
// represents a "face", a slice of the "big" cube made with 9 little cubes
//

public class FaceRotation : Item {
	public Item cube1;
	public Item cube2;
	public Item cube3;
	public Item cube4;
	public Item cube5;
	public Item cube6;
	public Item cube7;
	public Item cube8;
	public Item cube9;
	public Item[] face;
	public Vector3 position;

	// Use this for initialization
	void Start () {
		

	}

	public void updatePosition(){
		
		face = new Item[9];
		face [0] = cube1;
		face [1] = cube2;
		face [2] = cube3;
		face [3] = cube4;
		face [4] = cube5;
		face [5] = cube6;
		face [6] = cube7;
		face [7] = cube8;
		face [8] = cube9;
		cube1.transform.position = position + new Vector3 (1, 1, 0);
		cube2.transform.position = position + new Vector3 (1, 0, 0);
		cube3.transform.position = position + new Vector3 (1, -1, 0);
		cube4.transform.position = position + new Vector3 (0, 1, 0);
		cube5.transform.position = position + new Vector3 (0, 0, 0);
		cube6.transform.position = position + new Vector3 (0, -1, 0);
		cube7.transform.position = position + new Vector3 (-1, 1, 0);
		cube8.transform.position = position + new Vector3 (-1, 0, 0);
		cube9.transform.position = position + new Vector3 (-1, -1, 0);
	}
	
	// Update is called once per frame
	override public void update () {
		
	}
}
