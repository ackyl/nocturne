using System;

[System.Serializable]
public enum Identity
{
    Civilian,
    Agent,
    Enemy,
    Secret
}

public static class IdentityHelper
{
    public static Identity StringToIdentitySafe(string identityString)
    {
        if (Enum.TryParse(identityString, true, out Identity identity))
        {
            return identity; // Successfully parsed
        }

        // Return default if invalid
        return Identity.Civilian;
    }

}
