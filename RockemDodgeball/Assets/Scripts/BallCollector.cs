using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    private BoxCollider pickupCollider;

    // Start is called before the first frame update
    void Awake()
    {
        pickupCollider = gameObject.GetComponent<BoxCollider>();
    }

    
}
