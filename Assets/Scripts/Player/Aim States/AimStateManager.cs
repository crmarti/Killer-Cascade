using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    // Aiming state machine
    public AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();

    // Movement
    private float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSensitivity = 1f;

    // Animations & Camera
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public CinemachineVirtualCamera vCam;

    // Aim zoom
    public float adsFov = 40f;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10f;

    Transform aimPos;
    [HideInInspector] public Vector3 actualAimPosition;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

    // Camera swapping/crouching
    float xFollowPosition;
    float yFollowPosition, ogYPosition;
    [SerializeField]
    float crouchCamHeight = 0.9f;
    [SerializeField]
    float swapSpeed;
    MovementStateManager move;

    private void Start()
    {
        move = GetComponent<MovementStateManager>();
        xFollowPosition = camFollowPos.localPosition.x;
        ogYPosition = camFollowPos.localPosition.y;
        yFollowPosition = ogYPosition;
        aimPos = GameObject.FindGameObjectWithTag("AimPos").transform;

        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
        SwitchState(Hip);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gamePaused)
        {
            xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
            yAxis = Mathf.Clamp(yAxis, -90, 90);

            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
                actualAimPosition = hit.point;
            }

            MoveCamera();

            currentState.UpdateState(this);
        }
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            xFollowPosition = -xFollowPosition;

        if (move.currentState == move.Crouch)
            yFollowPosition = crouchCamHeight;
        else yFollowPosition = ogYPosition;

        Vector3 newFollowPos = new Vector3(xFollowPosition, yFollowPosition, camFollowPos.localPosition.z);
        camFollowPos.localPosition = Vector3.Lerp(camFollowPos.localPosition, newFollowPos, swapSpeed * Time.deltaTime);
    }
}
