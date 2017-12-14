using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float gravity;
	public string state ="";

	private List<Item> itemList;
	//this list will contain all the item in our game
	//private List<Force> forcesList;
	//this list will contain all the constant forces
	//garder les forces constantes dans le GM est peut-être pas si utile en vrai ^^

	private int i = 0;

	//public int maxID = 0;
	public static GameManager instance = null;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		itemList = new List<Item> ();
		//forcesList = new List<Force> ();

		//we initialize the list of item, collecting all the item in the game
		GameObject[] gameObjectsItem =
			GameObject.FindGameObjectsWithTag ("Item");

		foreach (GameObject go in gameObjectsItem)
			itemList.Add ((Item)go.GetComponent<Item> ());
        

	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (Item t in itemList.ToArray()) {
			if (t.itemName == "ground")
				t.itemList = itemList;
			

			if (t.name == "Boule Blanche") {
				i = 0;
				t.itemList = itemList;
				foreach (Item it in t.itemList.ToArray()) {
					i++;
					if (it.actualCelerity.magnitude==0) {
						i--;
					}
					state = i.ToString ();
				}
				if (i == 0){
					t.forcesList.Add (new Force (2000, new Vector3 (1, 0, -1), false,"Push"));
				}
			}


			t.update ();
			if (t.itemName == "boule") {
				if (t.actualCelerity.magnitude != 0) {
					CollisionDetection ((Ball)t);
				}
			}
		}
	}
	
	


	// Vide pour l'instant, gérera la détection de collision
	public void CollisionDetection (Ball ball)
	{
		//on regarde si sa position future n'entre pas en collision avec chacun des autres item de notre liste.
		//si oui, on calcule la force résultant de cette collision et l'ajoute au tableau de forces de notre Item,
		//ainsi qu'une force de même intensité mais opposée à l'objet avec lequel on collide.

		foreach (Item t in itemList.ToArray()) {
			if (t.itemName == "boule" && !t.Equals (ball)) {
				
				Ball otherBall = (Ball)t;
				Vector3 diffSpeed = (otherBall.actualCelerity - ball.actualCelerity);

				float a = Vector3.Distance (otherBall.actualCelerity, ball.actualCelerity);
				if (a * a > 0.001) {
					//distance entre les deux boules
					//vecteur normal si utilisé
					Vector3 dist = (otherBall.actualPosition - ball.actualPosition);
					//si collision, ajout d'une force aux deux boules
					if ((dist + diffSpeed * Time.deltaTime).magnitude < (ball.diameter / 2) + (otherBall.diameter / 2)) {
						Vector3 relativCelerity = -diffSpeed;
						float k1 = (ball.restitutionCoef + 1) * Vector3.Dot (relativCelerity, dist.normalized) / ((1 / ball.weight + 1 / otherBall.weight)) * Vector3.Dot (dist.normalized, dist.normalized);
						float k2 = (otherBall.restitutionCoef + 1) * Vector3.Dot (relativCelerity, dist.normalized) / ((1 / ball.weight + 1 / otherBall.weight)) * Vector3.Dot (dist.normalized, dist.normalized);
						ball.newCelerity -= k1 / ball.weight * dist.normalized;
						otherBall.newCelerity += k2 / otherBall.weight * dist.normalized;
						ball.actualCelerity = ball.newCelerity;
						otherBall.actualCelerity = otherBall.newCelerity;
					
						ball.forcesList.Add (new Force (ball.restitutionCoef * ball.newCelerity.magnitude, dist.normalized, false,"collision"));
						otherBall.forcesList.Add (new Force (otherBall.restitutionCoef * otherBall.newCelerity.magnitude, -1 * dist.normalized, false,"collision"));
					}
				}
			} else {
				if (t.itemName == "mur") {
					
					Wall wall = (Wall)t;
					Vector3 nextPos = (ball.actualPosition + ball.actualCelerity * Time.deltaTime);
					float rotat = t.transform.rotation.eulerAngles.y * Mathf.PI / 180;
					float cosi = Mathf.Cos (rotat);
					float sinu = Mathf.Sin (rotat);
					Vector3 scaleWall = t.transform.localScale / 2;
					float max1 = Mathf.Max (cosi * scaleWall.x - sinu * scaleWall.z, sinu * scaleWall.z - cosi * scaleWall.x);
					float min1 = Mathf.Min (cosi * scaleWall.x - sinu * scaleWall.z, sinu * scaleWall.z - cosi * scaleWall.x);
					float max2 = Mathf.Max (-sinu * scaleWall.x + cosi * scaleWall.z, sinu * scaleWall.x - cosi * scaleWall.z);
					float min2 = Mathf.Min (-sinu * scaleWall.x + cosi * scaleWall.z, sinu * scaleWall.x - cosi * scaleWall.z);
					if (((nextPos.x + ball.diameter / 2 - wall.transform.position.x < max1) &&
					    (nextPos.x + ball.diameter / 2 - wall.transform.position.x > min1) &&
					    (nextPos.z + ball.diameter / 2 - wall.transform.position.z < max2) &&
					    (nextPos.z + ball.diameter / 2 - wall.transform.position.z > min2)) ||
					    ((nextPos.x - ball.diameter / 2 - wall.transform.position.x < max1) &&
					    (nextPos.x - ball.diameter / 2 - wall.transform.position.x > min1) &&
					    (nextPos.z + ball.diameter / 2 - wall.transform.position.z < max2) &&
					    (nextPos.z + ball.diameter / 2 - wall.transform.position.z > min2)) ||
					    ((nextPos.x + ball.diameter / 2 - wall.transform.position.x < max1) &&
					    (nextPos.x + ball.diameter / 2 - wall.transform.position.x > min1) &&
					    (nextPos.z - ball.diameter / 2 - wall.transform.position.z < max2) &&
					    (nextPos.z - ball.diameter / 2 - wall.transform.position.z > min2)) ||
					    ((nextPos.x - ball.diameter / 2 - wall.transform.position.x < max1) &&
					    (nextPos.x - ball.diameter / 2 - wall.transform.position.x > min1) &&
					    (nextPos.z - ball.diameter / 2 - wall.transform.position.z < max2) &&
					    (nextPos.z - ball.diameter / 2 - wall.transform.position.z > min2))) {
						
						if (!ball.lastCollision.Equals (wall.name.ToString ())) {
							ball.lastCollision = wall.name.ToString ();
							float k1 = (ball.restitutionCoef + 1) * Vector3.Dot (ball.actualCelerity, new Vector3 (sinu, 0, cosi)) / (1 / ball.weight) * Vector3.Dot (new Vector3 (sinu, 0, cosi), new Vector3 (sinu, 0, cosi));
							ball.newCelerity -= k1 / ball.weight * new Vector3 (sinu, 0, cosi);
							ball.actualCelerity = ball.newCelerity;
							ball.forcesList.Add (new Force (ball.restitutionCoef * ball.newCelerity.magnitude, new Vector3 (sinu, 0, cosi), false,"collision"));

						}
					} else {
						if (ball.lastCollision.Equals (wall.name.ToString ())) {
							ball.lastCollision = "";
						} 
					}

				}

			}
		}
	}
	 
}
