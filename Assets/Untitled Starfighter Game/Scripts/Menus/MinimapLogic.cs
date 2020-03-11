using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapLogic : MonoBehaviour
{
    //References the player.
    public Transform player;

    //Gets called after Update and FixedUpdate.
    void LateUpdate()
    {
      //Creates new position of the 'minimap' camera.
      Vector3 newPosition = player.position;
      //Keeps the new camera zoom position to the same set position.
      newPosition.y = transform.position.y;
      transform.position = newPosition;

      //Rotates the camera with the player.
      transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
