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
     *  returns the top 5 high scores in a dictionary form: {"01": "score;name;timestamp", "02":"score;name;timestamp"...}
     *  
     *  
     *  retrieveUser(String name):
     *      gets all past games of a specific person, returns in the form of a Dictionary<String, String> where key is timestamp and value is score.
     * */

    static String _FILE;
    static IFirebaseConfig fcon;
    static IFirebaseClient client;
    public scoreboard()
    {

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
            
        }
    }

    // add a score instance to the realtime database
    public void updateUser(String name, String time, String score)
    {


        SetResponse response = client.Set("users/" + name + "/" + time, score);
        updateScoreboard(name, time, score);
        System.Diagnostics.Debug.WriteLine("DATA PUSHED");
    }
    // DO NOT call this method - it is used in update user
    // Keeps a list of the top 10 high scores
    public void updateScoreboard(String name, String time, String score)
    {


        // retrieve the dictionary of the responses {01: score/name/time, 02: score/name/time}
        FirebaseResponse response = client.Get("scoreboard");
        var p1 = response.GetType().GetProperties().First(o => o.Name == "Body").GetValue(response, null);
        String resp = p1.ToString();
        Dictionary<String, String> respDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(resp);



        // update score if needed - add scores to a queue then copy the first 5 elements into the dictionary
        bool scoreAdded = false;
        Queue<String> q = new Queue<String>();
        Dictionary<String, String> updateScoreboard = new Dictionary<string, string>();
        
        foreach (KeyValuePair<string, string> entry in respDict)
        {
            if(updateScoreboard.Count < 5)
            {
                // do something with entry.Value or entry.Key
                String value = entry.Value;
                int currentScore = Int16.Parse(value.Split(";")[0]);
                //nd.Add(currentScore, value);
                if (Int16.Parse(score) > currentScore && scoreAdded == false)
                {
                    q.Enqueue(score + ";" + name + ";" + time);
                    scoreAdded = true;
                }
                q.Enqueue(value);
                updateScoreboard.Add(entry.Key, q.Dequeue());
                //respDict[entry.Key] = q.Dequeue();
            }

        }
        SetResponse updateBoard = client.Set<Dictionary<String, String>>("scoreboard", updateScoreboard);
        System.Diagnostics.Debug.WriteLine("SCOREBOARD UPDATED");
    }

    public Dictionary<String, String> getScoreboard()
    {
        FirebaseResponse response = client.Get("scoreboard");
        var p1 = response.GetType().GetProperties().First(o => o.Name == "Body").GetValue(response, null);
        String resp = p1.ToString();
        Dictionary<String, String> respDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(resp);
        return respDict;
    }



    // retrieve stats of a specific person, returns in DICTIONARY form {timestamp: score, timestamp:score}



    public Dictionary<String, String> retrieveUser(String name)
    {
        FirebaseResponse response = client.Get("users/" + name);
        
        var p1 =  response.GetType().GetProperties().First(o => o.Name == "Body").GetValue(response, null);


        String resp = p1.ToString();
        System.Diagnostics.Debug.WriteLine("RESP");
        System.Diagnostics.Debug.WriteLine(resp);
        Dictionary<String, String> respDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(resp);


        return respDict;
    }


}