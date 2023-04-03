using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetObj;
    [SerializeField] private Vector3 offset;
    [Range(1, 10)]
    private float distanceCamera = -3.3f;
    private float distanceCameraY = 2.5f;
    // 1-10
    private float smoothTime; //  = 0.25f
    private Vector3 velocity;

    private void FixedUpdate()
    {
        // transform.position = targetObj.GetChild(0).position ;
        FollowToObj();
    }

    private void FollowToObj()
    {
        #region Camera conntroll
        //Vector3 targetPosition = targetObj.position + offset ; // GetChild(0) - parent
        Vector3 targetPosition = new Vector3(targetObj.position.x, targetObj.position.y + distanceCameraY, offset.z + distanceCamera);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
        // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime * Time.fixedDeltaTime);
        transform.position = targetPosition;
        #endregion
    }

    // Limit Camera control
    private void LimitCameraControl()
    {

    }
}
