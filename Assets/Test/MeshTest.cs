using UnityEngine;
using System.Collections;




public class MeshTest : MonoBehaviour 
{
    public Texture2D texture2d;

	void Start () 
    {
        PlaneData planeData = new PlaneData();
        planeData.Width = 5.12f;
        planeData.Hight = 2.56f;
        planeData.widthSegments = 50;
        planeData.hightSegments = 50;
        planeData.PlaneName = "onePlane";

        Plane plane = new Plane(planeData);
        plane.PlaneGameobject.GetComponent<MeshRenderer>().material.mainTexture = texture2d;
        plane.PlaneGameobject.AddComponent<PageCurl>();
	}


    //绘制一个包含四个顶点的面片，需要:
    //1、四个顶点的坐标数组
    //2、三角形顶点的顺序排列的数组
    //3、每个顶点的法线的数组
    //4、每个顶点在0-1坐标范围内的信息的数组
    //5、需要MeshFiler组件
    //6、需要MeshRender组件
    //http://docs.unity3d.com/Manual/Example-CreatingaBillboardPlane.html
    void CreatePlane()
    {
        //plane宽度
        float width = 50f;
        //plane高度
        float hight = 50f;
        //顶点坐标数组
        Vector3[] newVertices;
        //三角形顶点的顺序排列的数组
        int[] newTriangles;
        //每个顶点的法线的数组
        Vector3[] newNormals;
        //每个顶点在0-1坐标范围内的信息的数组
        Vector2[] newUV;


        //创建一个GameObject
        GameObject meshObj = new GameObject("plane");
        meshObj.transform.localPosition = Vector3.zero;
        meshObj.transform.localScale = Vector3.one;

        //添加MeshFilter组件
        MeshFilter meshFilter = meshObj.AddComponent<MeshFilter>();

        //添加MeshRenderer组件
        MeshRenderer meshRender = meshObj.AddComponent<MeshRenderer>();

        //设置一个shader
        meshRender.material = new Material(Shader.Find("Sprites/Default"));

        //创建一个Mesh
        Mesh mesh = new Mesh();

        //设置点的数组
        newVertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, hight , 0),
            new Vector3(width, hight, 0)
        };

        //一个四边形由两个三角形构建,两个三角形由点构建时的点的顺序
        //2 +          2 +--+ 3
        //  |\            \ |  
        //  | \            \| 
        //0 +--+ 1          + 1 

        //0 -> 1 -> 2
        //2 -> 1 -> 3

        newTriangles = new int[6];
        newTriangles[0] = 0;
        newTriangles[1] = 1;
        newTriangles[2] = 2;
        newTriangles[3] = 2;
        newTriangles[4] = 1;
        newTriangles[5] = 3;


        //四个顶点的法线
        newNormals = new Vector3[4]
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward
        };


        //四个顶点在UV坐标系中的位置 
        newUV = new Vector2[4] 
        {
           new Vector2(0, 0),
           new Vector2(1, 0),
           new Vector2(0, 1),
           new Vector2(1, 1)
        };


        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

        meshFilter.mesh = mesh;
    }

}


public class PlaneData
{
    //plane宽度
    public float Width = 1f;

    //plane高度
    public float Hight = 1f;

    //宽度分段
    public int widthSegments = 1;

    //高度分段
    public int hightSegments = 1;

    //面片的名字
    public string PlaneName = "plane";
}

public class Plane
{
    PlaneData PlaneData;
    
    public GameObject PlaneGameobject;

    public Plane(PlaneData planeData)
    {
        PlaneData = planeData;

        PlaneGameobject = CreatePlane();
    }

    public GameObject GetPlaneGameobject()
    {
        return PlaneGameobject;
    }


    GameObject CreatePlane()
    {
        int widthCount = PlaneData.widthSegments + 1;
        //高上有的点数
        int hightCount = PlaneData.hightSegments + 1;
        //总共的三角形点数
        int numTriangles = PlaneData.widthSegments * PlaneData.hightSegments * 6;
        //总共的点数
        int numVertices = widthCount * hightCount;

        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uvs = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];
        Vector3[] newNormals = new Vector3[numVertices];

        float uvFactorX = 1.0f / PlaneData.widthSegments;
        float uvFactorY = 1.0f / PlaneData.hightSegments;
        float scaleX = PlaneData.Width / PlaneData.widthSegments;
        float scaleY = PlaneData.Hight / PlaneData.hightSegments;

        //构建顶点数组 UV数组 法线数组
        for (int x = 0, index = 0; x < widthCount; x++)
        {
            for (int y = 0; y < hightCount; y++)
            {
                vertices[index] = new Vector3(x * scaleX, y * scaleY, 0.0f);

                uvs[index] = new Vector2(x * uvFactorX, y * uvFactorY);

                newNormals[index++] = Vector3.back;
            }
        }

        //构建点的顺序的数组
        for (int x = 0, index = 0; x < PlaneData.widthSegments; x++)
        {
            for (int y = 0; y < PlaneData.hightSegments; y++)
            {
                triangles[index] = x * widthCount + y;
                triangles[index + 1] = x * widthCount + 1 + y;
                triangles[index + 2] = (x + 1) * widthCount + y;
                triangles[index + 3] = (x + 1) * widthCount + y;
                triangles[index + 4] = x * widthCount + 1 + y;
                triangles[index + 5] = (x + 1) * widthCount + y + 1;

                index += 6;
            }
        }

        //创建一个GameObject
        GameObject meshObj = new GameObject(PlaneData.PlaneName);
        meshObj.transform.localPosition = Vector3.zero;
        meshObj.transform.localScale = Vector3.one;

        //添加MeshFilter组件
        MeshFilter meshFilter = meshObj.AddComponent<MeshFilter>();

        //添加MeshRenderer组件
        MeshRenderer meshRender = meshObj.AddComponent<MeshRenderer>();

        //设置一个shader
        meshRender.material = new Material(Shader.Find("Sprites/Default"));

        //创建一个Mesh
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;

        return meshObj;
    }

}
