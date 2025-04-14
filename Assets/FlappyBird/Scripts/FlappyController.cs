using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FlappyController : MonoBehaviour
{
	public bool isStarted = false; 		// Oyun başladı mı?
	public bool isAlive = true;    		// Oyuncu canlı mı?

	public float jumpForce = 1f;   		// Zıplama kuvveti
	public float rotationSpeed = 10f; 	// Dönme hızı

	private Rigidbody2D rb;   // Fizik motoru bileşeni
	public Animator anim;     // Animasyon kontrolcüsü

	public SoundManager soundManager; 		// Sesleri yönetecek nesne
	public GameObject getReady, gameOver; 	// "Hazır Ol" ve "Oyun Bitti" panelleri

	#region Singleton Paterni (Tek bir FlappyController olsun, her yerden FlappyController.Ins yazılarak ulaşılabilsin)
	private static FlappyController _instance;
	public static FlappyController Ins { get { return _instance; } } // Dışarıdan erişim için
	#endregion
	
	private void Awake()
    {
		#region Singleton Paterni
		if(_instance != null && _instance != this){Destroy(this.gameObject);} // Eğer varsa, yok et
		else{_instance = this;} // Yoksa bu nesne olsun
		#endregion
		
		rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileşenini al
		Init(); // Başlangıç ayarlarını yap
	}
	
	private void Update()
	{
		if (isAlive && Input.GetMouseButtonDown(0)) // Eğer canlıysa ve tıklandıysa
		{
			Action(); // İlk hareketi başlat
			rb.linearVelocity = Vector2.up * jumpForce; // Yukarı doğru zıplat
			soundManager.PlayAudio(SoundManager.Clips.wing); // Kanat sesi çal
		}
	}
	
	private void FixedUpdate()
	{
		// Hıza göre kuşu Z ekseninde döndür (Rotation X[0] Y[0] Z[?])
		transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * rotationSpeed);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isAlive) return; // Zaten öldüyse bir şey yapma

		if (collision.gameObject.CompareTag("Obstacle")) // Eğer engele çarptıysa
		{
			isAlive = false; // Oyuncu öldü
			GameOver(); // Oyun Bitti işlemlerini başlat
		}
	}
	
	#region States
	
	private void Init()
	{
		rb.bodyType = RigidbodyType2D.Kinematic; // Başta fizik kapalı
		anim.enabled = true; // Animasyon bileşeni aktif
		gameOver.SetActive(false); // "Oyun Bitti" objesi kapalı
		getReady.SetActive(true); // "Hazır Ol" objesi açık
	}

	private void Action()
	{
		if (isStarted) return; // Zaten başladıysa tekrar başlatma
		
		isStarted = true; // Oyun başlıyor
		rb.bodyType = RigidbodyType2D.Dynamic; // Fizik açılıyor
		getReady.SetActive(false); // "Hazır Ol" objesi kapanıyor
	}
	
	private void GameOver()
	{
		rb.linearVelocity = Vector2.zero; // Kuşu durdur
		soundManager.PlayAudio(SoundManager.Clips.hit); // Çarpma sesi çal
		anim.enabled = false; // Animasyonu durdur
		gameOver.SetActive(true); // "Oyun Bitti" panelini aç
		
		Invoke(nameof(Restart), 3); // 3 saniye sonra restart
		Debug.Log("Oyun Bitti!"); // Konsola yaz
	}
	
	private void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sahneyi yeniden yükle
	}
	#endregion
	
	#region Skor Yönetimi
	public int score = 0; // Başlangıç skoru
	public TextMeshProUGUI tmpText; // Skor yazısı

	public void IncreaseScore()
	{
		score++; // Skoru bir artır
		tmpText.text = score.ToString(); // Ekrana yaz
		soundManager.PlayAudio(SoundManager.Clips.score); // Skor sesi çal
	}
	#endregion
}