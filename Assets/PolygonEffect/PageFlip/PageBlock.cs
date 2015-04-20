using UnityEngine;
using System.Collections;

public class PageBlock : MonoBehaviour {

    // Fields
    public int[] _triangles;
    public Vector3[] _vertices;
    public bool normalize;
    public int resolutionDepth;
    public int resolutionHeight;
    public int resolutionWidth;

    // Methods
    /* private scope */ void Initializer()
    {
        this.resolutionWidth = 5;
        this.resolutionHeight = 6;
        this.resolutionDepth = 1;
        this.normalize = true;
    }

    public PageBlock()
    {
        this.Initializer();
    }

    public void createMesh(int resH, int resW, int resD)
    {
        int num23;
        int num24;
        int num25;
        int num26;
        int num = resH + 1;
        int num2 = resW + 1;
        int num3 = resD + 1;
        int num4 = (num * num2) * num3;
        Vector3[] array = new Vector3[num4];
        Vector2[] vectorArray2 = new Vector2[num4];
        Color[] colorArray = new Color[num4];
        float num5 = !this.normalize ? 1f : (1f / ((float) Mathf.Max(resW, resH)));
        Debug.Log((((((((("resH=" + resH) + " resW=") + resW) + " resD=") + resD) + " numVertices=") + num4) + " scalingFactor=") + num5);
        int index = 0;
        for (int i = 0; i < num3; i++)
        {
            if ((i == 0) || (i == (num3 - 1)))
            {
                for (int j = 0; j < num; j++)
                {
                    for (int k = 0; k < num2; k++)
                    {
                        float x = k;
                        float z = j;
                        float y = i * -1;
                        Vector3 vector = new Vector3(x, y, z);
                        array[index] = (Vector3) (vector * num5);
                        float num13 = (i != 0) ? (1f - (vector.x / ((float) (resW * 2)))) : (vector.x / ((float) (resW * 2)));
                        float num14 = vector.z / ((float) resH);
                        vectorArray2[index] = new Vector2(num13, num14);
                        colorArray[index] = Color.Lerp(Color.red, Color.green, vector.y);
                        index++;
                    }
                }
            }
        }
        this._vertices = array;
        int num15 = 0;
        int num16 = 0;
        int num17 = 0;
        int num18 = 0;
        int num19 = resW;
        int num20 = ((resH * resW) * 2) + (((resH * 2) + (resW * 2)) * resD);
        int num21 = num20 * 2;
        int[] numArray = new int[num21 * 6];
        Debug.Log((((((((("resH=" + resH) + " resW=") + resW) + " resD=") + resD) + " numTriangles=") + num21) + " newTriangles=") + numArray.Length);
        int num22 = 0;
        for (num23 = 0; num23 < (resH * resW); num23++)
        {
            num24 = num23 / num19;
            num25 = num23 % num19;
            num15 = (num24 * num2) + num25;
            num16 = num15 + 1;
            num17 = ((num24 + 1) * num2) + num25;
            num18 = num17 + 1;
            numArray[num22 + 0] = num16;
            numArray[num22 + 1] = num15;
            numArray[num22 + 2] = num17;
            numArray[num22 + 3] = num16;
            numArray[num22 + 4] = num17;
            numArray[num22 + 5] = num18;
            num22 += 6;
        }
        for (num23 = 0; num23 < resW; num23++)
        {
            num25 = num23;
            num26 = num * num2;
            num15 = num26 + num23;
            num16 = num15 + 1;
            num17 = num25;
            num18 = num17 + 1;
            numArray[num22 + 0] = num16;
            numArray[num22 + 1] = num15;
            numArray[num22 + 2] = num17;
            numArray[num22 + 3] = num16;
            numArray[num22 + 4] = num17;
            numArray[num22 + 5] = num18;
            num22 += 6;
        }
        for (num23 = 0; num23 < resH; num23++)
        {
            num24 = num23;
            num26 = (num * num2) + resW;
            num15 = num26 + (num24 * num2);
            num16 = num15 + num2;
            num17 = (num24 * num2) + resW;
            num18 = num17 + num2;
            numArray[num22 + 0] = num16;
            numArray[num22 + 1] = num15;
            numArray[num22 + 2] = num17;
            numArray[num22 + 3] = num16;
            numArray[num22 + 4] = num17;
            numArray[num22 + 5] = num18;
            num22 += 6;
        }
        for (num23 = resW - 1; num23 >= 0; num23--)
        {
            num25 = num23;
            num26 = (num * num2) + (num2 * resH);
            num15 = num26 + num23;
            num16 = num15 + 1;
            num17 = (num2 * resH) + num25;
            num18 = num17 + 1;
            numArray[num22 + 0] = num15;
            numArray[num22 + 1] = num16;
            numArray[num22 + 2] = num18;
            numArray[num22 + 3] = num15;
            numArray[num22 + 4] = num18;
            numArray[num22 + 5] = num17;
            num22 += 6;
        }
        for (num23 = resH - 1; num23 >= 0; num23--)
        {
            num24 = num23;
            num26 = num * num2;
            num15 = num26 + ((num24 + 1) * num2);
            num16 = num26 + (num24 * num2);
            num17 = (num24 + 1) * num2;
            num18 = num24 * num2;
            numArray[num22 + 0] = num16;
            numArray[num22 + 1] = num15;
            numArray[num22 + 2] = num17;
            numArray[num22 + 3] = num16;
            numArray[num22 + 4] = num17;
            numArray[num22 + 5] = num18;
            num22 += 6;
        }
        for (num23 = 0; num23 < (resH * resW); num23++)
        {
            int num27 = num * num2;
            num24 = num23 / num19;
            num25 = num23 % num19;
            num16 = (num27 + (num24 * num2)) + num25;
            num15 = num16 + 1;
            num18 = (num27 + ((num24 + 1) * num2)) + num25;
            num17 = num18 + 1;
            numArray[num22 + 0] = num16;
            numArray[num22 + 1] = num15;
            numArray[num22 + 2] = num17;
            numArray[num22 + 3] = num16;
            numArray[num22 + 4] = num17;
            numArray[num22 + 5] = num18;
            num22 += 6;
        }
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = array;
        mesh.colors = colorArray;
        mesh.uv = vectorArray2;
        mesh.triangles = numArray;
        mesh.RecalculateNormals();
        this._triangles = numArray;
    }

    public void initialize()
    {
        this.createMesh(this.resolutionHeight, this.resolutionWidth, this.resolutionDepth);
    }

    public void Main()
    {
    }

    public void Start()
    {
        this.initialize();
    }

}
