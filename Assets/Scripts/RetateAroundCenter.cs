using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetateAroundCenter : MonoBehaviour
{
    private bool isSpinning;
    private float[] cubesAngles;
    private float startAngle;
    private float finalAngle;
    private float currentLerpRotationTime;

    private int playedTimes;
    private Material lastTimeSelectedCubeMaterial;

    public GameObject centerSphere;

    // Start is called before the first frame update
    void Start()
    {
        this.isSpinning = false;

        // Fill the necessary angles
        this.cubesAngles = new float[] {
            0, 15, 30, 45, 60, 75, 90,
            105, 120, 135, 150, 165, 180,
            195, 210, 225, 240, 255, 270,
            285, 300, 315, 330, 345 };

        this.startAngle = 0;
        this.finalAngle = 0;
        this.currentLerpRotationTime = 0;
        this.playedTimes = 0;
        this.lastTimeSelectedCubeMaterial = null;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.RotateAround(centerCube.transform.position, 
        //    new Vector3(0.0f,1.0f,0.0f), 100*Time.deltaTime);

        if (false == this.isSpinning)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.StartRotation();
                this.isSpinning = true;
                if (0 != this.playedTimes && null != this.lastTimeSelectedCubeMaterial)
                {
                    this.lastTimeSelectedCubeMaterial.SetColor("_Color",
                new Color(80f / 255f, 140f / 255f, 200f / 255f));
                }
                return;
            }
            else
            {
                return;
            }
        }

        float maxLerpRotationTime = 4f;
        // Increment timer once per frame
        this.currentLerpRotationTime += Time.deltaTime;
        if(this.currentLerpRotationTime > maxLerpRotationTime || 
            this.centerSphere.transform.eulerAngles.z == this.finalAngle)
        {
            // Stop spinning
            this.currentLerpRotationTime = maxLerpRotationTime;
            this.isSpinning = false;
            this.startAngle = this.finalAngle % 360;
            this.PickClassmateByAngle();
            this.playedTimes++;
        }

        // Calculate current position using linear interpolation
        float t = this.currentLerpRotationTime / maxLerpRotationTime;
        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float currentRotationAngle = Mathf.Lerp(this.startAngle, this.finalAngle, t);
        this.centerSphere.transform.eulerAngles = new Vector3(0, currentRotationAngle, 0);
        //Debug.Log(this.startAngle);
        //Debug.Log(this.finalAngle);
    }

    private void StartRotation()
    {
        this.currentLerpRotationTime = 0f;

        int fullRotationTimes = 5;
        float randomLastTimeRotationAngle = this.cubesAngles[
                UnityEngine.Random.Range(0, this.cubesAngles.Length)
            ];

        this.finalAngle = -(360 * fullRotationTimes + randomLastTimeRotationAngle);
        this.isSpinning = true;
    }

    void PickClassmateByAngle()
    {
        string[] myClassmateNames = new string[24] {
            "伍芳兰", "张敏", "张玥", "杨涵", "申波", "杨名",
            "阙继婷", "李昌和", "薛有缘", "王位", "陈泽南", "尹红爱",
            "曾伟康", "王帅旗", "刘伟康", "王端举", "李学锋", "阮书琪",
            "童方超", "李亚军", "张露云", "刘湘川", "任海清", "易智隆"
        };

        int whichClassmate = (Mathf.RoundToInt(this.startAngle) + 360) % 360 / 15;
        GameObject classmateCube = this.transform.Find(
            whichClassmate.ToString()).gameObject;

        Renderer classmateCubeRender = classmateCube.GetComponent<Renderer>();
        this.lastTimeSelectedCubeMaterial = classmateCubeRender.material;
        classmateCubeRender.material.SetColor("_Color",
                new Color(255f / 255f, 120f / 255f, 255f / 255f));
    }
}
