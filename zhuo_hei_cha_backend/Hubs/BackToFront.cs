using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

public static class BackToFront
{
    public static IHubCallerClients clients;
    public static async Task AskForPlayBackend(IClientProxy client)
    {
        await client.SendAsync("AskForPlayFrontend");

        DateTime startTime = DateTime.Now;
        while(!PlayerHubTempData.finishPlay)
            if(((TimeSpan)(DateTime.Now - startTime)).TotalMilliseconds > 30000)
                break;
        
        PlayerHubTempData.finishPlay = false;
        await client.SendAsync("DisablePlayerButtons");
    }

    public static void NotifyOthersBackend()
    {        
        clients.All.SendAsync("NotifyOthersFrontend");
    }

    public static async Task AskPlayOneMoreRoundBackend()
    {
        await clients.All.SendAsync("AskPlayOneMoreRoundFrontend");
        await Task.Delay(15000);
        await clients.All.SendAsync("HidePlayOneMoreRoundFrontend");
    }

    public static async void AskReturnTributeBackend(IClientProxy client)
    {
        await client.SendAsync("AskReturnTributeFrontend");
    }

    public static async Task AskAceGoPublicBackend(IClientProxy client)
    {
        await client.SendAsync("AskAceGoPublicFrontend");
        await Task.Delay(5000);
        await client.SendAsync("HideAceGoPublicButton");
    }

    // alert, do not need respond
    public static void HandIsValidBackend(IClientProxy client)
    {
        client.SendAsync("HandIsValidFrontend");
    }

    public static void CreateErrorMessage(IClientProxy client, string errorMessage)
    {
        client.SendAsync("showErrorMessage", errorMessage);
    }

    public static void TributeReturnNotValidBackend(IClientProxy client)
    {
        client.SendAsync("TributeReturnNotValidFrontend");
    }

    public static void SendCurrentCardListBackend(IClientProxy client, List<Card> currentCardList)
    {
        var formattedCards = currentCardList.Select(card => card.ToString()).ToList();
        client.SendAsync("SendCurrentCardListFrontend", formattedCards);
    }

    public static void ShowCurrentPlayerTurnBackend(int currentPlayerIndex, IClientProxy client)
    {
        client.SendAsync("ShowCurrentPlayerTurnFront", currentPlayerIndex);
    }

    public static void PlayerListUpdateBackend(List<Card> userHand, string connectionId, int cardCount)
    {
        var handString = userHand.Select(c => c.ToString()).ToList();
        clients.All.SendAsync("PlayerListUpdateFrontend", handString, connectionId, cardCount);
    }

    public static void showAceIdPlayerListBackend(string aceId)
    {
        clients.All.SendAsync("showAceIdPlayerListFrontend", aceId);
    }

    public static void GameOverBackend(bool blackAceLose)
    {
        clients.All.SendAsync("GameOverFrontend", blackAceLose);
    }

    public static void BreakGameBackend()
    {
        clients.All.SendAsync("BreakGameFrontend");
    }

    public static void ResetState()
    {
        clients.All.SendAsync("ResetStateFrontend");
    }
}