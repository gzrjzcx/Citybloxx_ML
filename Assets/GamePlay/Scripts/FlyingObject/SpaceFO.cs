using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpaceFO : MonoBehaviour
{

	public enum SpaceFOType
    {
        Alien = 0,  // Scene has been loaded, but game not start
        ROCKET,
        SHIP,
        SATELLITE
    }

    public SpaceFOType spaceFOType;
    public GameObject helloGO;
    public int sign;

    private Vector3 spawnPos;
    [SerializeField]
    private float rectLength;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = this.transform.localPosition;
        PlayAnim();
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void PlayAnim()
    {
        switch(spaceFOType)
        {
            case SpaceFOType.Alien:
                PlayAlienAnim();
                break;
            case SpaceFOType.ROCKET:
                PlayRocketAnim();
                break;
            case SpaceFOType.SATELLITE:
                PlaySatelliteAnim();
                break;
            case SpaceFOType.SHIP:
                PlayShipAnim();
                break;

        }
    }

    void PlayRocketAnim()
    {

    	Vector3[] wayPoints = new Vector3[15];
    	wayPoints = GetRoundBezierWayPoints(Vector3.zero, sign);

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
    	float screen_offsetX = 4f * sign;
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

    	Vector3 endPoint = new Vector3(
    		-spawnPos.x - screen_offsetX, spawnPos.y + screen_offsetY, 0f);
    	wayPoints[12] = endPoint;
    	wayPoints[13] = new Vector3(forthPoint.x, forthPoint.y + end_offsetY, 0f);
    	wayPoints[14] = new Vector3(endPoint.x + end_offsetX, endPoint.y, 0f);

    	return wayPoints;

    }


    void PlayShipAnim()
    {
        float offsetX = 0.2f * sign;
        float offsetY = 2f;
        float _x = spawnPos.x + offsetX;
        Vector3[] wayPoints = new Vector3[4];
        wayPoints[0] = spawnPos;
        wayPoints[1] = new Vector3(_x, spawnPos.y + offsetY * 2, 0f);
        wayPoints[2] = new Vector3(_x - offsetX, spawnPos.y + offsetY * 4, 0f);
        wayPoints[3] = new Vector3(_x + offsetX, spawnPos.y + offsetY * 6, 0f);

        this.transform.DOLocalPath(wayPoints, 8f, PathType.CatmullRom, PathMode.TopDown2D)
            .SetLookAt(0f)
            .SetEase(Ease.InOutCubic);        
    }

    void PlaySatelliteAnim()
    {
        this.transform.DOLocalMoveX(7f, 60f).SetEase(Ease.Linear);
        // this.transform.DOPunchScale(new Vector3(-0.05f, -0.05f, 0), 20f, 0, 0f)
        // .SetLoops(-1, LoopType.Restart)
        // .SetEase(Ease.Linear);
    }

    void PlayAlienAnim()
    {
        float offsetX = 8f;


        Vector3[] wayPoints_1 = new Vector3[3];
        Vector3 end_1 = new Vector3(spawnPos.x + offsetX, spawnPos.y + 2f, 0f);
        wayPoints_1[0] = end_1;
        wayPoints_1[1] = new Vector3(spawnPos.x + 0.5f, spawnPos.y, 0f);
        wayPoints_1[2] = new Vector3(end_1.x, end_1.y - 1f, 0f);


        Sequence alienAnimSeq = DOTween.Sequence().SetId("alienAnimSeq");
        alienAnimSeq.SetLink(this.gameObject);
        Tween mov_in = this.transform.DOLocalPath(wayPoints_1, 3f, PathType.CubicBezier, PathMode.TopDown2D)
            .SetEase(Ease.OutQuart);
        Tween suspend = this.transform.DOLocalMoveY(end_1.y + 0.2f, 1f)
            .SetLoops(4, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);

        Vector3 end_2 = new Vector3(end_1.x + 6f, end_1.y + 3f, 0f);
        Vector3[] wayPoints_2 = new Vector3[3];
        wayPoints_2[0] = end_2;
        wayPoints_2[1] = new Vector3(end_1.x + 1, end_1.y, 0f);
        wayPoints_2[2] = new Vector3(end_2.x, end_2.y - 1, 0f);

        Tween mov_out = this.transform.DOLocalPath(wayPoints_2, 1f, PathType.CubicBezier, PathMode.TopDown2D)
            .SetEase(Ease.InExpo);

        alienAnimSeq.Append(mov_in);
        alienAnimSeq.AppendCallback(ActiveHelloImg);
        alienAnimSeq.Append(suspend);
        alienAnimSeq.AppendCallback(InactiveHelloImg);
        alienAnimSeq.Append(mov_out);
    }

    void ActiveHelloImg()
    {
        helloGO.SetActive(true);
        helloGO.GetComponent<SpriteRenderer>().DOFade(255, 0.4f);
    }

    void InactiveHelloImg()
    {
        helloGO.SetActive(false);
    }






























}
