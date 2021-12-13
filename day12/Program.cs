var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToList();

var caveSystem = new CaveSystem(input);

System.Console.WriteLine($"Part 1: {caveSystem.PossiblePathsWithMaxOneVisitToSmallCave} possible paths.");
System.Console.WriteLine($"Part 2: {caveSystem.PossiblePathsWithMaxTwoVisitsToSmallCaves} possible paths.");

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

    public int PossiblePathsWithMaxOneVisitToSmallCave
    {
        get
        {
            return FindPaths("start", new List<string> { "start" }, new List<string>());
        }
    }

    public int PossiblePathsWithMaxTwoVisitsToSmallCaves
    {
        get
        {
            return FindPaths("start", new List<string> { "start" }, new List<string>(), 2);
        }
    }

    private int FindPaths(string cave, List<string> visitedSmallCaves, List<string> breadCrumbs, int maxVisitSmallCaves = 1)
    {
        var pathCount = 0;

        breadCrumbs.Add(cave);

        if (cave == "end")
        {
            // System.Console.WriteLine(string.Join(',', breadCrumbs));
            return ++pathCount;
        }

        // Check (for part 2) if one single small cave have been visited twice, then lower this limit
        if (visitedSmallCaves.GroupBy(c => c).Select(group => group.Count()).Where(x => x >= 2).Any()) maxVisitSmallCaves = 1;
        var pathsToVisit = paths.Where(p => p.Key.Equals(cave) && visitedSmallCaves.Count(c => c == p.Value) < maxVisitSmallCaves && p.Value != "start").ToList();
        foreach (var path in pathsToVisit.OrderBy(p => p.Value))
        {
            // this starts a new path, copy visitedSmallCaves
            var visitedSmallCavesForThisPath = new List<string>(visitedSmallCaves);
            var breadCrumbForThisPath = new List<string>(breadCrumbs);

            if (path.Value == path.Value.ToLower())
            {
                // small cave
                if (visitedSmallCavesForThisPath.Count(c => c == path.Value) < maxVisitSmallCaves)
                {
                    // not visited before
                    visitedSmallCavesForThisPath.Add(path.Value);
                    pathCount += FindPaths(path.Value, visitedSmallCavesForThisPath, breadCrumbForThisPath, maxVisitSmallCaves);
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
                pathCount += FindPaths(path.Value, visitedSmallCavesForThisPath, breadCrumbForThisPath, maxVisitSmallCaves);
            }
        }
        return pathCount;
    }
}
