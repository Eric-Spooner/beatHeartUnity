using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

    enum States
    {
        None,
        Scan,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }

    public GameObject ScannedItemPrefab;

    public string ServiceUUID = "ffb0";
    public string SubscribeCharacteristic = "ffb2";

    private float timeout;
    private float startScanTimeout = 10f;
    private float startScanDelay = 0.5f;
    private bool startScan = true;
    private States state = States.None;
    private bool flapped = false;
    private string connectAddress;
    public GameObject foundBallText;
    private Dictionary<string, ScannedItemScript> scannedItems;

    // Use this for initialization
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        scannedItems = new Dictionary<string, ScannedItemScript>();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {
            Debug.Log("Started bluetooth search:");
            timeout = startScanDelay;
            Debug.Log("State Scan");
            state = States.Scan;
        },
        (error) =>
        {
            Debug.Log("Error: " + error);
            if (error.Contains("Bluetooth LE Not Enabled"))
                BluetoothLEHardwareInterface.BluetoothEnable(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.None:
            //    Debug.Log("State None");
                break;

            case States.Scan:
           //     Debug.Log("State Scan");
                flapped = false;
                timeout -= Time.deltaTime;
                if (timeout <= 0f)
                {
                    if (startScan)
                    {
                        startScan = false;
                        timeout = startScanTimeout;

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, null, (address, name, rssi, bytes) =>
                        {
                            if (!scannedItems.ContainsKey(address))
                            {
                                //Check for Ball
                                if (name == "weixin-nini")
                                {
                                    Debug.Log("FOUND A BALLLLL: address: " + address);
                                    foundBallText.SetActive(true);
                                    BluetoothLEHardwareInterface.StopScan();
                                    scannedItems = new Dictionary<string, ScannedItemScript>();
                                    connectAddress = address;
                                    state = States.Connect;
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
                break;

            case States.Connect:
                Debug.Log("State Connect");
                BluetoothLEHardwareInterface.ConnectToPeripheral(connectAddress, null, null, (address, serviceUUID, characteristicUUID) =>
                {
                    if (IsEqual(serviceUUID, ServiceUUID))
                    {
                        state = States.Subscribe;
                    }
                }, (disconnect) =>
                {
                    if (connectAddress == disconnect)
                    {
                        //disconnect action
                        Debug.Log("Device disconnected " + disconnect);
                        //start search and conenct again
                        state = States.Unsubscribe;
                    }
                });
                break;

            case States.Subscribe:
                Debug.Log("State Subscribe");
                BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(connectAddress, ServiceUUID, SubscribeCharacteristic, (not1, not2) =>
                {
                    Debug.Log("Notification action: " + not1 + not2);
                }, (address, characteristicUUID, bytes) =>
                {
                    if (state == States.Subscribe)
                    {
                        state = States.None;
                        Debug.Log("State None");
                        BluetoothLEHardwareInterface.StopScan();
                        startFlappy();
                    }
                    Debug.Log("received data :) " + " power: " + System.Text.Encoding.UTF8.GetString(bytes, 1, 1) +
                        " grip y/n: " + System.Text.Encoding.UTF8.GetString(bytes, 2, 1) +
                        " strength: " + System.Text.Encoding.UTF8.GetString(bytes, 3, 3));

                    string grip = System.Text.Encoding.UTF8.GetString(bytes, 2, 1);
                    string strength = System.Text.Encoding.UTF8.GetString(bytes, 3, 3);


                    if (grip == "1" && flapped == false)
                    {
                        if (GamesController.instance != null)
                        {
                            GamesController.instance.updateFunction();
                            flapped = true;
                        }
                    }
                    else if (grip == "0")
                    {
                        flapped = false;
                    }

                });
                break;

            case States.Unsubscribe:
                Debug.Log("State Unsuscribe");
                BluetoothLEHardwareInterface.UnSubscribeCharacteristic(connectAddress, ServiceUUID, SubscribeCharacteristic, null);
                state = States.Scan;
                startScan = true;
                timeout = startScanDelay;
                break;

            case States.Disconnect:
                break;
        }
    }

    void startFlappy()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadSceneAsync("flappyHero", LoadSceneMode.Single);
    }

    string FullUUID(string uuid)
    {
        return "0000" + uuid + "-0000-1000-8000-00805f9b34fb";
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
    }
}