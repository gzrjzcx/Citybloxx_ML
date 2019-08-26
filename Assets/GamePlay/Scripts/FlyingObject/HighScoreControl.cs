using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreControl : MonoBehaviour
{
	private class Record
	{
		public int score;
		public string name;
	}

	[SerializeField]
	private List<Record> records = new List<Record>();

	private GameObject highScoreBoardPrefab;
	private GameObject highScore;
	private int listLength = 10;
	private string scoreKey = "highScore_";
	private string nameKey = "highScoreName_";

	void Start()
	{
		// PlayerPrefs.DeleteAll();
		for(int i=0; i<listLength; i++)
		{
			Record r = new Record();
			r.score = PlayerPrefs.GetInt(scoreKey + i.ToString());
			r.name = PlayerPrefs.GetString(nameKey + i.ToString(), "Anonymous");
			records.Add(r);
		}
		records = records.OrderBy( r => r.score ).ToList();
	}

	public void UpdateHighScore(int curScore, string curName)
	{
		if(curScore > records[0].score)
		{
			records[0].score = curScore;
			records[0].name = curName;			
		}
		records = records.OrderBy( r => r.score ).ToList();
		for(int i = 0; i<listLength; i++)
		{
			PlayerPrefs.SetInt(scoreKey + i.ToString(), records[i].score);
			PlayerPrefs.SetString(nameKey + i.ToString(), records[i].name);				
		}
	}

	public void ShowHighScore()
	{
		for(int i=listLength-1; i>0; i--)
		{
			if(records[i].name != null 
				&& GameControl.instance.stackedPieceNum == records[i].score)
			{
				SpawnHighScoreBoard(records[i].name);
			}
		}
	}

	void SpawnHighScoreBoard(string name)
	{
		string pfbPath = "highScore/prefabs/HighScoreBoard";
		highScoreBoardPrefab = Resources.Load<GameObject>(pfbPath);

		Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, 0, 8));
		float offsetX = 3f;
		float offsetY = 2f;
		spawnPos.x += offsetX;
		spawnPos.y -= offsetY;

		highScore = Instantiate(highScoreBoardPrefab, spawnPos, Quaternion.identity);
		highScore.GetComponent<HighScore>().nameText.text = name;
		highScore.transform.SetParent(this.transform, true);
	}

}
