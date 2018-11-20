using UnityEngine;
using System.Collections;
using System.Linq;

public class LoadMusic
{
    public class MusicToKrosses
    {
        public float seconds;
        public Krosses kross;

        public MusicToKrosses(float seconds, Krosses kross)
        {
            this.seconds = seconds;
            this.kross = kross;
        }
    }

    private string[,] fileContent;
    private int cellsWithInfo;
    public MusicToKrosses[] creatorCommands;

    public LoadMusic(TextAsset file, float correctionSeconds)
    {
        cellsWithInfo = 0;
        fileContent = splitCsvFile(file.text);
        creatorCommands = extractMusicInfo(correctionSeconds);
    }

    private MusicToKrosses[] extractMusicInfo(float correctionSeconds)
    {
        MusicToKrosses[] musicInfo = new MusicToKrosses[cellsWithInfo];
        int musicInfoIndex = 0;
        for (int x = 0; x < fileContent.GetUpperBound(1) - 1; x++)
        {
            if (fileContent[1, x] != null && fileContent[1, x] != "0" && fileContent[1, x].Length > 0 && fileContent[0, x].Length > 0)
            {
                //only cells with information
                float time = timeInfo(fileContent[0, x]);
                Krosses arrow = (Krosses)(int.Parse(fileContent[1, x]));
                time = time - correctionSeconds;
                musicInfo[musicInfoIndex] = new MusicToKrosses(time, arrow);
                musicInfoIndex++;
            }
        }
        return musicInfo;
    }

    private float timeInfo(string time)
    {
        float output = 0;
        string[] timeText = time.Split(':');
        if (timeText.Length == 2)
        {
            output = float.Parse(timeText[0]) * 60 + float.Parse(timeText[1].Replace(",", "."));
        }
        return output;
    }

    private string[,] splitCsvFile(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        int width = 1;
        cellsWithInfo = 0;
        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = slitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x].Replace("\r", "");
            }
            if (outputGrid[1, y] != "0" && outputGrid[1, y] != "0" && outputGrid[1, y].Length > 0 && outputGrid[0, y].Length > 0)
            {
                cellsWithInfo++;
            }
        }
        return outputGrid;
    }

    private string[] slitCsvLine(string line)
    {
        return line.Split(';');
    }

}

