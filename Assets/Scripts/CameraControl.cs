using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float DampTime = 0.2f;
    public float ScreenEdgeBuffer = 4f;
    private float MinSize = 6f;
    private float MaxSize = 8.4f;
    public List<Transform> Targets;


    private Camera Camera;
    private float ZoomSpeed;
    private Vector3 MoveVelocity;
    private Vector3 DesiredPosition;

    private Vector3 minPositionValues, maxPositionValues;
    [SerializeField] private Vector3 minBorderValues, maxBorderValues;

    private void Awake()
    {
        Camera = GetComponentInChildren<Camera>();
        SetStartPositionAndSize();
    }


    private void FixedUpdate()
    {
        SetMinAndMaxPositions();
        Move();

        Zoom();
    }


    private void Move()
    {
        FindAveragePosition();
        var target = new Vector3(Mathf.Clamp(DesiredPosition.x, minPositionValues.x, maxPositionValues.x),
            Mathf.Clamp(DesiredPosition.y, minPositionValues.y, maxPositionValues.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref MoveVelocity, DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < Targets.Count; i++)
        {
            if (!Targets[i].gameObject.activeSelf)
                continue;

            averagePos += Targets[i].position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;

        DesiredPosition = new Vector3(averagePos.x, averagePos.y, -10);
    }


    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, requiredSize, ref ZoomSpeed, DampTime);
    }


    private float FindRequiredSize()
    {
        //Vector3 desiredLocalPos = transform.InverseTransformPoint(DesiredPosition);
        Vector3 desiredLocalPos = DesiredPosition;

        float size = 0f;

        for (int i = 0; i < Targets.Count; i++)
        {
            if (!Targets[i].gameObject.activeSelf)
                continue;

            //Vector3 targetLocalPos = transform.InverseTransformPoint(Targets[i].position);
            Vector3 targetLocalPos = Targets[i].position;

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / Camera.aspect);
        }

        size += ScreenEdgeBuffer;

        size = Mathf.Clamp(Mathf.Max(size, MinSize), MinSize, MaxSize);
        //size = Mathf.Max(size, MinSize);
        return size;
    }

    private void SetMinAndMaxPositions()
    {
        var cameraHalfHeight = Camera.orthographicSize;
        var cameraHalfWidth = Camera.aspect * cameraHalfHeight;

        minPositionValues.x = minBorderValues.x + cameraHalfWidth;
        minPositionValues.y = minBorderValues.y + cameraHalfHeight;
        minPositionValues.z = -10;

        maxPositionValues.x = maxBorderValues.x - cameraHalfWidth;
        maxPositionValues.y = maxBorderValues.y - cameraHalfHeight;
        maxPositionValues.z = -10;
    }


    public void SetStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = DesiredPosition;

        Camera.orthographicSize = FindRequiredSize();
    }
}
