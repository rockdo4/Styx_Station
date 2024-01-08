using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class TestServerTime : MonoBehaviour
{
    public static TestServerTime Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        StartCoroutine(GetRealDateTimeFromAPI());
    }


    //json container
    struct TimeData
    {
        //public string client_ip;
        //...
        public string datetime;
        //..
    }

    const string API_URL = "https://worldtimeapi.org/api/timezone/Asia/Seoul";  //"https://worldtimeapi.org/api/ip";

    [HideInInspector] public bool IsTimeLodaed = false;

    private DateTime _currentDateTime = DateTime.Now;    

    void Start()
    {
        
    }

    public DateTime GetCurrentDateTime()
    {
        //here we don't need to get the datetime from the server again
        // just add elapsed time since the game start to _currentDateTime
        StopCoroutine(GetRealDateTimeFromAPI());
        StartCoroutine(GetRealDateTimeFromAPI());
        return _currentDateTime;
    }

    public IEnumerator GetRealDateTimeFromAPI()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
        Debug.Log("getting real datetime...");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            //error
            Debug.Log("Error: " + webRequest.error);

        }
        else
        {
            //success
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
            //timeData.datetime value is : 2020-08-14T15:54:04+01:00

            _currentDateTime = ParseDateTime(timeData.datetime);
            IsTimeLodaed = true;

            Debug.Log("Success.");
            Debug.Log($"{_currentDateTime}");
        }

    }

    //datetime format => 2020-08-14T15:54:04+01:00
    DateTime ParseDateTime(string datetime)
    {
        //match 0000-00-00
        string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;

        //match 00:00:00
        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;

        string datetimeString = $"{date} {time}";

        DateTime parsedDateTime = DateTime.ParseExact(datetimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        // Format the datetime using 24-hour format
        string formattedDatetime = parsedDateTime.ToString("HH:mm:ss");
        Debug.Log(formattedDatetime);

        return parsedDateTime;


        //return DateTime.Parse(string.Format("{0} {1}", date, time));
    }


}
