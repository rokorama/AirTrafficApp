// See https://aka.ms/new-console-template for more information

internal class Program
{
    private static void Main(string[] args)
    {
        var map = new Map();
        map.Zones = InputParser.ReadMap("/Users/crisc/csharp/AirTrafficApp/SampleMap.map");

        var flightFeed = new List<string>()
        {
            "FR664(10,10) GB3265(4,9) NO5521(3,3)",
            "FR664(10,11) GB3265(4,9) NO5521(3,3)",
            "FR664(10,10) GB3265(4,9) NO5521(3,3)",
            "AE331(5,-1) GB3265(4,10) NO5521(4,4)",
            "AE331(5,0) GB3265(4,11) NO5521(5,5)",
            "AE331(5,1) GB3265(3,10)",
            "AE331(5,2) GB3265(3,9)",
            "AE331(5,3) GB3265(4,8)",
            "AE331(5,4) GB3265(4,7)",
            "AE331(5,5) GB3265(5,7)"
        };

        // var flightFeed = new List<string>()
        // {
        //     "FR664(10,10)",
        //     "FR664(10,11)",
        //     "FR664(10,10)",
        // };

        // var flightFeed = new List<string>()
        // {
        //     "FR664(10,10) GB3265(4,9) NO5521(3,3)",
        //     "FR664(10,11) GB3265(4,9) NO5521(3,3)",
        //     "FR664(10,10) GB3265(4,9) NO5521(3,3)"
        // };

        foreach (var entry in flightFeed)
        {
            map.UpdateMap(entry);
        }

        // while (true)
        // {
        //     var input = Console.ReadLine();
        //     map.UpdateMap(input!);

        // }
    }
}