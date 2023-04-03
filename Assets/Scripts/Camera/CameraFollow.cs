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
    [SerializeField] private float smoothTime; //  = 0.25f
    private Vector3 velocity;

    [Header("Axis Limitation")]
    [SerializeField] private Vector3 minValuesCamera, maxValuesCamera;

    private void FixedUpdate()
    {
        
        FollowToObj();
    }

    private void FollowToObj()
    {
        #region Camera conntroll
        //Vector3 targetPosition = targetObj.position + offset ; // GetChild(0) - parent
        Vector3 targetPosition = new Vector3(targetObj.position.x, targetObj.position.y + distanceCameraY, offset.z + distanceCamera);

        // Limit by min and max values
        Vector3 boundPosition = new Vector3
                        (
                            (Mathf.Clamp(targetObj.position.x, minValuesCamera.x, maxValuesCamera.x)),
                            (Mathf.Clamp(targetObj.position.y, minValuesCamera.y, maxValuesCamera.y)),
                            transform.position.z
                        );

        // Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothTime * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime * Time.fixedDeltaTime);
        transform.position = targetPosition;
        #endregion
    }

  
}
