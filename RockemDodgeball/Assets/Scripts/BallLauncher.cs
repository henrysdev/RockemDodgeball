using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject leftBallHolder;
    public GameObject rightBallHolder;
    public Transform leftBallPos;
    public Transform rightBallPos;
    public GameObject projectile;
    public float launchVelocity;
    private Animation anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO change to keycode reference for portability
        if (Input.GetKeyDown(KeyCode.LeftShift)) HandleBallLaunch();
    }

    void HandleBallLaunch()
    {
        if (rightBallHolder) ThrowBall();
        ReloadRightBall();
    }

    void ThrowBall()
    {
        // Play throw animation
        anim.Play();

        // Launch projectile
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        GameObject ball = Instantiate(projectile, rightBallHolder.transform);
        Vector3 direction = (new Vector3(h, 0, v)).normalized;
        Vector3 converted = Camera.main.transform.TransformDirection(direction) * launchVelocity * Time.deltaTime;
        ball.GetComponent<Rigidbody>().velocity = new Vector3(converted.x, converted.y, converted.z);
    }

    void ReloadRightBall()
    {
        // TODO Adjust ball models
        rightBallHolder = leftBallHolder;
        leftBallHolder = null;
    }

    void CollectBall(GameObject ball)
    {
        if (!rightBallHolder)
        {
            rightBallHolder = ball;
        }
        else if (!leftBallHolder)
        {
            leftBallHolder = ball;
        }

        Destroy(ball);
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject colliderObj = collider.gameObject;
        if (colliderObj.CompareTag("Ball"))
        {
            CollectBall(colliderObj);
        }
    }
}
