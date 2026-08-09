using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
        public Transform player;
    public float fixedY = 2f; // Camera height

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 pos = player.position;
        pos.y = fixedY; // Lock Y position
        transform.position = pos;
    }
}
