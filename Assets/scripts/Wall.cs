using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// A Wall class, from Item
//

public class Wall : Item
{
	// Use this for initialization
	void Start ()
	{
		initItem();
	}
	
	// Update is called once per frame
	override public void update ()
	{
		
	}

	void initItem()
	{ 
		itemName = "mur";
		canMove = false;
	}
}
