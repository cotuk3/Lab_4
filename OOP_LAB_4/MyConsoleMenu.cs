using System.Collections;

namespace OOP_LAB_4;
internal class MyConsoleMenu : ConsoleMenu.ConsoleMenu
{
    private MyCar? car;
    private Dictionary<string, Action> commands = new Dictionary<string, Action>();
    private Exception noCar = new Exception("You don't have a car!");
    public MyConsoleMenu()
    {
        commands.Add("/info", () => Info());
        commands.Add("/create", () => Create());
        commands.Add("/create def", () => Create(true));
        commands.Add("/move", () => Move());
        commands.Add("/stop", () => Stop());
        commands.Add("/refuel", () => Refuel());
        commands.Add("/show", () => Show(new List<MyCar> { car }));
        commands.Add("/cls", () => { Console.Clear(); Info(); });
        commands.Add("/end", () => { Console.WriteLine("Bye, have a good time!"); Console.ReadLine(); });
    }


    public override void Start()
    {
        string input = "";
        commands["/info"]();
        do
        {

            try
            {
                Console.Write("Enter a command: ");
                input = Console.ReadLine();

                if(int.TryParse(input, out int id))
                    switch(id)
                    {
                        case 1:
                        input = "/info";
                        break;
                        case 2:
                        input = "/create";
                        break;
                        case 3:
                        input = "/move";
                        break;
                        case 4:
                        input = "/stop";
                        break;
                        case 5:
                        input = "/refuel";
                        break;
                        case 6:
                        input = "/show";
                        break;
                        case 7:
                        input = "/cls";
                        break;
                        case 8:
                        input = "/end";
                        break;
                        case 21:
                        input = "/create def";
                        break;
                    }

                if(commands.ContainsKey(input))
                    commands[input]();
                else
                    throw new Exception("Uknown command");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } while(input != "/end");
    }

    public override void Info()
    {
        string info = "***** Events *****\n" +
            "1./info\n" +
            "2./create\n" +
            "  2.1./create def\n" +
            "3./move\n" +
            "4./stop\n" +
            "5./refuel\n" +
            "6./show\n" +
            "7./cls\n" +
            "8./end\n";
        Console.WriteLine(info);
    }

    public override void Show(IEnumerable collection)
    {
        foreach(var car in collection)
        {
            Console.WriteLine(car);
        }
    }

    private void Refuel()
    {
        if(car == null)
            throw noCar;
        car.Refueling();
        commands["/show"]();
    }

    private void Stop()
    {
        if(car == null)
            throw noCar;
        do
        {
            Console.WriteLine(car);
        } while(car.StopMoving());
    }

    private void Move()
    {
        if(car == null)
            throw noCar;
        do
        {
            Console.WriteLine(car);
        } while(car.StartToMove());
    }
    private void Create(bool def = false)
    {
        if(def)
        {
            car = new MyCar(150, 65, "My Sport Car");
        }
        else
        {
            Console.Write("Enter a name of the car:");
            string name = Console.ReadLine();

            Console.Write($"Enter max speed of {name}: ");
            string maxSpeedS = Console.ReadLine();
            double maxSpeed;
            do
            {
                if(double.TryParse(maxSpeedS, out maxSpeed))
                    break;
                else
                    Console.Write("Try again:");
                maxSpeedS = Console.ReadLine();
            } while(true);

            Console.Write($"Enter fuel capacity of the {name}: ");
            string fuelCapacityS = Console.ReadLine();
            double fuelCapacity;
            do
            {
                if(double.TryParse(fuelCapacityS, out fuelCapacity))
                    break;
                else
                    Console.Write("Try again:");
                fuelCapacityS = Console.ReadLine();
            } while(true);

            car = new MyCar(maxSpeed, fuelCapacity, name);
        }
        AddHandlers();
    }
    
    private void AddHandlers()
    {
        car.MyEvent += HandleEventMethods.TimeWhenExceeded;
        car.MyEvent += HandleEventMethods.AmountOfLowerCaseLetters;

        car.MyEvent += (sender, e) =>
        {
            int amount = 0;
            foreach(char c in e.Car.Name)
            {
                if(c >= 97 && c <= 122)
                    amount++;
            }
            Console.WriteLine($"Lambda expression: {e.Car.Name} exceeded max speed {e.Car._maxSpeed} on {e.TimeOfExcess.ToLongTimeString()}");
            Console.WriteLine($"Car name {e.Car.Name} has {amount} lower case letters");
        };
    }
}
