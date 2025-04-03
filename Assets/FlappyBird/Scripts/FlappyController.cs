using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FlappyController : MonoBehaviour
{
	public bool isStarted = false;
	public bool isAlive = true;
	
    public float jumpForce = 1f;
    public float rotationSpeed = 10f;
    private Rigidbody2D rb;
	public Animator anim;
    
	public SoundManager soundManager;
	public GameObject getReady, gameOver;
	
	#region Singleton Paterni (Sahnede yalnızca bir FlappyController olabilir ve her koddan .Ins yazılarak ulaşılabilir.)
	private static FlappyController _instance;
	public static FlappyController Ins{get{return _instance;}}
	#endregion
	
	private void Awake()
    {
		#region Singleton Paterni
		if(_instance != null && _instance != this){Destroy(this.gameObject);}
		else{_instance = this;}
		#endregion
		
		rb = GetComponent<Rigidbody2D>();
		Init();
	}
	
    private void Update()
    {
        if(isAlive && Input.GetMouseButtonDown(0))
        {
			Action();
			
            rb.linearVelocity  = Vector2.up * jumpForce;
			soundManager.PlayAudio(SoundManager.Clips.wing);
        }	
    }
	
	private void FixedUpdate()
	{
		transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * rotationSpeed);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(!isAlive)return;
		
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            isAlive = false;
			
			GameOver();
        }
    }
	
	#region States
	private void Init()
	{
		rb.bodyType = RigidbodyType2D.Kinematic;
		anim.enabled = true;
		gameOver.SetActive(false);
		getReady.SetActive(true);
	}
	
	private void Action()
	{
		if(isStarted)return;
		
		isStarted = true;
		
		rb.bodyType = RigidbodyType2D.Dynamic;
		getReady.SetActive(false);
	}
	
	private void GameOver()
	{
		rb.linearVelocity = Vector2.zero;
		soundManager.PlayAudio(SoundManager.Clips.hit);
		anim.enabled = false;
		gameOver.SetActive(true);
		
		Invoke(nameof(Restart), 3);
		
		Debug.Log("Oyun Bitti!");
	}
	
	private void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	#endregion
	
	#region Score
	public int score = 0;
	public TextMeshProUGUI tmpText;
	public void IncreaseScore()
	{
		score ++;
		tmpText.text = score.ToString();
		soundManager.PlayAudio(SoundManager.Clips.score);
	}
	
	/* private void ResetScore()
	{
		score = 0;
		tmpText.text = score.ToString();
	} */
	#endregion
}