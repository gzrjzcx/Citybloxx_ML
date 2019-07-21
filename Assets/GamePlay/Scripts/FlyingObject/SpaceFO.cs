using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpaceFO : MonoBehaviour
{

	public enum SpaceFOType
    {
        UFO = 0,  // Scene has been loaded, but game not start
        ROCKET,
        SHIP,
        SATELLITE
    }

    public SpaceFOType spaceFOType;

    private Vector3 spawnPos;
    [SerializeField]
    private float rectLength;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.localPosition;
        PlayRocketAnim();
    }

    void PlayAnim()
    {

    }

    void PlayRocketAnim()
    {

    	Vector3[] wayPoints = new Vector3[15];
    	wayPoints = GetRoundBezierWayPoints(Vector3.zero, -1);

    	this.transform.DOLocalPath(wayPoints, 8f, PathType.CubicBezier, PathMode.TopDown2D)
    		.SetLookAt(0f)
    		.SetEase(Ease.InOutCubic);
    }

    Vector3[] GetRoundBezierWayPoints(Vector3 center, int sign)
    {
    	float radiusX = 1f * -sign;
    	float radiusY = 1f;
    	float offsetX = 0.5f * sign;
    	float offsetY = 0.5f;
    	float end_offsetX = 2f * sign;
    	float end_offsetY = 2f;
    	float screen_offsetX = 2f * sign;
    	float screen_offsetY = 4f;
    	Vector3[] wayPoints = new Vector3[15];

    	Vector3 topPoint = new Vector3(center.x, center.y + radiusY, 0f);
    	wayPoints[0] = topPoint;
    	wayPoints[1] = new Vector3(spawnPos.x, spawnPos.y + end_offsetY, 0f);
    	wayPoints[2] = new Vector3(topPoint.x + offsetX, topPoint.y, 0f);

    	Vector3 secondPoint = new Vector3(center.x + radiusX, center.y, 0f);
    	wayPoints[3] = secondPoint;
    	wayPoints[4] = new Vector3(topPoint.x - offsetX, topPoint.y, 0f);
    	wayPoints[5] = new Vector3(secondPoint.x, secondPoint.y + offsetY, 0f);

    	Vector3 bottomPoint = new Vector3(center.x, center.y - radiusY, 0f);
    	wayPoints[6] = bottomPoint;
       	wayPoints[7] = new Vector3(secondPoint.x, secondPoint.y - offsetY, 0f);
    	wayPoints[8] = new Vector3(bottomPoint.x - offsetX, bottomPoint.y, 0f);

    	Vector3 forthPoint = new Vector3(center.x - radiusX, center.y, 0f);
    	wayPoints[9] = forthPoint;
       	wayPoints[10] = new Vector3(bottomPoint.x + offsetX, bottomPoint.y, 0f);
    	wayPoints[11] = new Vector3(forthPoint.x, forthPoint.y - offsetY, 0f);

    	// Vector3 topPointAgain = topPoint;
    	// wayPoints[12] = topPointAgain;
    	// wayPoints[13] = new Vector3(forthPoint.x, forthPoint.y + offsetY, 0f);
    	// wayPoints[14] = new Vector3(topPointAgain.x + offsetX, topPointAgain.y, 0f);

    	Vector3 endPoint = new Vector3(
    		-spawnPos.x - screen_offsetX, spawnPos.y + screen_offsetY, 0f);
    	wayPoints[12] = endPoint;
    	wayPoints[13] = new Vector3(forthPoint.x, forthPoint.y + end_offsetY, 0f);
    	wayPoints[14] = new Vector3(endPoint.x + end_offsetX, endPoint.y, 0f);

    	return wayPoints;

    }













}
