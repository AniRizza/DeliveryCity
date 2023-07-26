using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]
    private float cameraPositionChangeSpeed;
    [SerializeField]
    private float cameraHorizontalRotationSpeed;
    [SerializeField]
    private int edgeSlideDistance;
    [SerializeField]
    private float xMaxBorder;
    [SerializeField]
    private float xMinBorder;
    [SerializeField]
    private float yMaxBorder;
    [SerializeField]
    private float yMinBorder;
    [SerializeField]
    private float offsetYStep;
    [SerializeField]
    private float offsetZStep;
    [SerializeField]
    private float offsetYMax;
    [SerializeField]
    private float offsetYMin;
    [SerializeField]
    private float offsetZMax;
    [SerializeField]
    private float offsetZMin;


    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    void Update()
    {
        HandleCameraPositionChange();
        HandleCameraRotationHorizontal();
        HandleMouseEdgeSlide();
        HandleCameraOffsetScroll();
    }

    private void HandleCameraPositionChange() {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        // if (Input.GetKey(KeyCode.W)) inputDirection.z += 1f;
        // if (Input.GetKey(KeyCode.S)) inputDirection.z -= 1f;
        // if (Input.GetKey(KeyCode.D)) inputDirection.x += 1f;
        // if (Input.GetKey(KeyCode.A)) inputDirection.x -= 1f;
        inputDirection.z = Input.GetAxis("Vertical");
        inputDirection.x = Input.GetAxis("Horizontal");

        float inputMagnitude = inputDirection.magnitude;
        if (inputMagnitude > 1f) inputDirection /= inputMagnitude;

        Vector3 cameraPosition = transform.position;
        cameraPosition += (transform.forward * inputDirection.z + transform.right * inputDirection.x) * cameraPositionChangeSpeed * Time.deltaTime;

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

    private void HandleCameraOffsetScroll() {
        if (Input.mouseScrollDelta.y > 0) {
            cameraOffset.y -= offsetYStep;
            cameraOffset.z += offsetZStep;
        }
        if (Input.mouseScrollDelta.y < 0) {
            cameraOffset.y += offsetYStep;
            cameraOffset.z -= offsetZStep;
        }
        cameraOffset.y = Mathf.Clamp(cameraOffset.y, offsetYMin, offsetYMax);
        cameraOffset.z = Mathf.Clamp(cameraOffset.z, offsetZMin, offsetZMax);
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = Mathf.Lerp(
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y, cameraOffset.y, Time.deltaTime);
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = Mathf.Lerp(
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z, cameraOffset.z, Time.deltaTime);
    }

    private void HandleMouseEdgeSlide() {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        if (Input.mousePosition.x < edgeSlideDistance) inputDirection.x -= 1f;
        if (Input.mousePosition.y < edgeSlideDistance) inputDirection.z -= 1f;
        if (Input.mousePosition.x > Screen.width - edgeSlideDistance) inputDirection.x += 1f;
        if (Input.mousePosition.y > Screen.height - edgeSlideDistance) inputDirection.z += 1f;

        float inputMagnitude = inputDirection.magnitude;
        if (inputMagnitude > 1f) inputDirection /= inputMagnitude;

        Vector3 cameraPosition = transform.position;
        cameraPosition += (transform.forward * inputDirection.z + transform.right * inputDirection.x) * cameraPositionChangeSpeed * Time.deltaTime;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, xMinBorder, xMaxBorder);
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, yMinBorder, yMaxBorder);
        transform.position = cameraPosition;
    }

    public void SetBorder(float xMax, float xMin, float yMax, float yMin) {
        xMaxBorder = xMax;
        xMinBorder = xMin;
        yMaxBorder = yMax;
        yMinBorder = yMin;
    }
}
