using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour {

    public MovementMotor motor;
    public GameObject cursorPrefab;
    public GameObject joystickPrefab;

    public float cameraSmoothing = 0.01f;
    public float cameraPreview = 2.0f;

    public float cursorPlaneHeight = 0;
    public float cursorFacingCamera = 0;
    public float cursorSmallerWidthDistance = 0;
    public float cursorSmallerWhenClose = 1;

    private Camera mainCamera;

    private Transform cursorObject;
    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 cameraOffset = Vector3.zero;
    private Vector3 initOffsetToPlayer;

    private Vector3 cursorScreenPosition;
    private Plane playerMovementPlane;

    private Quaternion screenMovementSpace;
    private Vector3 screenMovementForward;
    private Vector3 screenMovementRight;

    private void Awake()
    {
        motor.movementDirection = Vector2.zero;
        motor.facingDirection = Vector2.zero;

        mainCamera = Camera.main;

        initOffsetToPlayer = mainCamera.transform.position - transform.position;

        cameraOffset = mainCamera.transform.position - transform.position;

        cursorScreenPosition = new Vector3(0.5f * Screen.width, 0.5f * Screen.height, 0);

        playerMovementPlane = new Plane(transform.up, transform.position + transform.up * cursorPlaneHeight);
    }

    // Use this for initialization
    void Start ()
    {
        screenMovementSpace = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;
	}
	
	// Update is called once per frame
	void Update ()
    {
        motor.movementDirection = Input.GetAxis("Horizontal") * screenMovementRight + Input.GetAxis("Vertical") * screenMovementForward;

        if (motor.movementDirection.sqrMagnitude > 1)
            motor.movementDirection.Normalize();

        playerMovementPlane.normal = transform.up;
        playerMovementPlane.distance = -transform.position.y + cursorPlaneHeight;

        Vector3 cameraAdjustmentVector = Vector3.zero;

        //check if controller connected
        //if not
        Vector3 cursorScreenPosition = Input.mousePosition;

       // Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane(cursorScreenPosition, playerMovementPlane, mainCamera);

        float halfWidth = Screen.width / 2.0f;
        float halfHeigth = Screen.height / 2.0f;

    }
}
