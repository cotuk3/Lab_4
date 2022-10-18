namespace OOP_LAB_4.Entity;
internal static class HandleEventMethods
{
    //public static void AmountOfLowerCaseLetters(object? sender, CarEventArgs e)
    //{

    //}

    public static EventHandler<CarEventArgs> AmountOfLowerCaseLetters = delegate (object? sender, CarEventArgs e)
    {
        int amount = 0;
        foreach(char c in e.Car.Name)
        {
            if(c >= 97 && c <= 122)
                amount++;
        }
        Console.WriteLine(e.Car.Name + $" exceeded max speed {e.Car._maxSpeed} on {e.TimeOfExcess.ToLongTimeString()}");
        Console.WriteLine($"Car name {e.Car.Name} has {amount} lower case letters\n");
    };


}
