using UnityEngine;

public static class ColorPalette
{
    public static readonly Color Black = HexToColor("#04131a");

    public static readonly Color Green1 = HexToColor("#154746");

    public static readonly Color Green2 = HexToColor("#458b73");

    public static readonly Color Green3 = HexToColor("#64a680");

    public static readonly Color Green4 = HexToColor("#aee0ab");

    public static readonly Color White = HexToColor("#efffe4");

    public static readonly Color Yellow = HexToColor("#fff2b3");

    public static readonly Color Red1 = HexToColor("#e57373");

    public static readonly Color Red2 = HexToColor("#cb3d65");

    public static readonly Color Red3 = HexToColor("#94003a");

    // Add more colors as needed...

    // Helper method to convert hex to Color
    private static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
        {
            return color;
        }
        else
        {
            Debug.LogError($"Invalid hex color: {hex}");
            return Color.black; // Fallback color
        }
    }
}
