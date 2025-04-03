using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Clips { wing, hit, score }
    
    public AudioClip wing, hit, score;
	
    private AudioSource mainAudioSource;
    private AudioSource scoreAudioSource;

    private void Awake()
	{
		mainAudioSource = gameObject.AddComponent<AudioSource>();
		scoreAudioSource = gameObject.AddComponent<AudioSource>();
		scoreAudioSource.volume = 0.4f;
	}

    public void PlayAudio(Clips clip)
    {
		AudioSource source = mainAudioSource;
		
        switch (clip)
        {
            case Clips.wing:
                source.clip = wing;
                break;
            case Clips.hit:
                source.clip = hit;
                break;
			case Clips.score:
				source = scoreAudioSource;
				source.clip = score;
				break;
        }
		
		source.pitch = Random.Range(0.9f, 1.11f);
        
        source.Play();
    }
}
