using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class UserDiagnostics : MonoBehaviour
{
    public SteamVR_Action_Boolean LeftHandY;
    public GameObject diaInterface;
    public TextMeshProUGUI hydraulicText;
    public AudioSource diagSound;

    public WaterSystem WS;
    public int engineFailures;
    public AudioSource EngineStart;
    public AudioSource EngineStartGood;
    public AudioSource EngineStartBad;
    public AudioSource EngineOff;

    public GameObject StopEngineBtn;
    public GameObject StartEngineBtn;
    public GameObject StartingEngineBtn;
    public TextMeshProUGUI StartingEngineBtnTxt;

    public ErrorMaterial errorMat;

    [Header("Finish Maintenance")]
    public Toggle HSToggle;
    public TextMeshProUGUI engineFailuresTxt;
    public TextMeshProUGUI timeElapsedTxt;
    public TextMeshProUGUI ratingTxt;

    private void Update()
    {
        if (LeftHandY.stateDown)
        { 
            diaInterface.SetActive(!diaInterface.activeSelf);

            if (!diagSound.isPlaying)
            {
                diagSound.Play();
            }
        }

        hydraulicText.text =
            !WS.isHydrolicSystemBuilt ? "Hydrolic system is missing components. \n Please attach them and try again." : (
            "Resistance Level - " + (WS.isFilterGood && WS.isWaterGood ? "200kΩ+" : WS.isFilterGood ? "120kΩ~" : "40kΩ-") + "\n" +
            "Water ionization - " + (WS.isWaterGood ? "Low" : "High") + "\n" +
            "Last filter replacement - " + (WS.isFilterGood ? "25/01/2021" : "01/11/2020"));
    }

    public void AttemptStartEngine()
    {
        EngineStart.Play();
        StartCoroutine(StartingEngineBtnText());
    }
    
    public IEnumerator StartingEngineBtnText ()
    {
        StartingEngineBtnTxt.text = ".";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = "..";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = "...";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = ".";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = "..";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = "...";

        if (WS.isHydrolicSystemBuilt && WS.isFilterGood)
        {
            EngineStartGood.Play();
        }
        else
        {
            EngineStartBad.Play();
        }

        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = ".";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = "..";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = "...";
        yield return new WaitForSeconds(0.33f);
        StartingEngineBtnTxt.text = ".";

        if (WS.isHydrolicSystemBuilt && WS.isFilterGood)
        {
            WS.isEngineOn = true;
            StartingEngineBtn.SetActive(false);
            StopEngineBtn.SetActive(true);
            
            if (WS.canWaterBeCleaned && WS.isFilterGood)
            {
                WS.isWaterGood = true;
                WS.waterBoltSystem.attachedObj.isObjectFunctional = true;
            }
        }
        else
        {
            WS.isEngineOn = false;
            errorMat.Error();
            engineFailures++;
            StartingEngineBtn.SetActive(false);
            StartEngineBtn.SetActive(true);
        }

        yield return new WaitForSeconds(3f);
        StopEngine();
    }

    public void StopEngine()
    {
        if (WS.isEngineOn)
        {
            WS.isEngineOn = false;
            EngineOff.Play();
            StartingEngineBtn.SetActive(false);
            StopEngineBtn.SetActive(false);
            StartEngineBtn.SetActive(true);
        }
    }

    public void FinishMaintenance ()
    {
        HSToggle.isOn = WS.isFilterGood && WS.isWaterGood;
        engineFailuresTxt.text += " " + engineFailures;

        int secs = (int)(Time.timeSinceLevelLoad % 60);
        int min = (int)((Time.timeSinceLevelLoad / 60) % 60);
        timeElapsedTxt.text += " " + (min < 10 ? ("0" + min) : "" + min) + ":" + (secs < 10 ? ("0" + secs) : "" + secs);
        ratingTxt.text += HSToggle.isOn && engineFailures == 0 ? "10/10" : HSToggle.isOn && engineFailures < 10 ? engineFailures + "/10" : " 0/10";
    }

    public void Retry ()
    {
        //SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}

