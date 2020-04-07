using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapLogic : MonoBehaviour
{
    //References the player.
    public Transform player;
    public Camera topCamera;
    public float orthographicSize = 40;

    private void Start()
    {
        if (!player) player.Find("MainCharacter");
    }

    //Gets called after Update and FixedUpdate.
    void LateUpdate()
    {
        // Set camera orthographic size.
        topCamera.orthographicSize = orthographicSize;

        transform.position = player.position;

        //Rotates the camera with the player.
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
