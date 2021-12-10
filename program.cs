class Program
    {
        static bool WithoutComment(string line, out string modifiedLine)
        {
            modifiedLine = line;
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] == '/' && line[i + 1] == '/') 
                {
                    modifiedLine = line.Remove(i);
                    return true;
                }
                for (int j = 0; j < line.Length - 1; j++)
                {
                    if (line[i] == '/' && line[i + 1] == '*' && i < j && line[j] == '*' && line[j + 1] == '/')
                    {
                        modifiedLine = line.Remove(i, j + 2 - i);
                        return true;
                    }
                }
            }
            return false;
        }
        static void Process(string fv, string fvr, string fa)
        {
            using (StreamReader reader = new StreamReader(fv))
            {
                using (StreamWriter fr = new StreamWriter(fvr, false))
                {
                    using (StreamWriter fra = new StreamWriter(fa, false))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Trim().Length > 0)
                            {
                                string modifiedLine = line;
                                if (WithoutComment(line, out modifiedLine))
                                    fra.WriteLine(line);
                                if (modifiedLine.Length > 0)
                                    fr.WriteLine(modifiedLine);
                            }
                            else
                                fr.WriteLine(line);
                        }
                    }
                }
            }
        }

        const string CFd = "Text.txt";
        const string CFr = "Results.txt";
        const string CFa = "Analysis.txt";

        static void Main(string[] args)
        {
            Process(CFd, CFr, CFa);
        }
    }
