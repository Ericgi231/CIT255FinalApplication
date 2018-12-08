using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Newtonsoft.Json;
using System.Net;

public class WeatherController : MonoBehaviour {

    public WeatherData WeatherData;
    public static bool IsRaining;

	// Use this for initialization
	void Start () {

        WeatherData = GetCurrentWeatherData();

        if (WeatherData.Weather[0].Main == "Rain")
        {
            GameObject rain = (GameObject)Instantiate(Resources.Load("RainPrefab")); ;
            IsRaining = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject rain = (GameObject)Instantiate(Resources.Load("RainPrefab")); ;
            IsRaining = true;
        }
	}

    WeatherData GetCurrentWeatherData()
    {
        WeatherData weatherData;
        string weatherString;

        string url = "http://api.openweathermap.org/data/2.5/weather?&lat=44.763058&lon=-85.620628&appid=f85a5d7205afbce709a5da55d609d089";

        using (WebClient client = new WebClient())
        {
            weatherString = client.DownloadString(url);
        }

        weatherData = JsonConvert.DeserializeObject<WeatherData>(weatherString);

        return weatherData;
    }
}
