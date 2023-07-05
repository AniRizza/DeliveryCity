using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public float cameraMovementSpeed;
    public float cameraHorizontalRotationSpeed;
    public float cameraVerticalalRotationSpeed;
    public float cameraZoomSpeed;
    public int edgeSlideDistance;
    public float fieldOfViewMax;
    public float fieldOfViewMin;

    private float fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = fieldOfViewMax/2;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotationHorizontal();
        HandleCameraRotationVertical();
        //HandleMouseEdgeSlide();
        HandleCameraZoom();
    }

    private void HandleCameraMovement() {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) inputDirection.z += 1f;
        if (Input.GetKey(KeyCode.S)) inputDirection.z -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDirection.x += 1f;
        if (Input.GetKey(KeyCode.A)) inputDirection.x -= 1f;

        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraMovementSpeed * Time.deltaTime;
    }

    private void HandleCameraRotationHorizontal() {
        float rotateDirection = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDirection += 1f;
        if (Input.GetKey(KeyCode.E)) rotateDirection -= 1f;

        transform.eulerAngles += new Vector3(0, rotateDirection * cameraHorizontalRotationSpeed, 0);
    }

    private void HandleCameraRotationVertical() {
        float rotateDirection = 0f;
        if (Input.GetKey(KeyCode.Z)) rotateDirection += 1f;
        if (Input.GetKey(KeyCode.X)) rotateDirection -= 1f;

        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset += new Vector3(0, rotateDirection * cameraVerticalalRotationSpeed, 0);

    }

    private void HandleMouseEdgeSlide() {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        if (Input.mousePosition.x < edgeSlideDistance) inputDirection.x -= 1f;
        if (Input.mousePosition.y < edgeSlideDistance) inputDirection.z -= 1f;
        if (Input.mousePosition.x > Screen.width - edgeSlideDistance) inputDirection.x += 1f;
        if (Input.mousePosition.y > Screen.height - edgeSlideDistance) inputDirection.z += 1f;

        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraMovementSpeed * Time.deltaTime;
    }

    private void HandleCameraZoom() {
        if (Input.mouseScrollDelta.y > 0) fieldOfView -= cameraZoomSpeed;
        if (Input.mouseScrollDelta.y < 0) fieldOfView += cameraZoomSpeed;
        
        fieldOfView = Mathf.Clamp(fieldOfView, fieldOfViewMin, fieldOfViewMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, fieldOfView, Time.deltaTime * cameraZoomSpeed);
    }
}
