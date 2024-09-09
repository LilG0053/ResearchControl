using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviourPunCallbacks
{
    struct LeftRightPosition
    {
        public int leftRights; // negative value indicates left, positive value indicates right
        public int leftRightJs; // negative value indicates left, positive value indicates right
    }
    
    private LeftRightPosition savedPosition = new LeftRightPosition{leftRights=0, leftRightJs=0};
    private LeftRightPosition currentPosition = new LeftRightPosition{leftRights=0, leftRightJs=0};

    private void Awake()
    {
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

    public void FlashingButtonClicked()
    {
        // object[] content = new object[] {"hiiiiiii"}; // Array contains the target position and the IDs of the selected units
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(Utility.ToggleFlashingEventCode, null, raiseEventOptions, SendOptions.SendReliable);
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

    public void SaveButtonClicked()
    {
        savedPosition.leftRights = currentPosition.leftRights;
        savedPosition.leftRightJs = currentPosition.leftRightJs;
    }

    public void LoadButtonClicked()
    {
        int leftRightDiff = savedPosition.leftRights - currentPosition.leftRights;
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

        int leftRightJDiff = savedPosition.leftRightJs - currentPosition.leftRightJs;
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
}
