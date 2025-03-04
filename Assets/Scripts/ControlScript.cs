using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.EventSystems;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public class ControlScript : MonoBehaviourPunCallbacks
{
    public Color buttonDepressedColor = new Color(1.0f, 1.0f, 1.0f);
    public Color buttonPressedColor = new Color(0.6f, 0.6f, 0.6f);

    struct LeftRightPosition
    {
        public int leftRights; // negative value indicates left, positive value indicates right
        public int leftRightJs; // negative value indicates left, positive value indicates right
        public int leftRights1; // negative value indicates left, positive value indicates right
        public int leftRightJs1; // negative value indicates left, positive value indicates right
        public int leftRights2; // negative value indicates left, positive value indicates right
        public int leftRightJs2; // negative value indicates left, positive value indicates right
    }
    
    private LeftRightPosition savedPosition = new LeftRightPosition{leftRights=0, leftRightJs=0, leftRights1=0, leftRightJs1=0, leftRights2=0, leftRightJs2=0};
    private LeftRightPosition currentPosition = new LeftRightPosition{leftRights=0, leftRightJs=0, leftRights1=0, leftRightJs1=0, leftRights2=0, leftRightJs2=0};

    private string positionDataPath;
    private bool isFlashing = false;

    private void Awake()
    {
        positionDataPath = Application.persistentDataPath + "/positionData.dat";
        positionDataPath = Application.persistentDataPath + "/positionData.dat";
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("parthandpriyankaarebullies", roomOptions, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LeftButtonClicked()
    {
       // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.MoveLeftEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        currentPosition.leftRights--;
    }    
    public void LeftJButtonClicked()
    {
       // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.MoveLeftJEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        currentPosition.leftRightJs--;
    }

    public void RightButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.MoveRightEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        currentPosition.leftRights++;
    }

    public void RightJButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.MoveRightJEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        currentPosition.leftRightJs++;
    }

    public void UpButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.MoveUpEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void DownButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.MoveDownEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void EnableFlashing(bool enabled)
    {
        if (enabled)
        {
            // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            PhotonNetwork.RaiseEvent(Utility.EnableFlashingEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        } else 
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
            PhotonNetwork.RaiseEvent(Utility.DisableFlashingEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public void Flash()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        //Toggles
        isFlashing = !isFlashing;
        if (isFlashing)
        {
            PhotonNetwork.RaiseEvent(Utility.ShowScreenEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        } else 
        {
            PhotonNetwork.RaiseEvent(Utility.HideScreenEventCode, null, raiseEventOptions, SendOptions.SendReliable);
        } 
        Debug.Log("Flash pressed");
    }

    public void ScaleUpButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.ScaleUpEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void ScaleDownButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.ScaleDownEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void NextButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.NextEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void PrevButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.PreviousEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void StartStopButtonClicked()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.PauseTrackerCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void setBlue()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.BlueScreen, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void setWhite()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.WhiteScreen, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void SelectFOV(int FOV)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        switch (FOV)
        {
            case 30:
                PhotonNetwork.RaiseEvent(Utility.Toggle30FOV, null, raiseEventOptions, SendOptions.SendReliable);
                break;
            case 60:
                PhotonNetwork.RaiseEvent(Utility.Toggle60FOV, null, raiseEventOptions, SendOptions.SendReliable);
                break;
            case 70:
                PhotonNetwork.RaiseEvent(Utility.Toggle70FOV, null, raiseEventOptions, SendOptions.SendReliable);
                break;
            case 80:
                PhotonNetwork.RaiseEvent(Utility.Toggle80FOV, null, raiseEventOptions, SendOptions.SendReliable);
                break;


        }
    }
    public void LoadButtonClicked(int num)
    {
        loadFromFile(positionDataPath);

        int leftRightDiff = 0;
        int leftRightJDiff = 0; 
        switch (num)
        {
            case 0:
                leftRightDiff = savedPosition.leftRights - currentPosition.leftRights;
                leftRightJDiff = savedPosition.leftRightJs - currentPosition.leftRightJs;
                break;
            case 1:
                leftRightDiff = savedPosition.leftRights1 - currentPosition.leftRights;
                leftRightJDiff = savedPosition.leftRightJs1 - currentPosition.leftRightJs;
                break;
            case 2:
                leftRightDiff = savedPosition.leftRights2 - currentPosition.leftRights;
                leftRightJDiff = savedPosition.leftRightJs2 - currentPosition.leftRightJs;
                break;
        }
         
        if (leftRightDiff < 0)
        {
            int lefts = Math.Abs(leftRightDiff);
            for (int i = 0; i < lefts; i++)
            {
                LeftButtonClicked();
            }
        }
        else
        {
            int rights = leftRightDiff;
            for (int i = 0; i < rights; i++)
            {
                RightButtonClicked();
            }
        }

        
        if (leftRightJDiff < 0)
        {
            int lefts = Math.Abs(leftRightJDiff);
            for (int i = 0; i < lefts; i++)
            {
                LeftJButtonClicked();
            }
        }
        else
        {
            int rights = leftRightJDiff;
            for (int i = 0; i < rights; i++)
            {
                RightJButtonClicked();
            }
        }
    }

    private void saveToFile(string path)
    {
        FileStream file;

		if (File.Exists(path)) file = File.OpenWrite(path);
		else file = File.Create(path);

        BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, savedPosition);
		file.Close();
    }

    private void loadFromFile(string path)
    {
		FileStream file;

		if (File.Exists(path)) file = File.OpenRead(path);
		else
		{
			Debug.LogError("File not found");
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		savedPosition = (LeftRightPosition) bf.Deserialize(file);
		file.Close();
    }

    public void ToggleOneEye()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.ToggleOneEyeEventCode, null, raiseEventOptions, SendOptions.SendReliable);
    }

    public void ToggleButtonColor()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        if (clickedButton.GetComponent<UnityEngine.UI.Image>().color == buttonDepressedColor)
            clickedButton.GetComponent<UnityEngine.UI.Image>().color = buttonPressedColor;
        else
        {
            clickedButton.GetComponent<UnityEngine.UI.Image>().color = buttonDepressedColor;
        }
    }
}
