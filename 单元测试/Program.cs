using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 单元测试
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class StringSample
    {
        public StringSample(string init)
        {
            if (init == null)
                throw new ArgumentNullException("init");
            this.init = init;
        }
        private string init;
        public string GetStringDemo(string first, string second)
        {
            if (first == null)
                throw new ArgumentNullException("first");
            if (string.IsNullOrEmpty(first))
                throw new ArgumentException("empty string is not allowed", first);
            if (second == null)
                throw new ArgumentNullException("second");
            if (second.Length > first.Length)
                throw new ArgumentOutOfRangeException("second",
                "must be shorter than first");
            int startIndex = first.IndexOf(second);
            if (startIndex < 0)
            {
                return string.Format("{0} not found in {1}", second, first);
            }
            else if (startIndex < 5)
            {
                return string.Format("removed {0} from {1}: {2}", second, first,
                first.Remove(startIndex, second.Length));
            }
            else
            {
                return init.ToUpperInvariant();
            }
        }
    }

    public class DeepThought
    {
        public int TheAnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything()
        {
            return 42;
        }
    }
}
