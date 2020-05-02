using Microsoft.AspNetCore.SignalR;

public class BackToFront
{
    // do some injection in startup.cs
    private static IHubContext<PlayerHub>  _hubContext;
    public BackToFront(IHubContext<PlayerHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public static async void AskForPlayBackend(string connectionId)
    {
        await _hubContext.Clients.Client(connectionId).SendAsync("AskForPlayFrontend");
    }

    public static async void AskPlayOneMoreRoundBackend()
    {
        await _hubContext.Clients.All.SendAsync("AskPlayOneMoreRoundFrontend");
    }

    public static async void AskReturnTributeBackend()
    {
        await _hubContext.Clients.All.SendAsync("AskReturnTributeFrontend");
    }

    public static async void AskAceGoPublicBackend(string aceId)
    {
        await _hubContext.Clients.Client(aceId).SendAsync("AskAceGoPublicFrontend");
    }

    // alert, do not need respond
    public static async void HandIsValidBackend(string userId)
    {
        await _hubContext.Clients.Client(userId).SendAsync("HandIsValidFrontend");
    }

    // alert, do not need respond
    public static async void HandIsNotValidBackend(string userId)
    {
        await _hubContext.Clients.Client(userId).SendAsync("HandIsNotValidFrontend");
    }

}