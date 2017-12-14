using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// The Force class
// Defines the ways to create a force
// A force is defined by:
// an intensity (magnitude), 2 vectors (one normalized for the direction) and a name
//

public  class Force {

    public float intensity;
	public float gravity = 9.81f;
	public bool kinetic = false;
    public Vector3 direction;
    public Vector3 force;
    public bool constant;
	public string name = "";

	public Force(float i, Vector3 d, bool b, string n)
    {
        intensity = i;
        direction = d;
        constant = b;
		name = n;

        force = intensity * direction;
    }

	public Force(Vector3 f, bool b, string n)
    {
        force = f;
        constant = b;
		name = n;
    }

	public Force(string n, float weight){
		switch (n) {
		    case "Gravity":
			    force = new Vector3 (0, -gravity* weight , 0);
			    name = n;
			    constant = true;
			    break;

		    case "KineticFriction":
			    kinetic = true;
			    name = n;
			    constant=true;
			    break;
		}
	}

    public Vector3 getForce()
    {
        return force;
    }

    public bool isConstant()
    {
        return constant;
    }
}
