using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;
public class AutoFlashController : MonoBehaviour
{
    public GameObject FlashButton;
    private Coroutine autoToggleCoroutine;
    private List<List<(bool isCrash, int duration)>> lapIntervals = new List<List<(bool, int)>>(); // List of intervals per lap
    private string lapData = "Lap 3: [('Non-Crash', 11), ('Crash', 10), ('Non-Crash', 8), ('Crash', 10), ('Non-Crash', 5), ('Crash', 9), ('Non-Crash', 6), ('Crash', 6), ('Non-Crash', 5)] Lap 4: [('Non-Crash', 14), ('Crash', 5), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 7), ('Crash', 9), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 5)] Lap 5: [('Non-Crash', 14), ('Crash', 7), ('Non-Crash', 6), ('Crash', 5), ('Non-Crash', 10), ('Crash', 6), ('Non-Crash', 5), ('Crash', 10), ('Non-Crash', 7)] Lap 6: [('Non-Crash', 6), ('Crash', 6), ('Non-Crash', 13), ('Crash', 9), ('Non-Crash', 10), ('Crash', 10), ('Non-Crash', 16)] Lap 7: [('Non-Crash', 12), ('Crash', 8), ('Non-Crash', 11), ('Crash', 5), ('Non-Crash', 7), ('Crash', 7), ('Non-Crash', 6), ('Crash', 9), ('Non-Crash', 5)] Lap 8: [('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 13), ('Crash', 10), ('Non-Crash', 11), ('Crash', 5), ('Non-Crash', 16)] Lap 9: [('Non-Crash', 8), ('Crash', 6), ('Non-Crash', 13), ('Crash', 8), ('Non-Crash', 14), ('Crash', 10), ('Non-Crash', 11)] Lap 10: [('Non-Crash', 8), ('Crash', 5), ('Non-Crash', 15), ('Crash', 6), ('Non-Crash', 8), ('Crash', 10), ('Non-Crash', 5), ('Crash', 7), ('Non-Crash', 6)] Lap 11: [('Non-Crash', 11), ('Crash', 6), ('Non-Crash', 6), ('Crash', 10), ('Non-Crash', 11), ('Crash', 6), ('Non-Crash', 20)] Lap 12: [('Non-Crash', 15), ('Crash', 8), ('Non-Crash', 5), ('Crash', 7), ('Non-Crash', 8), ('Crash', 7), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 5)] Lap 13: [('Non-Crash', 10), ('Crash', 8), ('Non-Crash', 7), ('Crash', 9), ('Non-Crash', 8), ('Crash', 10), ('Non-Crash', 5), ('Crash', 8), ('Non-Crash', 5)] Lap 14: [('Non-Crash', 12), ('Crash', 5), ('Non-Crash', 5), ('Crash', 6), ('Non-Crash', 9), ('Crash', 7), ('Non-Crash', 5), ('Crash', 10), ('Non-Crash', 11)] Lap 15: [('Non-Crash', 5), ('Crash', 10), ('Non-Crash', 8), ('Crash', 8), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 24)] Lap 16: [('Non-Crash', 12), ('Crash', 8), ('Non-Crash', 12), ('Crash', 9), ('Non-Crash', 11), ('Crash', 7), ('Non-Crash', 11)]";
    private bool isProcessing = false;
    private bool isPaused = false;
    private int currentLapIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Split data into lap lines based on lap #
        string[] lapLines = Regex.Split(lapData, @"Lap \d+:");
        Debug.Log("Number of lapLines is: " +  lapLines.Length);
        foreach (string lapLine in lapLines)
        {
            List<(bool, int)> currentLapIntervals = new List<(bool, int)>();
            // Use regex to extract number and crash data
            Match lapMatch = Regex.Match(lapLine, @"\[(.*?)\]");
            if (lapMatch.Success) 
            {
                string lapContent = lapMatch.Value;
                Debug.Log("Lap content: " + lapContent);
                MatchCollection matches = Regex.Matches(lapContent, @"\('(Crash|Non-Crash)', (\d+)\)");
                Debug.Log("Number of matches found is: " + matches.Count);
                foreach (Match match in matches)
                {
                    Debug.Log("Execution");
                    Debug.Log("Crash value:" + match.Groups[1].Value);
                    bool isCrash = match.Groups[1].Value == "Crash";
                    int duration = int.Parse(match.Groups[2].Value);
                    currentLapIntervals.Add((isCrash, duration));
                }
                Debug.Log("Current lap intervals count is: " + currentLapIntervals.Count);
                //Adds the list of lap intervals for the line to the 2D list
                lapIntervals.Add(currentLapIntervals);
            } 
        }
        autoToggleCoroutine = null;
        
    }

    private IEnumerator AutoToggleCoroutine()
    {
        var currentLap = lapIntervals[currentLapIndex];
        Debug.Log("Current lap index is: " + currentLapIndex);
        for (int i = 0; i < currentLap.Count; i++) 
        {
            if (i == 0)
            {
                //first interval starts as a non-crash:
                yield return new WaitForSeconds(currentLap[0].duration);
                continue;
            }
            Debug.Log("Interval index is : " + i);
            var interval = currentLap[i];

            //toggle flashbutton
            FlashButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            //waits for interval specified duration
            yield return new WaitForSeconds(interval.duration);
        }
        //Its done executing the laps
        isProcessing = false;
        //exit coroutine
        yield break;
    }

    // Button function
    public void AutoToggleButton()
    {
        if (isProcessing)
        {
            if (isPaused)
            {
                // If the process is paused, unpause it
                isPaused = false;
                Debug.Log("Resuming lap processing...");
            }
            else
            {
                // If the process is running, pause it
                isPaused = true;
                Debug.Log("Pausing lap processing...");
            }
        }
        else if (currentLapIndex < lapIntervals.Count)
        {
            // Start the processing if not running
            isProcessing = true;
            isPaused = false;
            autoToggleCoroutine = StartCoroutine(AutoToggleCoroutine());
            Debug.Log("Starting lap processing...");
            currentLapIndex++; // Move to next lap
        }
        else
        {
            Debug.Log("All laps completed.");
        }
    }
}
