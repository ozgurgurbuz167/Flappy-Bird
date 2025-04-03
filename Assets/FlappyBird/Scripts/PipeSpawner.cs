using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
	public Transform bird;
    public GameObject pipePrefab;
	public List<Pipe> pipeList = new List<Pipe>();
	
	public ushort gap = 1;
	
	private ushort isScoreChanged;
	
	private void Awake()
	{
		for(int i = 0; i < pipeList.Count; i++)
		{
			RePosition(pipeList[i].pipe);
		}
	}
	
	private void Update()
	{
		if(!FlappyController.Ins.isStarted || !FlappyController.Ins.isAlive)return;
		
		// 0.1 çünkü gap değerinden ileride olmalı
		if(isScoreChanged == 0 && bird.position.x > pipeList[0].pipe.position.x + 0.1f)
		{
			isScoreChanged = 1;			
			FlappyController.Ins.IncreaseScore();
		}
		else if(isScoreChanged == 1 && bird.position.x > pipeList[1].pipe.position.x + 0.1f)
		{
			isScoreChanged = 2;			
			FlappyController.Ins.IncreaseScore();
		}
		
		float dist = Mathf.Abs(bird.position.x - pipeList[0].pipe.position.x);
		
		if(dist > gap)
		{
			Pipe firstPipe = pipeList[0];
			Transform firstPipeTR = pipeList[0].pipe;
			
			float xAxis = pipeList[pipeList.Count -1].pipe.localPosition.x + gap;
			firstPipeTR.position = new Vector3(xAxis, firstPipeTR.localPosition.y, firstPipeTR.localPosition.z);
			
			RePosition(firstPipeTR);
			
			pipeList.RemoveAt(0);
			pipeList.Add(firstPipe);
			
			isScoreChanged = 0;
		}
	}
	
	private void RePosition(Transform pipe)
	{
		pipe.position = new Vector3(pipe.position.x, Random.Range(-0.2f, 0.701f), pipe.position.z);
	}
}

