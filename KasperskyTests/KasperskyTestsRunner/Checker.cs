using System;
using System.Collections.ObjectModel;
using System.Text;
using KasperskyTests;

namespace KasperskyTestsRunner
{
    class Checker
    {
        public void PerformCheck()
        {
            TestOne_Check();
            TestTwo_Check();
            Console.ReadKey();
        }

        private void TestOne_Check()
        {
            object pushedObject1 = 1;
            object pushedObject2 = 2;
            object pushedObject3 = 3;
            object pushedObject4 = 4;
            object poppedObject1 = null;
            object poppedObject2 = null;
            object poppedObject3 = null;
            object poppedObject4 = null;
            TestOne<object> queue = new TestOne<object>();

            Console.WriteLine("Starting TestOne");

            Action<int> threadSleep = time =>
                {
                    Console.WriteLine("TestOne: thread sleeps for: {0}", time);
                    System.Threading.Thread.Sleep(time);
                };

            Action<object> queuePush = obj =>
                {
                    Console.WriteLine("TestOne: pushing object into queue: {0}", obj);
                    queue.Push(obj);   
                };

            Func<object> queuePop = () =>
            {
                Console.WriteLine("TestOne: requesting pop from queue");
                object obj = queue.Pop();
                Console.WriteLine("TestOne: object popped from queue:  {0}", obj);
                return obj;
            };

            System.Threading.ParameterizedThreadStart a1 = obj =>
            {
                queuePush(pushedObject1);
                queuePush(pushedObject2);
            };

            System.Threading.ParameterizedThreadStart a2 = obj =>
            {
                poppedObject1 = queuePop();
            };

            System.Threading.ParameterizedThreadStart a3 = obj =>
            {
                poppedObject2 = queuePop();
            };

            System.Threading.ParameterizedThreadStart a4 = obj =>
            {
                poppedObject3 = queuePop();
            };

            System.Threading.ParameterizedThreadStart a5 = obj =>
            {
                poppedObject4 = queuePop();
            };

            System.Threading.ParameterizedThreadStart a6 = obj =>
            {
                queuePush(pushedObject3);
            };

            System.Threading.ParameterizedThreadStart a7 = obj =>
            {
                queuePush(pushedObject4);
            };

            new System.Threading.Thread(a1).Start();
            new System.Threading.Thread(a2).Start();
            new System.Threading.Thread(a3).Start();

            threadSleep(50);
            new System.Threading.Thread(a4).Start();
            new System.Threading.Thread(a5).Start();

            threadSleep(50);
            new System.Threading.Thread(a6).Start();
            threadSleep(50);
            new System.Threading.Thread(a7).Start();
            threadSleep(50);

            Console.WriteLine("TestOne: poppedObject1 is: {0}", poppedObject1);
            Console.WriteLine("TestOne: poppedObject2 is: {0}", poppedObject2);
            Console.WriteLine("TestOne: poppedObject3 is: {0}", poppedObject3);
            Console.WriteLine("TestOne: poppedObject4 is: {0}", poppedObject4);

            Console.WriteLine("Ending TestOne");
        }

        private void TestTwo_Check()
        {
            Collection<int> collection = new Collection<int> { 1, -1, 2, 3, 3, 7, 3, 8, 6, 7, 5, 5, 11 };
            int givenNumber = 10;
            Console.WriteLine("Starting TestTwo");

            Console.WriteLine("TestTwo: input single number is: {0}", givenNumber);
           
            StringBuilder sb = new StringBuilder();
            foreach (int colItem in collection)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(colItem);
            }
            Console.WriteLine("TestTwo: input collection items are: {0}", sb.ToString());
            
            TestTwo testTwoObject = new TestTwo();

            var result = testTwoObject.GetPairs(collection, givenNumber);

            sb = new StringBuilder();
            foreach (Tuple<int, int> colItem in result)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(string.Format("({0}, {1})", colItem.Item1, colItem.Item2));
            }
            Console.WriteLine("TestTwo: output pairs are: {0}", sb.ToString());
            Console.WriteLine("Ending TestTwo");
        }
    }
}
