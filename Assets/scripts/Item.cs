using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Abstract class for all kind of item
// Contains all the attributes an item can need to have
//
public abstract class Item : MonoBehaviour {

    public static GameManager gm = GameManager.instance;

    public string itemName = "";
    public int id;
    public bool canMove;
	public float weight;
	public float frictionCoef;

	public Vector3 centerOfMass;

    public Vector3 actualPosition;
    public Vector3 newPosition;

    public Vector3 actualCelerity;
    public Vector3 newCelerity;

    public Vector3 actualAcceleration;
    public Vector3 newAcceleration;

    public List<Force> forcesList;
    public List<Item> itemList;

    public float timeLapse;

    public abstract void update();
}
