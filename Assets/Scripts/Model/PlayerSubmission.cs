using System;

[System.Serializable]
public class PlayerSubmission
{
    public string characterId;

    public Identity identity;

    public bool submissionResult;

    public bool secretEnding;

    public void MapSubmission(CardType cardType)
    {
        switch (identity)
        {
            case Identity.Civilian:
            case Identity.Enemy:
                submissionResult = cardType == CardType.Standard;
                secretEnding = false;
                break;

            case Identity.Agent:
                submissionResult = cardType == CardType.VIP;
                secretEnding = false;
                break;

            case Identity.Secret:
                submissionResult = true; // Always true for Secret
                secretEnding = cardType == CardType.VIP; // True only if VIP
                break;

            default:
                throw new ArgumentOutOfRangeException($"Unhandled Identity: {identity}");
        }
    }

    public void MapFinalDaySubmission(CardType cardType)
    {
        if (cardType == CardType.Standard)
        {
            submissionResult = true;
        }
        else
        {
            submissionResult = false;
        }
    }
}
