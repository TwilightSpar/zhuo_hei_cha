using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

public static class BackToFront
{
    public static IHubCallerClients clients;
    public static async void AskForPlayBackend(IClientProxy client)
    {
        await client.SendAsync("AskForPlayFrontend");
    }

    public static async void AskPlayOneMoreRoundBackend()
    {
        await clients.All.SendAsync("AskPlayOneMoreRoundFrontend");
    }

    public static async void AskReturnTributeBackend(IClientProxy client)
    {
        await client.SendAsync("AskReturnTributeFrontend");
    }

    public static async void AskAceGoPublicBackend(IClientProxy client)
    {
        await client.SendAsync("AskAceGoPublicFrontend");
    }

    // alert, do not need respond
    public static void HandIsValidBackend(IClientProxy client)
    {
        client.SendAsync("HandIsValidFrontend");
    }

    // alert, do not need respond
    public static void HandIsNotValidBackend(IClientProxy client)
    {
        client.SendAsync("HandIsNotValidFrontend");
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
}