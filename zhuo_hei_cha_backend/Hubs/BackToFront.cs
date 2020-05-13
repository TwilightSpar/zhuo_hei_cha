using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

public static class BackToFront
{
    public static IHubCallerClients clients;
    public static async Task AskForPlayBackend(IClientProxy client)
    {
        await client.SendAsync("AskForPlayFrontend");
        await Task.Delay(10000);
        await client.SendAsync("HidePlayHandButton");
    }

    public static async void AskPlayOneMoreRoundBackend()
    {
        await clients.All.SendAsync("AskPlayOneMoreRoundFrontend");
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

    public static void PlayerListUpdateBackend(List<Card> userHand, string connectionId)
    {
        var handString = userHand.Select(c => c.ToString()).ToList();
        clients.All.SendAsync("PlayerListUpdateFrontend", handString, connectionId);
    }

    public static void showAceIdPlayerListBackend(string aceId)
    {
        clients.All.SendAsync("showAceIdPlayerListFrontend", aceId);
    }
}