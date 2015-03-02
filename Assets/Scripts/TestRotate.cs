using UnityEngine;
using System.Collections;

public class TestRotate : MonoBehaviour 
{
    public Transform from;
    public Transform to;
    public float speed = 0.1F;

	void Start () 
    {

	}
	
	void Update ()
    {
        transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);
	}
}
