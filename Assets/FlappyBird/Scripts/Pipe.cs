using UnityEngine;

public class Pipe : MonoBehaviour
{
	public Transform pipe;
	public float speed = 2;
	
    void Update()
    {
		if(!FlappyController.Ins.isStarted || !FlappyController.Ins.isAlive)return;
		
		transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
