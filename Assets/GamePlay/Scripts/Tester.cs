using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Tester : MonoBehaviour
{	
    public float totalThinkingTime = 0;
    public int missNum = 0;
    public int dcNum = 0;
    public int helpNum = 0;
    public int adjustNum = 0;

    public PauseMenu menu;

    public enum TestType
    {
        NORMAL = 0,  
        TIME_LIMIT,
        BLOCK_LIMIT
    }
    public TestType testType;

    public enum IsAI
    {
        NOAI = 0,  
        ATS,
        DDA,
        ATSDDA
    }
    public IsAI isAI;

    void Start()
    {
    	Title();
    	IsAISet();
    }

    void Update()
    {
    	if(testType == TestType.TIME_LIMIT)
    	{
    		TimeLimitTest();
    	}
    	else if(testType == TestType.BLOCK_LIMIT)
    	{
    		BlockLimitTest();
    	}
    }

    public void IsAISet()
    {
    	if(isAI == IsAI.ATS)
    	{
    		menu.ToggleATS();
    	}
    	else if(isAI == IsAI.DDA)
    	{
    		menu.ToggleDDA();
    	}
    	else if(isAI == IsAI.ATSDDA)
    	{
    		menu.ToggleDDA();
    		menu.ToggleATS();
    	}
    }

    public void Title()
    {
		using(StreamWriter sw = new StreamWriter(testType.ToString() + ".csv", true))
		{
			sw.WriteLine(
			 "total_num" + "\t" + "total_score" + "\t" + "thinkingTime" + "\t" + "missed_num" + "\t"
			  + "total_time" + "\t" + "dc_num" + "\t" + "help_num" + "\t" + "adjust_num" + "\t" + "isAI");
		}   		
    }

    public void Record()
    {
		using(StreamWriter sw = new StreamWriter(testType.ToString() + ".csv", true))
		{
			var gm = GameControl.instance;
			sw.WriteLine(
			  gm.stackedPieceNum + "\t" +
			  gm.populationScore + "\t" + 
			  (totalThinkingTime / gm.stackedPieceNum).ToString("F3") + "\t" + 
			  missNum + "\t" +
			  Time.time.ToString("F3") + "\t" + 
			  dcNum + "\t" + 
			  helpNum + "\t" + 
			  adjustNum + "\t" + 
			  isAI.ToString());
		}  
    }

    public void TimeLimitTest()
    {
    	if(Time.time > 300)
    	{
    		Record();
    		EditorApplication.isPaused = true;
    	}
    }

    public void BlockLimitTest()
    {
    	if(GameControl.instance.stackedPieceNum > 50)
    	{
    		Record();
    		EditorApplication.isPaused = true;    		
    	}
    }

























}
