using UnityEngine;

public class BackgroundFloow : MonoBehaviour
{
	public Transform bird;
	
    void Update()
    {
		transform.position = new Vector3(bird.position.x, transform.position.y, transform.position.z);
    }
}
