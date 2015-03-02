using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    public static int _playerScore = 0;
    public GUIText _scoreText;
	void Start () 
    {
	
	}
	

	void Update () 
    {
        Movement();
        transform.Rotate(2 +(int)(Time.time / 10), 0, 0);
        _playerScore = 1 + (int)Time.time ;
        _scoreText.text = "Score : " + _playerScore;
        //print((Time.time).ToString());
	}

    void Movement()
    {
        if (transform.position.x > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(0, .5f, 0);
            }
        }
        else if (transform.position.x < 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position = new Vector3(0, .5f, 0);
            }
        } 
        else if(transform.position.x == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(-2, .5f, 0);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position = new Vector3(2, .5f, 0);
            }
        }
        

        

    }
}
