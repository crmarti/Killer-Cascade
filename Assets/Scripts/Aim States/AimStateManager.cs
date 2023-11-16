using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    // Aiming state machine
    AimBaseState currentState;
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

    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

    private void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
        SwitchState(Hip);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -90, 90);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        currentState.UpdateState(this);
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
}
