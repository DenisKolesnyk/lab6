using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab6
{
    public interface IComponents
    {
        string marking { get; set; }
        string type { get; set; }
        long par { get; set; }
        double number { get; set; }
    }

    class Statement : IComponents, IComparable<Statement>
    {
        public string marking { get; set; }
        public string type { get; set; }
        public short par { get; set; }
        public double number { get; set; }
        string IComponents.marking { get; set; }
        string IComponents.type { get; set; }
        long IComponents.par { get; set; }
        double IComponents.number { get; set; }

        public Statement(string marking, string type, long par, double number)
        {
            this.marking = marking;
            this.type = type;
            this.par = (short)par;
            this.number = number;
        }

        public string Info
        {
            get { return $"{marking} {type}"; }
        }

        public int CompareTo(Statement other)
        {
            return string.Compare(other.Info, Info, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return String.Format("{0,20}|{1,20}|{2,20}|{3,20}", marking, type, par, number);
        }
    }

    class CollectionType<T> : IEnumerable<T> where T : Statement
    {
        List<T> list = new List<T>();

        public CollectionType()
        {
            list = new List<T>();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return list[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                list[index] = value;
            }
        }

        public void Add(T person)
        {
            list.Add(person);
        }

        public T Remove(T person)
        {
            var element = list.FirstOrDefault(h => h == person);
            if (element != null)
            {
                list.Remove(element);
                return element;
            }
            throw new NullReferenceException();
        }

        public void Sort()
        {
            list.Sort();
        }

        public T GetByName(string marking)
        {
            return
            list.FirstOrDefault(
            h => string.Compare(h.Info, marking, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class CollectionType
    {
        List<CollectionType> link = new List<CollectionType>();
        string marking { get; set; }
        string type { get; set; }
        long par { get; set; }
        double number { get; set; }

        public CollectionType() { }

        public CollectionType(string marking, string type, long par, double salary)
        {
            this.marking = marking;
            this.type = type;
            this.par = par;
            this.number = number;
        }

        public void Add(CollectionType[] coll)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                link.Add(coll[i]);
            }
        }

        public void Output()
        {
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Позначення ", "Тип ", "Номінал ", "Кількість"));
            foreach (CollectionType s in link)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", s.marking, s.type, s.par, s.number));
            }
        }

        public void Select()
        {
            Console.WriteLine("\n                Запит 1:");
            var where = link.Where(h => (h.par >= 80 && h.number > 1000));
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Позначення ", "Тип ", "Номінал ", "Кількість"));
            foreach (var c in where)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", c.marking, c.type, c.par, c.number));
            }
            Console.WriteLine("\n                Запит 2:");
            var min = link.Min(h => h.number);
            Console.WriteLine($"                {min}");
            Console.WriteLine("\n                Запит 3:");
            var max = link.Max(h => h.number);
            Console.WriteLine($"                {max}");
            Console.WriteLine("\n                Запит 4:");
            var count = link.Count();
            Console.WriteLine($"                {count}");
            Console.WriteLine("\n                Запит 5:");
            var order = link.OrderBy(h => h.par).ThenByDescending(h => h.marking);
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Позначення ", "Тип ", "Номінал ", "Кількість"));
            foreach (var c in order)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", c.marking, c.type, c.par, c.number));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Statement sgn1 = new Statement("RT 11 24", "R", 100000, 12);
            Statement sgn2 = new Statement("RT 11 24", "R", 100000, 10);
            Statement sgn3 = new Statement("CGU 12K", "C", 17, 3);

            CollectionType<Statement> collection = new CollectionType<Statement>();
            collection.Add(sgn1);
            collection.Add(sgn2);
            collection.Add(sgn3);

            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Позначення ", "Тип ", "Номінал ", "Кількість"));
            foreach (Statement s in collection)
            {
                Console.WriteLine(s.ToString());
            }

            Statement sgn5 = new Statement("RT 11 24", "R", 100000, 12);
            Statement sgn6 = new Statement("RT 11 24", "R", 50000, 10);
            Statement sgn7 = new Statement("CGU 12K", "C", 17, 3);

            CollectionType<Statement> collection2 = new CollectionType<Statement>();
            collection.Add(sgn5);
            collection.Add(sgn6);
            collection.Add(sgn7);

            var list = new List<CollectionType<Statement>>();
            list.Add(collection);
            list.Add(collection2);

            Console.WriteLine("\n                OrderBy:");
            var order = collection.OrderBy(h => h.par).ThenBy(h => h.marking);
            foreach (var signal in order)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                where:");
            var where = collection.Where(h => (h.par >= 100 && h.number > 1450) || h.Info.StartsWith("L"));
            foreach (var signal in where)
            {
                Console.WriteLine(signal.ToString());
            }
            Console.WriteLine("\n                Select:");
            var select = collection.Select((h, i) => new { Index = i + 1, h.Info });
            foreach (var s in select)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n                Skip:");
            var skip = collection.Skip(3);
            foreach (var signal in skip)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                Take:");
            var take = collection.Take(3);
            foreach (var signal in take)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                Concat:");
            var concat = collection.Concat(collection2);
            foreach (var signal in concat)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                First:");
            var first = collection.First(h => h.Info.Length > 5);
            Console.WriteLine(first);
            Console.Write("\n                Min: ");
            var min = collection.Min(h => h.number);
            Console.WriteLine(min);
            Console.Write("\n                Max: ");
            var max = collection.Max(h => h.number);
            Console.WriteLine(max);
            Console.WriteLine("\nAll and Any:");
            var allAny = list.First(c => c.All(h => h.par >= 14) && c.Any(h => h is Statement)).Select(h => h.Info).OrderByDescending(s => s);
            foreach (var str in allAny)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("\nContains:");
            var contains = list.Where(c => c.Contains(sgn1)).SelectMany(c => c.SelectMany(h => h.Info.Split(' '))).Distinct().OrderBy(s => s).ToList();
            foreach (var str in contains)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();

            CollectionType ct = new CollectionType();

            CollectionType[] collections = {
                new CollectionType("RT 11 24", "R", 100000, 12),
                new CollectionType("RT 11 24", "R", 50000, 10),
                new CollectionType("CGU 12K", "C", 17, 3)
            };

            ct.Add(collections);
            ct.Output();
            ct.Select();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

