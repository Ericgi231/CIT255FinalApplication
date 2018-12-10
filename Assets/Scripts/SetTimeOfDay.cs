using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeOfDay : MonoBehaviour
{

    public Material midday;
    public Material evening;
    public Material midnight;
    public Material sunset;
    public Material daybreak;

    // Use this for initialization
    void Start()
    {

        //get hours into day
        double HoursIntoDay = System.DateTime.Now.TimeOfDay.TotalHours;

        //set skybox to time of day
        if (HoursIntoDay >= 20)
        {
            RenderSettings.skybox = midnight;
            GetComponent<Light>().enabled = false;
        }
        else if (HoursIntoDay >= 18)
        {
            RenderSettings.skybox = sunset;
        }
        else if (HoursIntoDay >= 14)
        {
            RenderSettings.skybox = evening;
        }
        else if (HoursIntoDay >= 10)
        {
            RenderSettings.skybox = midday;
        }
        else if (HoursIntoDay >= 7)
        {
            RenderSettings.skybox = daybreak;
        }
        else
        {
            RenderSettings.skybox = midnight;
            GetComponent<Light>().enabled = false;
        }

        //set angle of sun
        transform.Rotate(Vector3.right, ((int)HoursIntoDay * 15));

        //Update light
        DynamicGI.UpdateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {

    }
}