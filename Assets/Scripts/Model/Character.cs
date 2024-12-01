using System;

[System.Serializable]
public class Character
{
    public int number;
    public string id;
    public string fullName;
    public Identity identity;
    public Document doc;

    public override string ToString()
    {
        return $"Character:\n" +
               $"  Number: {number}\n" +
               $"  ID: {id}\n" +
               $"  Full Name: {fullName}\n" +
               $"  Identity: {identity}\n" +
               $"  {doc}";
    }
}
