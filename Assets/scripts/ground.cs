using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// The Ground class, an Item
//

public class ground : Item {
    private Vector3 n;

    // Use this for initialization
    public void Start()
    {
        canMove = false;
        itemName = "ground";
    }

    // Update is called once per frame
    override public void update() {

     
	}
}
