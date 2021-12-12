var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .Select(l => l.ToCharArray().Select(c => Convert.ToInt32(c.ToString())));
