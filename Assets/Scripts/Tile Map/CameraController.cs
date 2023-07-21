using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public float cameraPositionChangeSpeed;
    public float cameraHorizontalRotationSpeed;
    public float cameraFieldOfViewSpeed;
    public float cameraAngleChangeSpeed;
    public int edgeSlideDistance;
    public float fieldOfViewDefault;
    public float fieldOfViewMax;
    public float fieldOfViewMin;

    [SerializeField]
    private float xMaxBorder;
    [SerializeField]
    private float xMinBorder;
    [SerializeField]
    private float yMaxBorder;
    [SerializeField]
    private float yMinBorder;

    private Vector3 cameraPosition;
    private float fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = transform.position;
        fieldOfView = fieldOfViewDefault;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraPositionChange();
        HandleCameraRotationHorizontal();
        HandleCameraAngleChange();
        //HandleMouseEdgeSlide();
        HandleCameraFieldOfViewChange();
    }

    private void HandleCameraPositionChange() {
        if (Input.GetKey(KeyCode.W)) cameraPosition.z += cameraPositionChangeSpeed;
        if (Input.GetKey(KeyCode.S)) cameraPosition.z -= cameraPositionChangeSpeed;
        if (Input.GetKey(KeyCode.D)) cameraPosition.x += cameraPositionChangeSpeed;
        if (Input.GetKey(KeyCode.A)) cameraPosition.x -= cameraPositionChangeSpeed;

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, xMinBorder, xMaxBorder);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, yMinBorder, yMaxBorder);
        transform.position = cameraPosition;
    }

    private void HandleCameraRotationHorizontal() {
        float rotateDirection = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDirection += 1f;
        if (Input.GetKey(KeyCode.E)) rotateDirection -= 1f;

        transform.eulerAngles += new Vector3(0, rotateDirection * cameraHorizontalRotationSpeed, 0);
    }

    private void HandleCameraAngleChange() {
        float rotateDirection = 0f;
        if (Input.GetKey(KeyCode.Z)) rotateDirection += 1f;
        if (Input.GetKey(KeyCode.X)) rotateDirection -= 1f;

        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y += rotateDirection * cameraAngleChangeSpeed;
        //cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y, cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y + rotateDirection, cameraZoomSpeed);
    }

    // private void HandleMouseEdgeSlide() {
    //     Vector3 inputDirection = new Vector3(0, 0, 0);
    //     if (Input.mousePosition.x < edgeSlideDistance) inputDirection.x -= 1f;
    //     if (Input.mousePosition.y < edgeSlideDistance) inputDirection.z -= 1f;
    //     if (Input.mousePosition.x > Screen.width - edgeSlideDistance) inputDirection.x += 1f;
    //     if (Input.mousePosition.y > Screen.height - edgeSlideDistance) inputDirection.z += 1f;

    //     Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
    //     transform.position += moveDirection * cameraMovementSpeed * Time.deltaTime;
    // }

    private void HandleCameraFieldOfViewChange() {
        if (Input.mouseScrollDelta.y > 0) fieldOfView -= 5;
        if (Input.mouseScrollDelta.y < 0) fieldOfView += 5;
        
        fieldOfView = Mathf.Clamp(fieldOfView, fieldOfViewMin, fieldOfViewMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, fieldOfView, cameraFieldOfViewSpeed * Time.deltaTime);
    }

    public void SetBorder(float xMax, float xMin, float yMax, float yMin) {
        xMaxBorder = xMax;
        xMinBorder = xMin;
        yMaxBorder = yMax;
        yMinBorder = yMin;
    }
}
