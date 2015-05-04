using System;
using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

namespace SKJZ
{
    namespace Core
    {
        /// <summary>
        /// 单例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Singleton<T>  where T : new()
        {
            protected static T _instance;
            private static readonly object _lockObj;

            static Singleton()
            {
                _lockObj = new object();
            }

            public static T Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        lock (_lockObj)
                        {
                            if (_instance == null)
                            {
                                //如果是引用类型创建一个T实例，如果是值类型返回值的默认值
                                _instance = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
                            }
                        }
                    }

                    return _instance;
                }
            }
        }
    }
}



//代码分析

/*=========================================================default(T)========================================================
泛型代码中的默认关键字（C# 编程指南）
在泛型类和泛型方法中产生的一个问题是，在预先未知以下情况时，如何将默认值分配给参数化类型 T：  

    T 是引用类型还是值类型。  

    如果 T 为值类型，则它是数值还是结构。  

给定参数化类型 T 的一个变量 t，只有当 T 为引用类型时，语句 t = null 才有效；只有当 T 为数值类型而不是结构时，
语句 t = 0 才能正常使用。  解决方案是使用 default 关键字，此关键字对于引用类型会返回 null，对于数值类型会返回零。
对于结构，此关键字将返回初始化为零或 null 的每个结构成员，具体取决于这些结构是值类型还是引用类型。
对于可以为 null 的值类型，默认返回 System.Nullable<T>，它像任何结构一样初始化。

default keyword will return null for reference types and zero for numeric value types. 

For structs, it will return each member of the struct initialized to zero or null depending on whether they are value or reference types.

{
    class Foo
    {
        public string Bar { get; set; }
    }

    struct Bar
    {
        public int FooBar { get; set; }
        public Foo BarFoo { get; set; }
    }

    public class AddPrinterConnection
    {
        public static void Main()
        {

            int n = default(int);
            Foo f = default(Foo);
            Bar b = default(Bar);

            Console.WriteLine(n);

            if (f == null) Console.WriteLine("f is null");

            Console.WriteLine("b.FooBar = {0}",b.FooBar);

            if (b.BarFoo == null) Console.WriteLine("b.BarFoo is null");

        }
    }
{
  
 * output
0
f is null
b.FooBar = 0
b.BarFoo is null
*/


/*========================================================where T : new()=======================================================
 * where T : new() is a constraint which means that type argument T must have public parameterless constructor.
*/


/*========================================================Activator.CreateInstance (Type)=======================================================
Activator.CreateInstance (Type)实例
输入一个类的名称为参数,返回一个相应的类的实例。
这在工厂模式中是非常有用的，这样，可以使程序有更高的扩展性 
*/



/*========================================================ILSpy=======================================================
 * ILSpy is the open-source .NET assembly browser and decompiler.
 * http://ilspy.net/
*/



/*========================================================http://www.dotnetperls.com=======================================================
 * http://www.dotnetperls.com/
 * 代码示例
*/



/*========================================================lock=======================================================
 * lock  关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。  如果其他线程尝试进入锁定的代码，则它将一直等待（即被阻止），直到该对象被释放。 
*/



/*========================================================readonly=======================================================
 * readonly  关键字与 const 关键字不同。  const  字段只能在该字段的声明中初始化。 readonly  字段可以在声明或构造函数中初始化。
 * 因此，根据所使用的构造函数，readonly 字段可能具有不同的值。 另外，const 字段为编译时常数，而 readonly 字段可用于运行时常数 
*/