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

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
            _currentDateTime = ParseDateTime(timeData.datetime);
            IsTimeLodaed = true;
            Debug.Log("Success.");
            Debug.Log($"{_currentDateTime}");
        }
    }

    DateTime ParseDateTime(string datetime)
    {
        string date = Regex.Match(datetime, @"^\d{4}-\d{2}-\d{2}").Value;
        string time = Regex.Match(datetime, @"\d{2}:\d{2}:\d{2}").Value;
        string datetimeString = $"{date} {time}";
        DateTime parsedDateTime = DateTime.ParseExact(datetimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        string formattedDatetime = parsedDateTime.ToString("HH:mm:ss");
        Debug.Log(formattedDatetime);
        return parsedDateTime;
    }


}
