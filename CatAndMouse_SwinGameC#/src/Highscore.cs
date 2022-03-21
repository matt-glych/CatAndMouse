using System;
using System.IO;

namespace MyGame
{
    public class Highscore
    {
        public string name;
        public int score;
        public string result;
        public string filename = "highscore.txt";

        public Highscore()
        {
            Read();
        }

        // read highscore from file
        public void Read()
        {
            try
            {
                StreamReader readScore = new StreamReader(filename);
                //name = readScore.ReadLine();
                string score_txt = readScore.ReadLine();
                if(string.IsNullOrEmpty(score_txt))
                {
                    score = 0;
                }
                else
                {
                    score = int.Parse(score_txt);
                }
              
                readScore.Close();
                result = "HIGH SCORE DATA FOUND!";
            }
            catch
            {
                result = "NO HIGH SCORE DATA FOUND!";
            }
        }

        // write highscore to file
        public void Save(string playerName, int newHighScore)
        {
            try
            {
                File.WriteAllText(filename, String.Empty);
                StreamWriter writeScore = new StreamWriter(filename);
                //writeScore.WriteLine(playerName);
                writeScore.WriteLine(newHighScore.ToString());
                writeScore.Close();
                result = "NEW HIGH SCORE SAVED!";
            }
            catch
            {
                result = "HIGHSCORE ERROR: REQ ADMIN PRIV";
            }
        }
    }
}