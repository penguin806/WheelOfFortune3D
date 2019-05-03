using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour
{
    public Font customCubeSurfaceFont;

    public void CreateCubeAngle15()
    {
        // The script will be attached to the GameObject <Sphere>
        // at the origin point
        Vector3 centerPos = this.transform.position;
        float radius = 10;
        int angle;
        string[] myClassmateNames = new string[24] {
            "伍芳兰", "张敏", "张玥", "杨涵", "申波", "杨名",
            "阙继婷", "李昌和", "薛有缘", "王位", "陈泽南", "尹红爱",
            "曾伟康", "王帅旗", "刘伟康", "王端举", "李学锋", "阮书琪",
            "童方超", "李亚军", "张露云", "刘湘川", "任海清", "易智隆"
        };

        // Generate a cube every 15 degrees
        for (angle = 0; angle < 360; angle += 15)
        {
            // Calculate the position of the cube   
            float x = centerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);
            float y = centerPos.y + radius * Mathf.Sin(angle * 3.14f / 180f);

            GameObject myCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            myCube.name = (angle / 15).ToString();
            myCube.transform.position = new Vector3(x, centerPos.z, y);
            myCube.transform.SetParent(this.transform);

            Renderer render = myCube.GetComponent<Renderer>();
            //render.material.SetColor("_Color", Color.blue);
            render.material.SetColor("_Color", 
                new Color(80f / 255f, 140f / 255f, 200f / 255f));

            GameObject surfaceText = new GameObject();
            surfaceText.name = "SurfaceText";
            surfaceText.transform.position = myCube.transform.position + 
                new Vector3(-0.01f, 0.5f, 0.0f);
            surfaceText.transform.SetParent(myCube.transform);

            TextMesh surfaceTextMesh = surfaceText.AddComponent<TextMesh>();
            surfaceTextMesh.font = this.customCubeSurfaceFont;
            surfaceTextMesh.text = (angle / 15 + 1).ToString() +
                "\n" + myClassmateNames[angle / 15];
            // Debug.Log(surfaceTextMesh.text);

            surfaceTextMesh.fontSize = 100;
            surfaceTextMesh.alignment = TextAlignment.Center;
            surfaceTextMesh.anchor = TextAnchor.MiddleCenter;
            surfaceTextMesh.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
            surfaceTextMesh.transform.localEulerAngles = new Vector3(90, 0, 90);

            // IMPORTANT! Without material, the font cannot display normally
            MeshRenderer surfaceTextMeshRender = surfaceText.GetComponent<MeshRenderer>();
            surfaceTextMeshRender.material = this.customCubeSurfaceFont.material;

            // Todo: Rotate the cubes
            //var rot = Quaternion.FromToRotation(Vector3.forward, centerPos - new Vector3(x, centerPos.z, y));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateCubeAngle15();
        //float radius = 20f;
        //for (int i = 1; i <= 24; i++)
        //{
        //    float angle = i * Mathf.PI * 2f / 24;
        //    Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        //    GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), newPos, Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
