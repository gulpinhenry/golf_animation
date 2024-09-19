using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Script : MonoBehaviour
{
    [SerializeField] float killZone = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > killZone)
        {
            Destroy(this.gameObject);
        }
    }
}
