using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour {

    [SerializeField]
    private Transform start, end;

    [SerializeField]
    private float speed;
    private bool run = false;

    private Vector3 destination;

    // Use this for initialization
    void Start ()
    {
        destination = start.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);

        if (Mathf.Pow(Vector3.SqrMagnitude(destination - transform.position), 2) < Mathf.Pow(0.5f, 2))
        {
            if (destination.Equals(start.position))
                destination = end.position;
            else
                destination = start.position;
        }

    }
}
