using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{

	// public GameObject stackDeadCenterEffectGO;
	// public GameObject fallenWaterEffectGO;
	// public GameObject comboPeriodEffectGO;

	public ParticleSystem stackDeadCenterAnim;
	public ParticleSystem fallenWaterAnim;
	public ParticleSystem comboPeriodAnim;

    public void PlayStackDeadCenterAnim(Vector3 pos)
    {
    	if(!stackDeadCenterAnim.isPlaying)
    	{
    		stackDeadCenterAnim.transform.position = pos;
    		stackDeadCenterAnim.Play();
    	}
    }


    public void PlayFallenWaterAnim(Vector3 pos)
    {
    	if(!fallenWaterAnim.isPlaying)
    	{
    		fallenWaterAnim.transform.position = pos;
    		fallenWaterAnim.Play();
    	}
    }

    public void PlayComboPeriodAnim()
    {
        float centerPosY = GameControl.instance.columnObj.GetCenterPostion();
        var sh = comboPeriodAnim.shape;
        sh.shapeType = ParticleSystemShapeType.Box;
        sh.position = new Vector3(0, centerPosY, 0);
        sh.scale = (new Vector3(1, GameControl.instance.columnObj.transform.childCount, 0));

        if(!comboPeriodAnim.isPlaying)
        {
            comboPeriodAnim.Play();
        }
    }

    public void StopComboPeriodAnim()
    {
    	comboPeriodAnim.Stop();
    }
}
