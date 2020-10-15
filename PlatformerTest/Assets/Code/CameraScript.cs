using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    [SerializeField] private Transform player;
    [SerializeField] float smoothSpeed = 0.5f;
    
    private void FixedUpdate()
    {

        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }

}
