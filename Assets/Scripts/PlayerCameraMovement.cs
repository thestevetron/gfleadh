using UnityEngine;
using System.Collections;

public class PlayerCameraMovement : MonoBehaviour 
{
    public GameObject _Camera, _directionSetter, _controller;
    public static int _playerScore = 0;
    public GUIText _scoreText;
    public static int _playerFacing;
    bool _rotateCamera;
    int centrePos;
    DestroyGround dest;
	void Start ()
    {
        _playerFacing = 1;
        centrePos = 1;
        _rotateCamera = false;
	}
	
	void Update () 
    {
        Debug.Log("Position" + centrePos);
        Movement();
        //transform.Rotate(3,0,0);
        //_playerScore = 1 + (int)Time.time;
        _playerScore = (int)transform.position.z;
        _scoreText.text = "FPS : " + 1.0f / Time.deltaTime;
        _directionSetter.transform.position = transform.position;
        CameraSettings();
        _controller = GameObject.FindGameObjectWithTag("Controller");
        if(_rotateCamera)
        {
            RotateCamera();
        }
	}

    void RotateCamera()
    {
        if (_playerFacing == 1)
        {

            _Camera.transform.localRotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, -90, 0), 5f);
        }
        else if (_playerFacing == 0)
        {

            _Camera.transform.localRotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, 0, 0), 5f);
        }
    }
    void CameraSettings()
    {
        if(_playerFacing == 0)
        {
            //_Camera.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            //_Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, -90, 0), 1000);
            _Camera.transform.position = new Vector3(transform.position.x + 8, 4, transform.position.z);
        }
        else if(_playerFacing == 1)
        {
            //_Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, Quaternion.Euler(0, 0, 0), 1000);
            //_Camera.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _Camera.transform.position = new Vector3(transform.position.x, 4, transform.position.z - 8);
        }

    }

    void Movement()
    {

        //transform.rigidbody.AddForce(new Vector3(0, 0, 7));
        transform.rigidbody.AddForce(_directionSetter.transform.forward * 7, ForceMode.Acceleration);
        //transform.position += new Vector3(0, 0, .5f);
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
                _rotateCamera = true;
                Quaternion target = Quaternion.Euler(0, -90, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target, 1000);
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
                _rotateCamera = true;
                Quaternion target = Quaternion.Euler(0, 0, 0);
                //transform.rotation = Quaternion.Slerp(transform.rotation, target, 1000);
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
        _rotateCamera = false;
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
