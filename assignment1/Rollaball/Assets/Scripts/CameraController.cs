using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position; 
    }

    // Update is called once per frame
    void LateUpdate() // best for cameras
    {
        if (player != null)
    {
        transform.position = player.transform.position + offset;
    }

    }
}
