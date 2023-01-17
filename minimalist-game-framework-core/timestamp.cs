using System;
public class timestamp
{
	public timestamp()
	{

	}
	public static String get_time()
	{
        // returns current timestamp as a string
        DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;

        return now.ToUnixTimeSeconds().ToString();
    }
	public static String convert(String timestamp)
	{
		Double ts;
        if (Double.TryParse(timestamp, out ts) == true)
		{
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
            string formattedDate = dt.ToString("F");
			return formattedDate;
        }
		else
		{
			return "EMPTY";
		}

    }

}

