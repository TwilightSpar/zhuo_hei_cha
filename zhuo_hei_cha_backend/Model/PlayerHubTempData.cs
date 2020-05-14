using System.Collections.Generic;

public static class PlayerHubTempData
{
    public static List<Card> userHand = new List<Card>{};
    public static bool playOneMoreTime;
    public static bool aceGoPublic = false;
    public static List<Card> returnCards;
    public static bool finishPlay = false;
    
}