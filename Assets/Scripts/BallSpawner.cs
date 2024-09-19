using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Transform club;

    [SerializeField] private float clubSpawnOffset = 20;
    [SerializeField] private float clubSwingOffset = 90;

    [SerializeField] private int maxBallForce;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject ballPrefab;

    private Vector3 clubDirection;
    private bool swing = true;
    private int ballForce;
    private GameObject ball;

    float newBallTimer;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = maxBallForce;
        ball = Instantiate(ballPrefab, club.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        clubDirection = Input.mousePosition - club.position;
        club.transform.right = clubDirection;
        Vector3 clubRotation = club.transform.rotation.eulerAngles;

        if (!swing)
        {
            club.rotation = Quaternion.Euler(clubRotation.x, clubRotation.y, clubRotation.z + clubSpawnOffset);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ball.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
            rb.AddForce(clubDirection.normalized * ballForce);
                
        }

        print("Ball pos = " + ball.transform.position);
        print("Club pos = " + club.position);

        if (newBallTimer > 2*Time.deltaTime && Mathf.Abs(ball.transform.position.x - club.position.x) >= 1)
        {
            ball = Instantiate(ballPrefab, club.position, Quaternion.identity);
            club.rotation = Quaternion.Euler(clubRotation.x, clubRotation.y, clubRotation.z - clubSwingOffset);
            newBallTimer = 0;
        }

        newBallTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ballForce = Mathf.Abs(Mathf.RoundToInt(Mathf.Sin(Time.time) * maxBallForce));
        slider.value = ballForce;
    }
}
