using System;

// understand the color of a card
public static class ColorExtension
{
    public static int colorLevel(Color c1){
        if(c1 == Color.BigJoker)
            return 6;
        else if(c1 == Color.SmallJoker)
            return 5;
        else if(c1 == Color.Space)
            return 4;
        else if(c1 == Color.Heart)
            return 3;
        else if(c1 == Color.Club)
            return 2;
        else 
            return 1;
    }

    // if there are equal, than c1 is still greater than c2, because c1 is the first
    public static bool isGreaterThan(this Color c1, Color c2){
        return ColorExtension.colorLevel(c1) >= ColorExtension.colorLevel(c2);
    }
}

public enum Color{    
    SmallJoker, BigJoker, Space, Heart, Dimond, Club
}
