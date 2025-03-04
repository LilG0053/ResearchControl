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
    private List<(bool isCrash, int duration)> intervals = new List<(bool, int)>();
    private string lapData = "Lap 3: [('Non-Crash', 11), ('Crash', 10), ('Non-Crash', 8), ('Crash', 10), ('Non-Crash', 5), ('Crash', 9), ('Non-Crash', 6), ('Crash', 6), ('Non-Crash', 5)] Lap 4: [('Non-Crash', 14), ('Crash', 5), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 7), ('Crash', 9), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 5)] Lap 5: [('Non-Crash', 14), ('Crash', 7), ('Non-Crash', 6), ('Crash', 5), ('Non-Crash', 10), ('Crash', 6), ('Non-Crash', 5), ('Crash', 10), ('Non-Crash', 7)] Lap 6: [('Non-Crash', 6), ('Crash', 6), ('Non-Crash', 13), ('Crash', 9), ('Non-Crash', 10), ('Crash', 10), ('Non-Crash', 16)] Lap 7: [('Non-Crash', 12), ('Crash', 8), ('Non-Crash', 11), ('Crash', 5), ('Non-Crash', 7), ('Crash', 7), ('Non-Crash', 6), ('Crash', 9), ('Non-Crash', 5)] Lap 8: [('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 13), ('Crash', 10), ('Non-Crash', 11), ('Crash', 5), ('Non-Crash', 16)] Lap 9: [('Non-Crash', 8), ('Crash', 6), ('Non-Crash', 13), ('Crash', 8), ('Non-Crash', 14), ('Crash', 10), ('Non-Crash', 11)] Lap 10: [('Non-Crash', 8), ('Crash', 5), ('Non-Crash', 15), ('Crash', 6), ('Non-Crash', 8), ('Crash', 10), ('Non-Crash', 5), ('Crash', 7), ('Non-Crash', 6)] Lap 11: [('Non-Crash', 11), ('Crash', 6), ('Non-Crash', 6), ('Crash', 10), ('Non-Crash', 11), ('Crash', 6), ('Non-Crash', 20)] Lap 12: [('Non-Crash', 15), ('Crash', 8), ('Non-Crash', 5), ('Crash', 7), ('Non-Crash', 8), ('Crash', 7), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 5)] Lap 13: [('Non-Crash', 10), ('Crash', 8), ('Non-Crash', 7), ('Crash', 9), ('Non-Crash', 8), ('Crash', 10), ('Non-Crash', 5), ('Crash', 8), ('Non-Crash', 5)] Lap 14: [('Non-Crash', 12), ('Crash', 5), ('Non-Crash', 5), ('Crash', 6), ('Non-Crash', 9), ('Crash', 7), ('Non-Crash', 5), ('Crash', 10), ('Non-Crash', 11)] Lap 15: [('Non-Crash', 5), ('Crash', 10), ('Non-Crash', 8), ('Crash', 8), ('Non-Crash', 7), ('Crash', 8), ('Non-Crash', 24)] Lap 16: [('Non-Crash', 12), ('Crash', 8), ('Non-Crash', 12), ('Crash', 9), ('Non-Crash', 11), ('Crash', 7), ('Non-Crash', 11)]";
    private bool autoToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        // Split data into lap lines based on lap #
        string[] lapLines = Regex.Split(lapData, @"Lap \d+:");
        Debug.Log("Number of lapLines is: " +  lapLines.Length);
        foreach (string lapLine in lapLines)
        {
            // Use regex to extract number and crash data
            Match lapMatch = Regex.Match(lapLine, @"\[(.*?)\]");
            if (!lapMatch.Success) 
            {
                Debug.Log("Lap match is not a success");
                continue;
            } 

            //Puts the crashes and durations into a matchcolleciton
            string lapContent = lapMatch.Value;
            Debug.Log("Lap content: " + lapContent);
            MatchCollection matches = Regex.Matches(lapContent, @"\('(Crash|Non-Crash)', (\d+)\)");
            Debug.Log("Number of matches found is: " + matches.Count);
            foreach (Match match in matches)
            {
                Debug.Log("Execution");
                Debug.Log("Crash value:" + match.Groups[1].Value);
                bool isCrash = match.Groups[0].Value == "Crash";
                int duration = int.Parse(match.Groups[2].Value);
                intervals.Add((isCrash, duration));
            }
            Debug.Log("Intervals count is: " + intervals.Count);
        }
        if (autoToggleCoroutine == null)
        {
            autoToggleCoroutine = StartCoroutine(AutoToggleCoroutine());
        }
        
    }

    private IEnumerator AutoToggleCoroutine()
    {
        int i = 0;
        while (autoToggle)
        {
            Debug.Log("Interval index is: " + i);
            var interval = intervals[i];
            i = (i + 1) % intervals.Count;
            // turn on/off the flash button, since its always alternating I can just invoke onclick right?
            FlashButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            yield return new WaitForSeconds(interval.duration);
        }
    }

    // Update is called once per frame
    public void AutoToggleButton()
    {
        autoToggle = !autoToggle;
        if (FlashButton == null)
        {
            Debug.LogError("Flash button not assigned in editor");
        }
        if (autoToggleCoroutine == null)
        {
            autoToggleCoroutine = StartCoroutine(AutoToggleCoroutine());
        }
    }
}
