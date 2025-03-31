using System;

public abstract class Vehicle
{
    public abstract void Drive();
}

public class Car : Vehicle
{
    public override void Drive()
    {
        Console.WriteLine("Автомобиль едет");
    }
}

public class Bicycle : Vehicle
{
    public override void Drive()
    {
        Console.WriteLine("Велосипед едет");
    }
}

public abstract class VehicleFactory
{
    public abstract Vehicle CreateVehicle();
}

public class CarFactory : VehicleFactory
{
    public override Vehicle CreateVehicle()
    {
        return new Car();
    }
}

public class BicycleFactory : VehicleFactory
{
    public override Vehicle CreateVehicle()
    {
        return new Bicycle();
    }
}

class Program
{
    static void Main(string[] args)
    {
        VehicleFactory carFactory = new CarFactory();
        Vehicle car = carFactory.CreateVehicle();
        car.Drive();
        VehicleFactory bicycleFactory = new BicycleFactory();
        Vehicle bicycle = bicycleFactory.CreateVehicle();
        bicycle.Drive();
    }
}
