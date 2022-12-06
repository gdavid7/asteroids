/* SCOREBOARD
 * Keeps track of high scores
 * Author: David
 * Last Edited: Nov. 17
 * 
 * */


using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


    public class scoreboard
    {
        static String _FILE;
        public scoreboard()
        {

            _FILE = "assets/scores.txt";
            // THIS MAY HAVE TO BE CHANGED [ONLY THING TO CHANGE]
        }

        //public static void Main(String[] args)
        //{
        //    append("dsfjk", "57");
        //    append("dkjshf", "86");
        //    append("Njksd", "420");
        //    append("kdjs", "63");

        //    //Nindroz -> Fella -> broski -> Nigerian

        //    String a = retrieve("Nindroz");
        //    Console.WriteLine(a);
        //    // 420

        //    String b = retrieve("David");
        //    Console.WriteLine("b");
        //    // null

        //    clear();
        //    // Null

        //}


        public static void append(String name, String score)
        {

            ArrayList newArr = new ArrayList();// pasted into the txt file at the end
            // Adding stuff to txt document
            var scores = getScoreboard();
            bool scoreAdded = false;

            if (scores[0] == "" || scores == null) // if array is empty
            {
                newArr.Add(name + "/" + score);
            }

            else
            {
                for (int i = 0; i < scores.Length; i++)
                {
                    String scoreInstance = scores[i];
                    int numberScore = int.Parse(scoreInstance.Split("/")[1]);

                    if (numberScore <= int.Parse(score) && scoreAdded == false) //if score instance is lower than the new score, put the mew score in front.
                    {
                        newArr.Add(name + "/" + score);
                        scoreAdded = true;
                    }
                    newArr.Add(scoreInstance);
                }
            }

            // inserting scores into txt file
            String finalScores = String.Join(",", newArr.Cast<string>().ToArray());
            String directory = Directory.GetCurrentDirectory();
        String filePath = "./assets/scores.txt";
        System.IO.File.WriteAllText(filePath, finalScores);
        System.Diagnostics.Debug.WriteLine(name + " successfully added!");

        }
        public static string[] getScoreboard()
        {

            // get an STRING array of all the scores in order
            // FORMAT: ["NAME/SCORE", "NAME2/SCORE2", "NAME3/SCORE3"...]

            String directory = Directory.GetCurrentDirectory();
            String filePath = "./assets/scores.txt";
            string[] scores = System.IO.File.ReadAllText(filePath).Split(",");
            return scores;
        }

        public static string retrieve(String name)
        {
            // searches for name and retrieves score, returns NULL if name not found.
            string[] scores = getScoreboard();
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i].Split("/")[0].Equals(name))
                {
                    return scores[i].Split("/")[1];
                }
            }
            return null;
        }

        public static void clear()
        {
            //Clear the entire scoreboard
            String directory = Directory.GetCurrentDirectory();
        //String filePath = System.IO.Path.Combine(directory, _FILE);
        String filePath = "./assets/scores.txt";

        System.IO.File.WriteAllText(filePath, null);
        }
    }