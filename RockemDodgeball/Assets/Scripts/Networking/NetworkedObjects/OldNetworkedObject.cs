using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldNetworkedObject : MonoBehaviour
{
    public bool puller = false;
    public bool pusher = false;
    public GamestateController gamestateController;

    private float updateTimer;
    private float updateFrequency;
    public Vector3 position;
    public Quaternion rotation;

    void Awake()
    {
        updateFrequency = (1f / gamestateController.framesPerSecond);
    }

    public void SetTransform(Vector3 pos, Quaternion rot)
    {
        position = pos;
        rotation = rot;
    }

    void FixedUpdate()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer > updateFrequency)
        {
            updateTimer = 0.0f;
            if (pusher)
                gamestateController.PlayerUpdate(transform.position, transform.rotation);
            else if (puller)
            {
                transform.position = position;
                transform.rotation = rotation;
            }
        }
    }
}
