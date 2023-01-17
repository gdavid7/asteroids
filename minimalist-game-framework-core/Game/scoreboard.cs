using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml;

public class scoreboard
{



    /*
     * CLASS DIRECTIONS:
     * updateUser(String name, String time, String score):
     *  adds a score to the database. Make sure that the time is in the form of a timestamp: DateTime.Now.ToString("yyyyMMddHHmmssffff")
     *  
     * getScoreboard()
     *  returns the top 5 high scores in a dictionary form: {"01": "score/name/timestamp", "02":"score/name/timestamp"...}
     * */

    static String _FILE;
    static IFirebaseConfig fcon;
    static IFirebaseClient client;
    public scoreboard()
    {

        System.Diagnostics.Debug.WriteLine("DAVID IS HERE");
        _FILE = "assets/Scores.txt";
        // THIS MAY HAVE TO BE CHANGED [ONLY THING TO CHANGE]

        fcon = new FirebaseConfig()
        {
            AuthSecret = "6Q9l7CqqO3G4NgkvoRobpsddFVmaF2XKeVYHIWZh",
            BasePath = "https://asteroids-1e858-default-rtdb.firebaseio.com/"
        };
        client = new FireSharp.FirebaseClient(fcon);
        if (client != null)
        {
            System.Diagnostics.Debug.WriteLine("Firebase Connection is Established");
            updateUser("Nigerian", DateTime.Now.ToString("yyyyMMddHHmmssffff"), "54");
            updateUser("Nigerian", DateTime.Now.ToString("yyyyMMddHHmmssffff"), "57");
            Dictionary<String, String> r = retrieveUser("Nigerian");
            System.Diagnostics.Debug.WriteLine(string.Join(Environment.NewLine, r));

        }
    }

    // add a score instance to the realtime database
    public static void updateUser(String name, String time, String score)
    {


        SetResponse response = client.Set("users/" + name + "/" + time, score);
        updateScoreboard(name, time, score);
        System.Diagnostics.Debug.WriteLine("DATA PUSHED");
    }
    // DO NOT call this method - it is used in update user
    // Keeps a list of the top 10 high scores
    public static void updateScoreboard(String name, String time, String score)
    {


        // retrieve the dictionary of the responses {01: score/name/time, 02: score/name/time}
        FirebaseResponse response = client.Get("scoreboard");
        var p1 = response.GetType().GetProperties().First(o => o.Name == "Body").GetValue(response, null);
        String resp = p1.ToString();
        Dictionary<String, String> respDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(resp);



        // update score if needed - add scores to a queue then copy the first 5 elements into the dictionary
        bool scoreAdded = false;
        Queue<String> q = new Queue<String>();
        
        
        foreach (KeyValuePair<string, string> entry in respDict)
        {
            // do something with entry.Value or entry.Key
            String value = entry.Value;
            int currentScore = Int16.Parse(value.Split(";")[0]);
            //nd.Add(currentScore, value);
            if(Int16.Parse(score) > currentScore && scoreAdded == false)
            {
                q.Enqueue(score + ";" + name + ";" + time);
                scoreAdded = true;
            }
            q.Enqueue(value);
            respDict[entry.Key] = q.Dequeue();
        }
        SetResponse updateBoard = client.Set<Dictionary<String, String>>("scoreboard", respDict);
        System.Diagnostics.Debug.WriteLine(string.Join(Environment.NewLine, respDict));
    }

    public static Dictionary<String, String> getScoreboard()
    {
        FirebaseResponse response = client.Get("scoreboard");
        var p1 = response.GetType().GetProperties().First(o => o.Name == "Body").GetValue(response, null);
        String resp = p1.ToString();
        Dictionary<String, String> respDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(resp);
        return respDict;
    }

    /*

    // retrieve stats of a specific person, returns in DICTIONARY form {timestamp: score, timestamp:score}



    public static Dictionary<String, String> retrieveUser(String name)
    {
        FirebaseResponse response = client.Get("users/" + name);
        //Dictionary<String, int> r = response.ResultAs<Dictionary<String, int>>();
        //System.Diagnostics.Debug.WriteLine("Data Retrieved");
        var p1 =  response.GetType().GetProperties().First(o => o.Name == "Body").GetValue(response, null);
        // https://stackoverflow.com/questions/4144778/get-properties-and-values-from-unknown-object ^^


        String resp = p1.ToString();
        Dictionary<String, String> respDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(resp);


        return respDict;
    }






    public static void append(String name, String score)
        {
            // [NAME, SCORE, DATE]
            // profile/name/


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
        String filePath = System.IO.Path.Combine(directory, _FILE);
        System.IO.File.WriteAllText(filePath, finalScores);
        Console.WriteLine(name + " successfully added!");

    }



    public static string[] getScoreboard()
    {
        // get an STRING array of all the scores in order
        // FORMAT: ["NAME/SCORE", "NAME2/SCORE2", "NAME3/SCORE3"...]

        String directory = Directory.GetCurrentDirectory();
        String filePath = System.IO.Path.Combine(directory, _FILE);
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
        String filePath = System.IO.Path.Combine(directory, _FILE);
        System.IO.File.WriteAllText(filePath, null);
    }
    public static String getTime()
    {
        return DateTime.Now.ToString("h:mm:ss tt");
    }
    */
}