using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Script : MonoBehaviour
{
    [SerializeField] float killZone = 10;
    [SerializeField] bool canBePressed;
    [SerializeField] AudioSource hole;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > killZone)
        {

            if (gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                Pause();
            }

            Destroy(this.gameObject);

        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
            hole.Play();
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(5);
    }
}
