using UnityEngine;
using System.Collections;

public class DestroyGround : MonoBehaviour 
{
    GameObject _player, _controller, _curved;
    public Vector3 lastSpawnedGround;
    public static int numbertimesspawned = 0;
   

	void Start () 
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _controller = GameObject.FindGameObjectWithTag("Controller");
        _curved = GameObject.FindGameObjectWithTag("Curved");
        DoTheStuff();
	}
	

	void Update () 
    {
        DoTheStuff();
	}

    public void DoTheStuff()
    {
        float distance = Vector3.Distance(_player.transform.position, this.transform.position);
        if (PlayerCameraMovement._playerFacing == 1)
        {
            if (transform.position.z < (_player.transform.position.z - 17))
            {
                Destroy(this.gameObject);
                //if(_controller.GetComponent<GroundGeneration>()._groundPieces.Contains(this.gameObject))
                //{
                //    _controller.GetComponent<GroundGeneration>()._groundPieces.Remove(this.gameObject);
                //}
            }
            //if (_controller.GetComponent<GroundGeneration>()._groundPieces.Count <= 10)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        _controller.GetComponent<GroundGeneration>().CreateGroundStraight(_curved.transform.position + new Vector3(0, 0, 8 * i));
            //        numbertimesspawned += 1;
            //    }
            //}
           
        }
        else if (PlayerCameraMovement._playerFacing == 0)
        {
            if (transform.position.x > (_player.transform.position.x + 17))
            {
                Destroy(this.gameObject);
            }
           
        }

    }

}
