using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioSource bigHit, bigHit2, bigHit3;
    public AudioSource smallHit, smallHit2, smallHit3;
    public AudioSource ambience;
    public AudioSource clapSFX;
    public float randNum;

    public BallSpawner lclBallSpawner;

    // Start is called before the first frame update
    void Start()
    {
        lclBallSpawner = gameObject.GetComponent<BallSpawner>();
        ambience.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lclBallSpawner.sound.isOn)
            {
                if (lclBallSpawner.isBigHit)
                {
                    clapSFX.PlayDelayed((float)0.5);
                }
                hitSFX();
            }
        }

        if (lclBallSpawner.sound.isOn)
        {
            if (!ambience.isPlaying)
            {
                ambience.Play();
            }
        }
        else
        {
            ambience.Pause();
        }
    }


    public void hitSFX()
    {
        randNum = Random.Range(0, 9);
        if (lclBallSpawner.isBigHit)
        {
            if (randNum < 3)
            {
                bigHit.Play();
            }
            else if (randNum < 6)
            {
                bigHit2.Play();
            }
            else
            {
                bigHit3.Play();
            }
        }
        else
        {
            if (randNum < 3)
            {
                smallHit.Play();
            }
            else if (randNum < 6)
            {
                smallHit2.Play();
            }
            else
            {
                smallHit3.Play();
            }
        }
    }
}