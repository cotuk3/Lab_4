namespace OOP_LAB_4.Entity;


internal class MyCar // 2.клас в якому генерується подія
{

    public readonly double _maxSpeed;
    double _currentSpeed;

    double _fuelCapacity;
    double _currentFuelAmount;

    public event EventHandler<CarEventArgs> MyEvent = null;

    public MyCar(double maxSpeed, double fuelCapacity, string name, double currentFuelAmount = -1)
    {
        _maxSpeed = maxSpeed;
        _fuelCapacity = fuelCapacity;
        _currentFuelAmount = currentFuelAmount == -1 ? fuelCapacity : currentFuelAmount;
        Name = name;
    }


    #region Properties

    public double CurrentSpeed
    {
        get => _currentSpeed;
    }

    public double CurrentFuelAmount
    {
        get => _currentFuelAmount;
    }
    public string Name
    {
        get; set;
    }

    #endregion

    #region Move

    public bool StartToMove()
    {
        if(_currentSpeed != 0 && _currentSpeed >= _maxSpeed / 10)
        {
            return Move();
        }
        else
        {
            if(_currentSpeed < _maxSpeed / 10)
            {
                Thread.Sleep(300);
                _currentSpeed += 2.5;

                FuelСonsumption();

                return true;
            }
            return false;
        }
    }

    public bool Move()
    {
        if(_currentSpeed > _maxSpeed)
        {
            TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);
            OnMaximumSpeedExcess(this, time);
            return false;
        }

        Thread.Sleep(150);
        _currentSpeed += 10;
        FuelСonsumption(1.5);

        return true;
    }

    public bool StopMoving()
    {

        Thread.Sleep(150);
        if(CurrentSpeed > 0)
        {
            _currentSpeed = _currentSpeed - 10 < 0 ? 0 : _currentSpeed - 10;

            return true;
        }

        if(CurrentFuelAmount == 0)
            Refueling(_fuelCapacity);
        return false;
    }

    #endregion

    #region Fuel

    public void Refueling(double gasAmount = -1)
    {
        if(gasAmount == -1)
            _currentFuelAmount = _fuelCapacity;
        else
            _currentFuelAmount += gasAmount;

        if(_currentFuelAmount > _fuelCapacity)
            _currentFuelAmount = _fuelCapacity;
    }

    public void FuelСonsumption(double coefficient = 1) // TODO : check if lower than 0
    {
        _currentFuelAmount -= 2 * coefficient;
        if(_currentFuelAmount <= 0)
        {
            throw new Exception("Your car is out of fuel");
        }
    }

    #endregion

    private void OnMaximumSpeedExcess(MyCar myCar, TimeOnly time)
    {
        if(MyEvent != null)
        {
            CarEventArgs args = new CarEventArgs(myCar, time);
            MyEvent(this, args);
        }
    }

    public override string ToString()
    {
        return $"{Name} : curr: {_currentSpeed} km/h, max: {_maxSpeed} km/h\n" +
               $"\t\tcurr: {_currentFuelAmount} l, max: {_fuelCapacity} l.";
    }
}
