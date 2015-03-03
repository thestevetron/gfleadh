using UnityEngine;
using System.Collections;

public class PlayerCameraMovement : MonoBehaviour 
{
    public GameObject _Camera, _directionSetter, _controller;
    public static int _playerScore = 0;
    public GUIText _scoreText;
    public static int _playerFacing;
    int centrePos;
    public float turnTime;
    DestroyGround dest;


	void Start ()
    {
        _playerFacing = 1;
        centrePos = 1;
	}
	
	void Update () 
    {
        Debug.Log("Rotation " + _Camera.transform.rotation);
        Movement();
        _playerScore = (int)transform.position.z;
        _scoreText.text = "FPS : " + 1.0f / Time.deltaTime;
        _directionSetter.transform.position = transform.position;
        CameraSettings();
        _controller = GameObject.FindGameObjectWithTag("Controller");
	}

    IEnumerator RotateCamera()
    {
        if (_playerFacing == 0)
        {
            while (_Camera.transform.rotation != Quaternion.Euler(0, -90, 0))
            {
                _Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, -90, 0), .25f);
                yield return new WaitForSeconds(0.02f);
            }
        }
        else if (_playerFacing == 1)
        {
            while (_Camera.transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                _Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, 00, 0), .25f);
                yield return new WaitForSeconds(0.02f);
            }
        }
        CameraSettings();
    }
    void CameraSettings()
    {

        if (_playerFacing == 0)
        {
            //_Camera.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            //_Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, -90, 0), 1000);
            _Camera.transform.position = new Vector3(transform.position.x + 8, 4, transform.position.z);
        }
        else if (_playerFacing == 1)
        {
            //_Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, 0, 0), 1000);
            //_Camera.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _Camera.transform.position = new Vector3(transform.position.x, 4, transform.position.z - 8);
        }
    }

    void Movement()
    {
        transform.rigidbody.AddForce(_directionSetter.transform.forward * 8, ForceMode.Acceleration);
        ///Jump
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    transform.position += new Vector3(0, 2, 0);
        //}
        if (_playerFacing == 1)
        {
            if (centrePos == 2)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position = new Vector3(transform.position.x-2, transform.position.y, transform.position.z);
                    centrePos = 1;
                }
            }
            else if (centrePos == 0)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                    centrePos = 1;
                }
            }
            else if (centrePos == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                    centrePos = 0;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                    centrePos = 2;
                }
            }
        }

        else if (_playerFacing == 0)
        {
            if (centrePos == 2)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    //Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), 50);
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
                    centrePos = 1;
                }
            }
            else if (centrePos == 0)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //Vector3.Lerp(transform.position, new Vector3(transform.position.x, .755f, transform.position.z + 2), 50);
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
                    centrePos = 1;
                }
            }
            else if (centrePos == 1)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    //Vector3.Lerp(transform.position, new Vector3(transform.position.x, .755f, transform.position.z - 2), 50);
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
                    centrePos = 0;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //Vector3.Lerp(transform.position, new Vector3(transform.position.x, .755f, transform.position.z + 2), 50);
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
                    centrePos = 2;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "GCC")
        {
            if (_playerFacing == 1)
            {
                _playerFacing = 0;
                ///Camera Rotation
                CameraSettings();
                StartCoroutine("RotateCamera");
                Quaternion target = Quaternion.Euler(0, -90, 0);
                transform.localRotation = target;
                centrePos = 1;
                transform.rigidbody.freezeRotation = true;
                rigidbody.velocity = new Vector3(-rigidbody.velocity.z, 0, 0);
                _directionSetter.transform.localRotation = target;
                _controller.GetComponent<GroundGeneration>().CreateGroundStraight();

                Destroy(col.gameObject);
                Debug.Log("CORNER" + _playerFacing);
            }
            else if (_playerFacing == 0)
            {

                _playerFacing = 1;
                ///Camera Rotation
                CameraSettings();
                StartCoroutine("RotateCamera");
                Quaternion target = Quaternion.Euler(0, 0, 0);
                transform.rigidbody.freezeRotation = true;
                transform.localRotation = target;
                centrePos = 1;
                rigidbody.velocity = new Vector3(0, 0, -rigidbody.velocity.x);
                _directionSetter.transform.localRotation = target;
                _controller.GetComponent<GroundGeneration>().CreateGroundStraight();
                Destroy(col.gameObject);
                Debug.Log("CORNER" + _playerFacing);
            }
        }

        if (col.gameObject.tag == "Curved")
        {
            print("HSDFHSDFJSDFJKSDFKSD");
        }
    }

    void OnTriggerExit(Collider col)
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            this.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        }

    }

    void OnCollisionExit()
    {
        this.rigidbody.constraints = RigidbodyConstraints.None;
    }
}
