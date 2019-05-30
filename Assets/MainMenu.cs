using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour {

    public GameObject ScannedItemPrefab;

    private float timeout;
    private float startScanTimeout = 10f;
    private float startScanDelay = 0.5f;
    private bool startScan = true;
    public GameObject foundBallText;
    private Dictionary<string, ScannedItemScript> scannedItems;

    // Use this for initialization
    void Start()
    {
        scannedItems = new Dictionary<string, ScannedItemScript>();

        BluetoothLEHardwareInterface.Initialize(true, false, () => {
            Debug.Log("Started bluetooth search:");
            timeout = startScanDelay;
        },
        (error) => {
            Debug.Log("Error: " + error);
            if (error.Contains("Bluetooth LE Not Enabled"))
                BluetoothLEHardwareInterface.BluetoothEnable(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        timeout -= Time.deltaTime;
        if (timeout <= 0f)
        {
            if (startScan)
            {
                startScan = false;
                timeout = startScanTimeout;

                BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, null, (address, name, rssi, bytes) => {
                    if (!scannedItems.ContainsKey(address))
                    {
                        //Check for Ball
                        if(name == "weixin-nini")
                        {
                            Debug.Log("FOUND A BALLLLL: address: " + address);
                            foundBallText.SetActive(true);
                        }
                    }
                }, true);
            }
            else
            {
                BluetoothLEHardwareInterface.StopScan();
                startScan = true;
                timeout = startScanDelay;
            }
        }
    }
}