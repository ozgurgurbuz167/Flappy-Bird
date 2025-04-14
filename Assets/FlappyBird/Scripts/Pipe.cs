using UnityEngine;

public class Pipe : MonoBehaviour
{
	// Borunun (üst+alt) ana Transform bileşeni
	public Transform pipe;
	
	// Borunun hareket hızı (X ekseninde sola doğru)
	public float speed = 2;
	
    void Update()
    {
		// Oyun başlamadıysa veya kuş öldüyse bu satırdan aşağıya inme
		if(!FlappyController.Ins.isStarted || !FlappyController.Ins.isAlive)return;
		
		// Boruyu X ekseninde sola doğru hareket ettir
		transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
