﻿using System;
using System.Text;

namespace 委托
{
    class Program
    {
        // 定义委托，并引用一个方法，这个方法需要获取一个int型参数返回void
        internal delegate void Feedback(int value);
        static void Main(string[] args)
        {
            #region Clr C#
            //Program p = new Program();
            //StaticDelegateDemo();
            //InstanceDelegateDemo();
            //ChainDelegateDemo(p);
            //ChainDelegateDemo2(p);
            //Console.WriteLine("Hello World!");
            //string[] names = { "Jeff", "Jee", "aa", "bb" };
            //char find = 'e';
            //names= Array.FindAll(names, name => name.IndexOf(find) >= 0);
            //Array.ForEach(names, Console.WriteLine);
            #endregion

            #region C# In Depth

            Person jon = new Person("jon");
            Person tom = new Person("tom");
            // 声明委托实例
            StringProcessor jonVoice, tomVoice, background;
            jonVoice = new StringProcessor(jon.Say);
            tomVoice = new StringProcessor(tom.Say);
            background = new StringProcessor(Background.Note);
            jonVoice += tomVoice;
            // 调用委托实例
            jonVoice("hello"); // 简式调用
            //tomVoice.Invoke("world"); // 显示调用
            //background("back");
            #endregion

            Test test = new Test();
            test.Name = "ss";
            //Test test2 = test;
            Console.WriteLine(test.Name);
            //test2.Name = "sdsds";
            Console.WriteLine(test.Name);
            StringBuilder sb = new StringBuilder();
            sb.Append("a");
            Console.WriteLine(sb.ToString());
            //Console.WriteLine(test2.Name);
            Program.GetClone(test,sb);
            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }

        public static void GetClone(Test test,StringBuilder sb)
        {
            sb.Append("b");
            Console.WriteLine(test.Name);
        }
        

        #region Clr C#
        /// <summary>
        /// 静态调用
        /// </summary>
        private static void StaticDelegateDemo()
        {
            Console.WriteLine("---------委托调用静态方法------------");
            Counter(1, 10, null);
            //Counter(1, 10, new Feedback(FeedbackToConsole));
            //Counter(1, 10, FeedbackToConsole);
            Counter(1, 10, value => Console.WriteLine(value));

        }

        /// <summary>
        /// 实例调用
        /// </summary>
        private static void InstanceDelegateDemo()
        {
            Console.WriteLine("---------委托调用实例方法------------");
            Program p = new Program();
            Counter(1, 10, null);
            Counter(1, 5, new Feedback(p.InstanceFeedbackToConsole));
        }

        /// <summary>
        /// 委托链调用 1
        /// </summary>
        /// <param name="p"></param>
        private static void ChainDelegateDemo(Program p)
        {
            Console.WriteLine("---------委托链调用1------------");
            Feedback fb1 = new Feedback(FeedbackToConsole);
            Feedback fb2 = new Feedback(p.InstanceFeedbackToConsole);
            Feedback fbChain = null;
            fbChain = (Feedback)Delegate.Combine(fbChain, fb1);
            fbChain = (Feedback)Delegate.Combine(fbChain, fb2);
            Counter(1, 3, fbChain);
            Console.WriteLine();
            fbChain = (Feedback)Delegate.Remove(fbChain, new Feedback(FeedbackToConsole));
            Counter(1, 3, fbChain);
        }

        /// <summary>
        /// 委托链调用 2
        /// </summary>
        /// <param name="p"></param>
        private  static void ChainDelegateDemo2(Program p)
        {
            Console.WriteLine("---------委托链调用2------------");
            Feedback fb1 = new Feedback(FeedbackToConsole);
            Feedback fb2 = new Feedback(p.InstanceFeedbackToConsole);
            Feedback fbChain = null;
            fbChain += fb1;
            fbChain += fb2;
            Counter(1, 3, fbChain);
            Console.WriteLine();
            fbChain -= new Feedback(FeedbackToConsole);
            Counter(1, 2, fbChain);
        }
        /// <summary>
        /// 使用此方法触发委托回调
        /// </summary>
        /// <param name="from">开始</param>
        /// <param name="to">结束</param>
        /// <param name="fb">委托引用</param>
        private static void Counter(int from,int to, Feedback fb)
        {
            for (int val = from; val <= to; val++)
            {
                // fb不为空，则调用回调方法
                if (fb != null)
                {
                    fb(val);
                }
                //fb?.Invoke(val); 简化版本调用
            }
        }

        /// <summary>
        /// 静态回调方法
        /// </summary>
        /// <param name="value"></param>
        private static void FeedbackToConsole(int value)
        {
            // 依次打印数字
            Console.WriteLine("Item=" + value);
        }
        /// <summary>
        /// 实例回调方法
        /// </summary>
        /// <param name="value"></param>
        private void InstanceFeedbackToConsole(int value)
        {
            Console.WriteLine("Item=" + value);
        }
        #endregion


        #region C# In Depth

        // 1.声明委托类型
        delegate void StringProcessor(string input);

        class Person
        {
            string name;
            public Person(string name)
            {
                this.name = name;
            }
            // 2.声明兼容的实例方法
            public void Say(string message)
            {
                Console.WriteLine($"{name} say: {message}");
            }
        }

        class Background
        {
            // 3.声明兼容的静态方法
            public static void Note(string note)
            {
                Console.WriteLine($"({note})");
            }
        }
        #endregion

    }
    public class Test
    {
        public string Name { get; set; }
    }
}
