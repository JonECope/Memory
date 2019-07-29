using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class initializeGame : MonoBehaviour
{
    public UnityEvent showSetupButton;
    public UnityEvent startSplashScreen;

    // Start is called before the first frame update
    void Start()
    {
        string filepath = Path.Combine(Application.dataPath, "DataInfo.json");
        if (!File.Exists(filepath))
        {
            File.Create(filepath);
            showSetupButton.Invoke();
        }
        else
        {
            // Start the splash screen and stop the initialize screen
            startSplashScreen.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
