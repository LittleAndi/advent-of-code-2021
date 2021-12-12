var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

var caveSystem = new CaveSystem(input);

System.Console.WriteLine($"{caveSystem.PossiblePaths}");

internal class CaveSystem
{
    List<KeyValuePair<string, string>> paths = new List<KeyValuePair<string, string>>();

    public CaveSystem(List<string> sensorInput)
    {
        foreach (var input in sensorInput)
        {
            var nodes = input.Split('-');
            paths.Add(new KeyValuePair<string, string>(nodes[0], nodes[1]));
            paths.Add(new KeyValuePair<string, string>(nodes[1], nodes[0]));
        }
    }

    public int PossiblePaths
    {
        get
        {
            return FindPaths("start", new HashSet<string> { "start" }, new List<string>());
        }
    }

    private int FindPaths(string cave, HashSet<string> visitedSmallCaves, List<string> breadCrumbs)
    {
        var pathCount = 0;

        breadCrumbs.Add(cave);

        if (cave == "end")
        {
            System.Console.WriteLine(string.Join(',', breadCrumbs));
            return ++pathCount;
        }

        var pathsToVisit = paths.Where(p => p.Key.Equals(cave) && !visitedSmallCaves.Contains(p.Value)).ToList();
        foreach (var path in pathsToVisit)
        {
            // this starts a new path, copy visitedSmallCaves
            var visitedSmallCavesForThisPath = new HashSet<string>(visitedSmallCaves);
            var breadCrumbForThisPath = new List<string>(breadCrumbs);

            if (path.Value == path.Value.ToLower())
            {
                // small cave
                if (visitedSmallCavesForThisPath.Add(path.Value))
                {
                    // not visited before
                    pathCount += FindPaths(path.Value, visitedSmallCavesForThisPath, breadCrumbForThisPath);
                }
                else
                {
                    // visited before, this is not a path
                    continue;
                }
            }
            else
            {
                // BIG CAVE
                pathCount += FindPaths(path.Value, visitedSmallCavesForThisPath, breadCrumbForThisPath);
            }
        }
        return pathCount;
    }
}
