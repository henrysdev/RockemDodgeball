using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldNetworkedObject : MonoBehaviour
{
    public int objectId;
    public enum ObjectType {
        Player,
        Ball
    };
    public ObjectType objectType;
    public bool puller = false;
    public bool pusher = false;
    public GamestateController gamestateController;

    public float lerpSpeed;
    public int frameBufferSize = 5;

    private Queue<Frame> frames;
    private float updateTimer;
    private float updateFrequency;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    
    private Frame tempFrame;
    private float lerpStartTime;
    private Vector3 lerpStartPosition;
    private Quaternion lerpStartRotation;


    void Awake()
    {
        switch (objectType)
        {
            case ObjectType.Ball:
                objectId = transform.GetComponent<Dodgeball>().id;
                break;
            case ObjectType.Player:
                objectId = transform.GetComponent<DodgeballPlayer>().id;
                break;
        }
        frames = new Queue<Frame>(frameBufferSize);
        updateFrequency = (1f / gamestateController.framesPerSecond);
    }

    public void PushFrame(Vector3 pos, Quaternion rot)
    {
        // Pop frame off the end and make lerp target if available
        if (frames.Count >= frameBufferSize) {
            Frame targetFrame = frames.Dequeue();
            tempFrame = targetFrame;
        }

        // Push new frame
        Frame frame = new Frame(pos, rot);
        frames.Enqueue(frame);
    }

    void FixedUpdate()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer > updateFrequency)
        {
            updateTimer = 0.0f;
            if (pusher)
            {
                switch (objectType)
                {
                    case ObjectType.Ball:
                        gamestateController.BallUpdate(objectId, transform.position, transform.rotation);
                        break;
                    case ObjectType.Player:
                        gamestateController.PlayerUpdate(transform.position, transform.rotation);
                        break;
                }
            }
            else if (puller)
            {
                // Hack: tempFrame allows us to use mainthread time for lerpStartTime
                if (tempFrame != null)
                {
                    lerpStartPosition = transform.position;
                    targetPosition = new Vector3(-tempFrame.position.x, tempFrame.position.y, tempFrame.position.z);
                    targetRotation = tempFrame.rotation;
                    lerpStartTime = Time.time;
                    tempFrame = null;
                }
                if (frames.Count >= frameBufferSize && frames.Peek() != null)
                {
                    transform.position = Vector3.Lerp(lerpStartPosition, targetPosition, lerpSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(lerpStartRotation, targetRotation, lerpSpeed * Time.deltaTime);
                }
            }
        }
    }
}

public class Frame
{
    public Vector3 position;
    public Quaternion rotation;

    public Frame(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
