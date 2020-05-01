using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
public class SampleHub: Hub
{
    public Task SendMessage()
    {
        return Clients.All.SendAsync("sampleClientMethod");
    }

    public Task AskBlackAce()
    {
        return Clients.All.SendAsync("a");
    }

    // public Task AnswerBlackAce(bool showBlackAce)
    // {
    //     Game game;
    //     game.publicList.Add()
    // }

    // public Task GetPlayerHand()
    // {

    // }

    // public Task ReturnPlayerHand(List<Card> cards)
    // {
    //     Hand hand;
    //     try {
    //         hand = new Hand(cards)
    //     }
    //     catch
    //     {
            
    //     }

    //     Player player;
    //     player.PlayHand(hand);
    // }
}