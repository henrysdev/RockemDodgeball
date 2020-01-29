using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Queue<GameObject> ballQueue;
    public int maxBalls;
    public Transform leftBallPos;
    public Transform rightBallPos;
    public GameObject projectile;
    public float launchVelocity;
    public Transform launchTrans;

    private Animation anim;
    private DodgeballPlayer player;

    // Start is called before the first frame update
    void Awake()
    {
        player = transform.GetComponent<DodgeballPlayer>();
        ballQueue = new Queue<GameObject>(maxBalls);
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO change to keycode reference for portability
        if (Input.GetKeyDown(KeyCode.LeftShift) && projectile != null) HandleBallLaunch();
    }

    void HandleBallLaunch()
    {
        ThrowBall();
    }

    void ThrowBall()
    {
        //GameObject ball = Instantiate(projectile, launchTrans.position, Quaternion.identity);
        projectile.transform.position = launchTrans.position;
        projectile.transform.rotation = Quaternion.identity;
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.GetComponent<Dodgeball>().thrower = player;
        projectile.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * launchVelocity, ForceMode.Impulse);
        //projectile = null;
    }

    void CollectBall(GameObject ball)
    {
        ballQueue.Enqueue(ball);
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
