using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : Scannable {

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject disable;

    public bool active = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(active)
            {
                GameObject clone = Instantiate(explosion, transform.position + (Vector3.up * 2f), Quaternion.identity) as GameObject;

                active = false;

                gameObject.SetActive(false);
            }
        }
    }

    public override void Ping()
    {
        //gameObject.SetActive(false);

        //active = false;

        //GameObject clone = Instantiate(disable, transform.position, Quaternion.identity) as GameObject;

        base.Ping();

        if(active && Camera.main.gameObject.GetComponent<Scanner>().destroyer)
            StartCoroutine(Hijack());
    }

    IEnumerator Hijack()
    {
        active = false;

        GameObject clone = Instantiate(disable, transform.position, Quaternion.identity) as GameObject;

        gameObject.SetActive(false);

        yield return null;
    }
}
