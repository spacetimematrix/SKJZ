using UnityEngine;
using System.Collections;
using System;

[Serializable, RequireComponent(typeof(MeshFilter))]
public class PageCurl : MonoBehaviour 
{   
    //点坐标集合
    Vector3[] v0;
    //锥顶的位置

    public float Y;
    Vector3 apex;
    //母线偏离中轴线的角度
    public float theta;
    //在锥形横截面上的旋转角度
    public float rho;


    void Init()
    {
        theta = 0;
        apex = new Vector3(0f, Y, 0f);
    }


    public PageCurl()
    {
        Init();
    }

    void Awake()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        v0 = mesh.vertices;
    }


    public Vector3 curlTurn(Vector3 p)
    {
        float R = Mathf.Sqrt(Mathf.Pow(p.x, 2) + Mathf.Pow(p.y - apex.y, 2));
        float r = R * Mathf.Sin(theta);
        float bta = Mathf.Asin(p.x / R) / Mathf.Sin(theta);

        float x = r * Mathf.Sin(bta);
        float y = R + apex.y - r * (1 - Mathf.Cos(bta)) * Mathf.Sin(theta);
        float z = r * (1 - Mathf.Cos(bta)) * Mathf.Cos(theta);

        return new Vector3(x , y, z );
    }


    public void LateUpdate()
    {
        apex = new Vector3(0, Y, 0);

        if (v0.Length > 0)
        {
            renderMesh();
        }
    }


    public void renderMesh()
    {
        Vector3[] a = new Vector3[v0.Length];
        for (int i = 0; i < v0.Length; i++)
        {
            a[i] = curlTurn(v0[i]);
        }
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = a;
        mesh.RecalculateNormals();

        transform.localEulerAngles = new Vector3(0, rho, 0);
    }

 

}
