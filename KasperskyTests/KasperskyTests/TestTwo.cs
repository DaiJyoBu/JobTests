using System;
using System.Collections.Generic;

namespace KasperskyTests
{
    public class TestTwo
    {
        //2. Есть коллекция чисел и отдельное число Х. Надо вывести все пары чисел, которые в сумме равны заданному Х.

        public IEnumerable<Tuple<int, int>> GetPairs(
            ICollection<int> numbers, int givenNumber)
        {
            List<Tuple<int, int>> result =
                new List<Tuple<int, int>>();

            if (numbers == null || numbers.Count < 2)
                return result;

            Dictionary<int, int> numbersCount =
                new Dictionary<int, int>();

            foreach (int curNumber in numbers)
            {
                try
                {
                    int secondSumArgument = checked(givenNumber - curNumber);
                    if (numbersCount.ContainsKey(secondSumArgument))
                    {
                        Tuple<int, int> newPair = new Tuple<int, int>(secondSumArgument, curNumber);
                        result.Add(newPair);

                        if (numbersCount[secondSumArgument] == 1)
                            numbersCount.Remove(secondSumArgument);
                        else
                            numbersCount[secondSumArgument] = numbersCount[secondSumArgument] - 1;
                        continue;
                    }
                }
                catch (OverflowException)
                {
                }


                if (!numbersCount.ContainsKey(curNumber))
                {
                    numbersCount.Add(curNumber, 1);
                }
                else
                {
                    numbersCount[curNumber] = numbersCount[curNumber] + 1;
                }
            }

            return result;
        }
    }
}
