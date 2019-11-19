using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Brick"))
        {
            Destroy(collision.gameObject);
        }
        
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}
}
