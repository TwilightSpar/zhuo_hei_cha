using System.Collections.Generic;

public static class PlayerHubTempData
{
    public static List<Card> userHand = new List<Card>{};
    public static bool playOneMoreRound = true;
    public static bool aceGoPublic = false;
    public static List<Card> returnCards;
    public static bool finishPlay = false;
    
    public static void reinitTempData(){
        PlayerHubTempData.userHand = new List<Card>{};
        PlayerHubTempData.playOneMoreRound = true;
        PlayerHubTempData.aceGoPublic = false;
        PlayerHubTempData.finishPlay = false;
    }
}