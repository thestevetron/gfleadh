using UnityEngine;
using System.Collections;

public class DestroyObj : MonoBehaviour 
{
    GameObject _player, _controller;
    GameObject to;
    public float speed = 0.001F;


	void Start () 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _controller = GameObject.FindGameObjectWithTag("Controller");
        to = GameObject.FindGameObjectWithTag("To");
        this.transform.Rotate(new Vector3(135, 0, 0));
        to.transform.position = new Vector3(transform.position.x, transform.position.y + .6f, transform.position.z - 1);
        
	}
	

	void Update ()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, to.transform.rotation, Time.time * speed);
        if(transform.position.z < (_player.transform.position.z - 5))
        {
            _controller.GetComponent<CreateObstacle>().Trees.Remove(this.gameObject);
            _controller.GetComponent<CreateObstacle>().Trees.Clear();
            Destroy(this.gameObject);

        }


	}
}
