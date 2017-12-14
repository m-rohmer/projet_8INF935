using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// A minor class that launchs the GameManager
//

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {

        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
