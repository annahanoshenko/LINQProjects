internal class Program
{
    private static void Main(string[] args)
    {
        var set = new TestSet();

        var filteredSet = set.Where(s => s > +50).Select(s => "*" + s.ToString() + "*");
       
        foreach(string i in filteredSet)
        {
            Console.WriteLine(i);
        }

        var result = set.First(s => s % 2 == 0);
        Console.WriteLine(result);

        var result1 = set.Where(i => i <= 5).Agregate(0, (acc, i) => acc + i);
        Console.WriteLine(result1);

        var resultSet = set.Select(in => new { Number = i, IsEven = i % 2 == 0 }).OrderBy(r => r.IsEven);
        foreach(var i in resultSet)
        {
            Console.WriteLine(i.Number);
        }

        var resultSet1 = set.Where(i => i % 2 == 0)
            .Select(i => "*" + i.ToString() + "*")
            .Where(i => i.Length == 4);
       
        foreach(var i in resultSet1)
        {
            Console.WriteLine(i);
        }
        Console.ReadLine();
    }
}