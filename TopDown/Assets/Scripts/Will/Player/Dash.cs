using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    private bool            dashing         = false;

   // public bool             canDash         = true;

    public int              maxDashCount    = 3;

    private static Vector3  destination;

    private Vector3         moveVector;

    private float           cooldown;

    [SerializeField]        
    private float           dashDistance    = 1f;

    [SerializeField]        
    private float           dashSpeed       = 10f;

    [SerializeField]        
    private int             dashCount       = 3;

    private Rigidbody rb;

    [SerializeField]
    private GameObject player, dash;

    private TrailRenderer trail;

    private SkinnedMeshRenderer[] dashRenderer, playerRenderer;

    private bool locked = false;

	// Use this for initialization
	void Start ()
    {
        trail = gameObject.GetComponentInChildren<TrailRenderer>();

        trail.enabled = false;

        if (!dash.activeSelf)
            dash.SetActive(true);

        rb = gameObject.GetComponent<Rigidbody>();

        dashRenderer = dash.GetComponentsInChildren<SkinnedMeshRenderer>();
        playerRenderer = player.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer render in dashRenderer)
        {
            render.enabled = false;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(!dashing)
            StartDash();

        Dashing();

        if(dashCount < maxDashCount)
        {
            if(cooldown <= Time.time)
            {
                dashCount++;
                cooldown = Time.time + 0.8f;
            }

            if(locked)
            {
                if (dashCount >= maxDashCount)
                    locked = false;
            }
        }

        Debug.Log(dashCount);

        if(dashing)
            Debug.Log("Destination is " + destination);
    }

    private void StartDash()
    {
        moveVector.Set(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && dashCount > 0 && !locked)
        {
            RaycastHit hit;

            dashing = true;

            if (Physics.Raycast(rb.position + (Vector3.up * 0.5f), moveVector, out hit, dashDistance, ~(1 << 8)))
            {
                destination = new Vector3(hit.point.x + (hit.normal.x * 0.55f), rb.position.y, hit.point.z + (hit.normal.z * 0.55f));
                //destination = hit.point;
            }
            else
            {
                destination = rb.position + moveVector.normalized * dashDistance;
            }

            dashCount--;
            cooldown = Time.time + 0.8f;

            if (dashCount <= 0)
            {
                locked = true;
                cooldown = Time.time + 0.5f;
            }
        }
    }

    private void Dashing()
    {
        if (dashing)
        {
            trail.enabled = true;
            //player.SetActive(false);
            foreach (SkinnedMeshRenderer render in playerRenderer)
            {
                render.enabled = false;
            }
            //smear.GetComponent<MeshRenderer>().enabled = true;
            foreach (SkinnedMeshRenderer render in dashRenderer)
            {
                render.enabled = true;
            }

            rb.isKinematic = true;

            rb.position = Vector3.Lerp(rb.position, destination, dashSpeed * Time.deltaTime);

            if (Mathf.Pow(Vector3.SqrMagnitude(destination - rb.position), 2) < Mathf.Pow(0.5f, 2))
            {
                //player.SetActive(true);
                foreach (SkinnedMeshRenderer render in playerRenderer)
                {
                    render.enabled = true;
                }
                //smear.GetComponent<MeshRenderer>().enabled = false;
                foreach (SkinnedMeshRenderer render in dashRenderer)
                {
                    render.enabled = false;
                }
                rb.isKinematic = false;
                trail.enabled = false;
                dashing = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(dashing)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(destination, 0.3f);
        }
    }
}
