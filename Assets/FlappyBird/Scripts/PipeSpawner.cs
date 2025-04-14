using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
	// Kuşun Transform bileşeni (pozisyon, dönüş, ölçek bilgisi)
	public Transform bird;
	
	// Boruları tutan liste
	public List<Pipe> pipeList = new List<Pipe>();
	
	// Borular arasındaki yatay mesafe (X ekseninde)
	public ushort gap = 1;
	
	// Skorun artıp artmadığını kontrol eden değişken
	private ushort isScoreChanged;
	
	private void Awake()
	{
		// Başlangıçta döngü çalıştır
		for(int i = 0; i < pipeList.Count; i++)
		{
			// Her boruyu yeniden konumlandır
			RePosition(pipeList[i].pipe);
		}
	}
	
	private void Update()
	{
		// Oyun başlamadıysa veya kuş öldüyse bu satırdan aşağıya inme
		if(!FlappyController.Ins.isStarted || !FlappyController.Ins.isAlive)return;
		
		ScoringSystem();
		
		RecyclePipes();
	}
	
	private void ScoringSystem()
	{
		// 0.1 çünkü gap değerinden ileride olmalı
		if(isScoreChanged == 0 && bird.position.x > pipeList[0].pipe.position.x + 0.1f) // İlk boruyu geçti mi?
		{
			isScoreChanged = 1; // Skor bir kez arttı
			FlappyController.Ins.IncreaseScore(); // Skoru artır
		}
		else if(isScoreChanged == 1 && bird.position.x > pipeList[1].pipe.position.x + 0.1f) // İkinci boruyu geçti mi?
		{
			isScoreChanged = 2; // Skor ikinci kez arttı
			FlappyController.Ins.IncreaseScore(); // Skoru artır
		}
	}
	
	private void RecyclePipes()
	{
		// Kuş ile ilk boru arasındaki mesafeyi ölç
		float dist = Mathf.Abs(bird.position.x - pipeList[0].pipe.position.x);
		
		// Eğer kuş, borudan gap kadar uzaklaştıysa listedeki ilk boruyu yeniden konumlandır
		if(dist > gap)
		{
			Pipe firstPipe = pipeList[0]; // İlk boruyu al
			Transform firstPipeTR = pipeList[0].pipe; // İlk borunun Transform bileşenini al
			
			// Son borunun gap kadar ilerisinde yeni X konumu belirle
			float xAxis = pipeList[pipeList.Count -1].pipe.localPosition.x + gap;
			
			// İlk boruyu yeni X konumuna taşı
			firstPipeTR.position = new Vector3(xAxis, firstPipeTR.localPosition.y, firstPipeTR.localPosition.z); 
			
			// Y konumunu da rastgele değiştir
			RePosition(firstPipeTR); 
			
			// İlk boruyu listeden çıkar
			pipeList.RemoveAt(0);
			// Eski ilk boruyu listenin sonuna ekle
			pipeList.Add(firstPipe); 
			
			// Skor artışı sıfırla
			isScoreChanged = 0; 
		}
	}
	
	private void RePosition(Transform pipe)
	{
		// Yüksekliği rastgele belirle diğer eksenler olduğu gibi kalsın
		pipe.position = new Vector3(pipe.position.x, Random.Range(-0.2f, 0.701f), pipe.position.z); 
	}
}

