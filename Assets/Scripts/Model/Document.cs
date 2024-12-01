using System;

[System.Serializable]
public class Document
{
    public string name;
    public string birth;
    public string nationality;
    public string id;
    public string contact;
    public string checkInDate;
    public string checkOutDate;
    public string roomType;
    public string request;
    public bool signature;
    public bool mark;

    public override string ToString()
    {
        return $"Document:\n" +
               $"  Name: {name}\n" +
               $"  Birth: {birth}\n" +
               $"  Nationality: {nationality}\n" +
               $"  ID: {id}\n" +
               $"  Contact: {contact}\n" +
               $"  Check-In Date: {checkInDate}\n" +
               $"  Check-Out Date: {checkOutDate}\n" +
               $"  Room Type: {roomType}\n" +
               $"  Request: {request}\n" +
               $"  Signature: {signature}\n" +
               $"  Mark: {mark}";
    }
}
