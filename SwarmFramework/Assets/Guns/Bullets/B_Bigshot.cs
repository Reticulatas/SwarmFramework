using UnityEngine;
using System.Collections;

public class B_Bigshot : MonoBehaviour
{
    public float BulletSpeed = 10.0f;

	void Start ()
	{
	    transform.Rotate(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
	}
	
	void Update ()
    {
        transform.Translate(transform.forward * Time.deltaTime * BulletSpeed, Space.World);
    }
}
