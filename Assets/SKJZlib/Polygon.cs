using UnityEngine;
using System.Collections;

namespace SKJZ
{
    namespace lib
    {
        public struct Point { public float x, y;}

        public class Polygon
        {
            /// <summary>
            /// 使用行列式公式求多边形面积
            /// 多边形各点的集合 vertices(x1,y1),...,(xn,yn);
            /// A = 1/2 * (x1y2 - x2y1) + (x2y3 - x3y2) + ... + (xny1 - x1yn)
            ///                     -    -       -     -    -
            ///         1    | x1   x2   x3     xN-1   xN   x1 |
            /// area = --- * |    ×    ×    ...      ×    ×    |
            ///         2    | y1   y2   y3     yN-1   yN   y1 |
            ///                      +    +      +      +    +
            /// 右下斜线相乘取正号，左下斜线相乘取符号，然后统统加起来，除以二
            /// 如果是逆时针旋转，求出来为正值
            /// 如果是顺时针旋转，求出来为负值，必须再取绝对值
            /// 一般来说我们会让多边形顶点顺序为逆时针顺序，以求得正值
            /// </summary>
            /// <param name="pointArr"></param>
            public static float Area(Point[] pointArr)
            {
                float area = default(float);

                for(int i = pointArr.Length -1, j = 0; j < pointArr.Length; i = j++ )
                {
                    area += pointArr[i].x * pointArr[j].y;
                    area -= pointArr[j].x * pointArr[i].y;
                }

                return area *= 0.5f;
            }
        }

    }
}


