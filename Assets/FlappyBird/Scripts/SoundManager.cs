using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Oynatılabilecek ses efektlerini temsil eden enum
    public enum Clips { wing, hit, score }
    
    // Her bir ses efekti için AudioClip referansları
    public AudioClip wing, hit, score;
	
    // Ana ses kaynağı (wing ve hit için kullanılacak)
    private AudioSource mainAudioSource;
    // Skor sesi için ayrı bir ses kaynağı (diğer sesleri bitmeden kapatmaması için)
    private AudioSource scoreAudioSource;
	
    private void Awake()
	{
		// GameObject'e ana ses kaynağını ekle
		mainAudioSource = gameObject.AddComponent<AudioSource>();
		
		// Skor sesi için ayrı bir ses kaynağı ekle
		scoreAudioSource = gameObject.AddComponent<AudioSource>();
		scoreAudioSource.volume = 0.4f; // Skor sesi daha düşük sesle çalacak
	}

    public void PlayAudio(Clips clip)
    {
		// Varsayılan olarak ana ses kaynağını kullan
		AudioSource source = mainAudioSource;
		
        switch (clip)
        {
            case Clips.wing: // "wing" sesi çalınacaksa
                source.clip = wing;
                break;
            case Clips.hit: // "hit" sesi çalınacaksa
                source.clip = hit;
                break;
			case Clips.score: // "score" sesi çalınacaksa
				source = scoreAudioSource; // Skor için özel ses kaynağı kullan
				source.clip = score;
				break;
        }
		
		// Sesin tonunu hafif rastgele değiştir (daha doğal bir his için)
		source.pitch = Random.Range(0.9f, 1.11f);
        
        // Seçilen sesi çal
        source.Play();
    }
}
