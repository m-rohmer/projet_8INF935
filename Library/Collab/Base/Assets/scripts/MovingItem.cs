using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : Item
{
	public string state = "";
	public string state5 = "";
	public string state6 = "";
	public float restitutionCoef;
	public float elasticityCoef;
	public float diametre;
	public string lastCollision;
	// Use this for initialization
	public Force gravity;
	public Force kinetic;

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	override public void update ()
	{
		timeLapse = Time.deltaTime;

		if (centerOfMass.y + actualCelerity.y * timeLapse <= diametre / 2 + 0.00001f) {
			if (state != "grounded") {
				if (actualCelerity.y < -0.4f) {
					
					Vector3 n = new Vector3 (0, 1, 0);
					float k = (restitutionCoef + 1) * Vector3.Dot (-1 * newCelerity, n) * weight;
					newCelerity = actualCelerity + k / weight * n;
					Force repulsion = new Force (restitutionCoef * newCelerity.magnitude, n, false, "Repulse");
					forcesList.Add (repulsion);
					forcesList.Remove (gravity);
					state = "grounded";


				} else {
					
					newCelerity.y = 0;	
					forcesList.Remove (gravity);
					kinetic = new Force ("KineticFriction", weight);
					forcesList.Add (kinetic); 
					state = "grounded";

				}
			}

		} else {
			switch (state) {
			case "grounded":
				forcesList.Remove (kinetic);
				gravity = new Force ("Gravity", weight);
				forcesList.Add (gravity); 
				state = "not grounded";
				break;
			case "":
				gravity = new Force ("Gravity", weight);
				forcesList.Add (gravity); 
				state = "not grounded";
				break;
				
			}
		}


			//on actualise ses position, vitesse et accélération
			actualPosition = newPosition;
			transform.position = actualPosition;
			actualCelerity = newCelerity;
			actualAcceleration = newAcceleration;
			centerOfMass = actualPosition;

			state5 = "";
			foreach (Force f in forcesList) {
				state5 += f.name;}
			//calcule des futures position, vitesse et accélération
			newAcceleration = new Vector3 (0, 0, 0);
			foreach (Force force in forcesList.ToArray()) {	
				if (force.kinetic) {
					newAcceleration += -frictionCoef * new Vector3 (actualCelerity.x, 0, actualCelerity.z);
				} else {

					newAcceleration += force.getForce ();
					if (!force.isConstant ())
						forcesList.Remove (force);
				}
			}

			Vector3 v = actualCelerity;
			//Si vitesse trop faible on arrête l'objet
			if ((((v.x) * (v.x) + (v.z) * (v.z)) < 0.3f) && (state == "grounded")) {
				state5 = "true";
				actualCelerity = new Vector3 (0, 0, 0);
			}

			newCelerity = actualCelerity + newAcceleration * timeLapse / weight;
			newPosition = actualPosition + newCelerity * timeLapse;

		}
	}




