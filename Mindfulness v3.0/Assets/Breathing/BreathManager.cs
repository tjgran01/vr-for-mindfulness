using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathManager : MonoBehaviour {

    public static BreathManager instance;

    [SerializeField]
    ParticleSystem TPV_inhale;      //Third person View Inhale Particle system

    [SerializeField]
    ParticleSystem TPV_exhale;      //Third person View Exhale Particle system


    [SerializeField]
    ParticleSystem FPV_inhale;      //First person View Inhale Particle system

    [SerializeField]
    ParticleSystem FPV_exhale;      //First person View Exhale Particle system


    public float inhaleDuration;
    public float exhaleDuration;
     
    public int gap;

    private ParticleSystem inhale;
    private ParticleSystem exhale;

    private void Awake()
    {
        instance = this;

        var x = TPV_inhale.main;
        var xx = FPV_inhale.main;
        if (inhaleDuration == 0.0f)
        {
            inhaleDuration = 4.00f;
        }
        x.duration = inhaleDuration;
        xx.duration = inhaleDuration;

        var y = TPV_exhale.main;
        var yy = FPV_exhale.main;
        if (exhaleDuration == 0.0f)
        {
            exhaleDuration = 2.40f;
        }
        y.duration = exhaleDuration;
        yy.duration = exhaleDuration;

    }


    void Start () {

        if(ChangeViews.instance.spawnIndex == 0)
        {
            inhale = FPV_inhale;
            exhale = FPV_exhale;
        }else
        {
            inhale = TPV_inhale;
            exhale = TPV_exhale;
        }
        //StartCoroutine(Breathe());

    }

    public void BreatheOnClick()
    {
        StartCoroutine(Breathe());
    }

    IEnumerator Breathe()
    {

        inhale.Stop();
        exhale.Stop();

        inhale.Play();
        yield return new WaitForSeconds(inhaleDuration + gap);
        //inhale.Stop();
        exhale.Play();
        yield return new WaitForSeconds(exhaleDuration + gap);
        //exhale.Stop();
    }

    public void ChangedView(string view)
    {
        switch (view)
        {
            case "FPV":
                inhale = FPV_inhale;
                exhale = FPV_exhale;
                break;
            case "TPV":
                inhale = TPV_inhale;
                exhale = TPV_exhale;
                break;
            default:
                inhale = TPV_inhale;
                exhale = TPV_exhale;
                break;
        }

        //StartCoroutine(Breathe());
    }

}
