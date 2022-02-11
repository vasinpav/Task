namespace Data
{
	internal class Car
	{
		public string? Brand { get; }

		public ushort? YearOfIssue { get; }

		public float? EngineCapacity { get; }

		public ushort? NumDoors { get; }

		public Car(string? brand, ushort? yearOfIssue, float? engineCapacity, ushort? numDoors)
		{
			Brand = brand;
			YearOfIssue = yearOfIssue;
			EngineCapacity = engineCapacity;
			NumDoors = numDoors;
		}
	}
}