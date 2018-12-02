using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class WeatherController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        

    }

    // Update is called once per frame
    void Update () {
		
	}

    WeatherData GetCurrentWeatherData()
    {
        WeatherData weatherData;
        string weatherString;

        string url = "http://api.openweathermap.org/data/2.5/weather?&lat=44.763058&lon=-85.620628&appid=f85a5d7205afbce709a5da55d609d089";

        weatherData = JsonConvert

        return weatherData;
    }
}
