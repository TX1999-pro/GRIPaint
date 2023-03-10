using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

public class SerialConnection : MonoBehaviour
{
    private SerialPort sp;
    public string recievedData;
    public static float forceValue;

    public static bool sendHapticPI;

    private PainManager painScript;
    private PainPoint painPoint;

    private float relPIHaptic;
    private string writeRelativePIHaptic;

    public int readTimeOutTime = 20;

    // Start is called before the first frame update
    void Start()
    {
        string the_com = "";

        //change COM and baude rate here
        sp = new SerialPort("COM4", 9600);


        if (!sp.IsOpen)
        {
            print("Opening " + the_com + ", baud 9600");
            sp.Open();
            sp.Handshake = Handshake.None;
            if (sp.IsOpen) { print("Open"); }

            sp.ReadTimeout = readTimeOutTime;
        }
    }

    private void OnEnable()
    {
        GameManager.OnNewPainPointRequired += assignNewPainPoint;
    }

    private void OnDisable()
    {
        GameManager.OnNewPainPointRequired -= assignNewPainPoint;
    }

    public void assignNewPainPoint(float x, float y)
    {
        painPoint = PainManager.painPoint;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            recievedData = sp.ReadLine();
            recievedData = recievedData.Replace(".", ",");
            if (float.TryParse(recievedData, out forceValue))
            {
            }
            else
            {
                Debug.Log("Parsing failed");
            }

            //Send HapticPI to arduino
            if (sendHapticPI == true)
            {
                relPIHaptic = painPoint.relativePIHaptic / 100;
                writeRelativePIHaptic = relPIHaptic.ToString("0.00");
                Debug.Log("relPIHaptic =" + writeRelativePIHaptic);
                sp.WriteLine(writeRelativePIHaptic);
            }
        }
        catch
        {
            if (sendHapticPI == true)
            {
                relPIHaptic = painPoint.relativePIHaptic / 100;
                writeRelativePIHaptic = relPIHaptic.ToString("0.00");
                Debug.Log("relPIHaptic =" + writeRelativePIHaptic);
                sp.WriteLine(writeRelativePIHaptic);
            }
        }
    }

    private void OnApplicationQuit()
    {
        sp.Close();
    }
}
