﻿namespace BinaryToBarrelConverter
{
    internal class Logic
    {
        public Form1 Form {  get; set; }
        public char Direction {  get; set; }

        public void RunConversion(string[] text)
        {
            var dataPackFilePath = Path.Combine(Path.GetTempPath(), "LoadRom\\");

            new CreateDataPackFileStructure().CreateEverything(dataPackFilePath);

            new LookupTables().ChangeOfCords(Direction);


            // Write To Barrel
            int maxIterations = 0;

            // Set MaxIterations acording to lines in a singel ROM address (32 lines = 8 Hex Numbers)
            for (int i = 0; i < text.Length; i += 32)
            {
                if (text.Length - maxIterations > 32)
                {
                    maxIterations += 32;
                }
                else
                {
                    maxIterations = text.Length;
                }

                for (int j = 0; j < text[0].Length; j++)
                {
                    int hexAsInt = 0;

                    // Steps 4 lines at a time and write the Hex Value into a Barrel
                    for (int k = i; k < maxIterations; k += 4)
                    {
                        if (text[k][j] == '1') hexAsInt += 1;
                        if (text[k + 1][j] == '1') hexAsInt += 2;
                        if (text[k + 2][j] == '1') hexAsInt += 4;
                        if (text[k + 3][j] == '1') hexAsInt += 8;
                    }
                }
            }

            char[] line = text[0].ToCharArray();
            char[] t = line;
        }
    }
}
