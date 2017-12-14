using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// The Ball class, from MovingItem
// Is defined by a diameter, coefficient of restitution and friction...
//

public class Ball : MovingItem {

    // Use this for initialization
    public void Start()
    {
        initItem();
		newPosition = centerOfMass;
		newCelerity = new Vector3(0, 0, 0);
		newAcceleration = new Vector3(0, 0, 0);
    }
       
    void initItem()
    {
        itemName = "boule";
        canMove = true;
        weight = 5f;
        diameter = 0.8f;
        restitutionCoef = 0.3f;
		frictionCoef = 0.95f;
		lastCollision = "";
        centerOfMass = transform.position;
        forcesList = new List<Force>();
    }

}
