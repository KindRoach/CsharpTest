using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons1 ={
                                    new Person
                                    {
                                        FirstName="Michael",
                                        LastName="Jackson"
                                    },
                                    new Person
                                    {
                                        FirstName="Michael2",
                                        LastName="Jackson2"
                                    },
                                    new Person
                                    {
                                        FirstName="Michael3",
                                        LastName="Jackson3"
                                    }
                                };
            Person[] persons2 ={
                                    new Person
                                    {
                                        FirstName="Michael",
                                        LastName="Jackson"
                                    },
                                    new Person
                                    {
                                        FirstName="Michael2",
                                        LastName="Jackson2"
                                    },
                                    new Person
                                    {
                                        FirstName="Michael3",
                                        LastName="Jackson3"
                                    }
                                };
            if ((persons1 as IStructuralEquatable).Equals(
                persons2, EqualityComparer<Person>.Default))
            {
                Console.WriteLine("the same content");
            }
            else
            {
                Console.WriteLine("Not the same content");
            }
        }

        public class Person : IEquatable<Person>
        {
            public int ID { get; private set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public override string ToString()
            {
                return
                    string.Format("{0},{1}{2}", ID, FirstName, LastName);
            }

            public override bool Equals(object obj)
            {
                if (obj == null) throw new ArgumentNullException("obj");
                return Equals(obj as Person);
            }

            public bool Equals(Person other)
            //比较代码
            {
                return this.ID == other.ID &&
                        this.FirstName == other.FirstName &&
                        this.LastName == other.LastName;
            }

            public override int GetHashCode()
            {
                return ID.GetHashCode();
            }
        }

    }
}
