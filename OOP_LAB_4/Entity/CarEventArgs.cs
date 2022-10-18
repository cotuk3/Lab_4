namespace OOP_LAB_4.Entity;
internal class CarEventArgs : EventArgs // 1.клас - аргумент події
{
	public TimeOnly TimeOfExcess { get; set; }
	public MyCar Car { get; set; }

	public CarEventArgs(MyCar car, TimeOnly timeOfExcess)
	{
		Car = car;
		TimeOfExcess = timeOfExcess;
	}

}
