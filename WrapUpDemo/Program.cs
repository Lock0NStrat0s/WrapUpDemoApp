namespace WrapUpDemo;

class Program
{
    static void Main(string[] args)
    {
        List<PersonModel> people = new List<PersonModel>
        {
            new PersonModel {FirstName = "Tim", LastName = "Coreydarnit", Email = "timc@gmail.com"},
            new PersonModel {FirstName = "Bob", LastName = "Jackson", Email = "bobj@gmail.com"},
            new PersonModel {FirstName = "Quack", LastName = "Malone", Email = "quackm@gmail.com"}
        };

        List<CarModel> cars = new List<CarModel>
        {
            new CarModel {Manufacturer = "Toyota", Model = "Corolla"},
            new CarModel {Manufacturer = "Toyotaheck", Model = "Highlander"},
            new CarModel {Manufacturer = "Ford", Model = "Mustang"}
        };


        DataAccess<PersonModel> peopleData = new DataAccess<PersonModel>();
        peopleData.BadEntryFound += PeopleData_BadEntryFound;
        peopleData.SaveToCSV(people, @"C:\Users\moham\OneDrive\Documents\a_Programming\IAMTIMCOREY\C#_MASTER_COURSE\6.8_MiniProject_Generics_Events\WrapUpDemoApp\people.csv");
        
        DataAccess<CarModel> carData = new DataAccess<CarModel>();
        carData.BadEntryFound += CarData_BadEntryFound;
        carData.SaveToCSV(cars, @"C:\Users\moham\OneDrive\Documents\a_Programming\IAMTIMCOREY\C#_MASTER_COURSE\6.8_MiniProject_Generics_Events\WrapUpDemoApp\cars.csv");

        Console.ReadLine();
    }

    private static void CarData_BadEntryFound(object? sender, CarModel e)
    {
        Console.WriteLine($"Bad entry found for {e.Manufacturer} {e.Model}");
    }

    private static void PeopleData_BadEntryFound(object? sender, PersonModel e)
    {
        Console.WriteLine($"Bad entry found for {e.FirstName} {e.LastName}");
    }
}

public class DataAccess<T> where T : new()
{
    public event EventHandler<T> BadEntryFound;

    // using reflection
    public void SaveToCSV(List<T> items, string filePath)
    {
        List<string> rows = new List<string>();
        T entry = new T();
        var cols = entry.GetType().GetProperties();

        string row = "";
        foreach (var col in cols)
        {
            row += $",{col.Name}";
        }
        row = row.Substring(1);
        rows.Add(row);

        foreach (var item in items)
        {
            row = "";
            bool badWordDetected = false;
            foreach (var col in cols)
            {
                string val = col.GetValue(item, null).ToString();

                badWordDetected = BadWordDetector(val);
                if (badWordDetected)
                {
                    BadEntryFound?.Invoke(this, item);
                    break;
                }

                row += $",{val}";
            }

            if (!badWordDetected)
            {
                row = row.Substring(1);
                rows.Add(row);
            }
        }

        File.WriteAllLines(filePath, rows);
    }

    private bool BadWordDetector(string stringToTest)
    {
        bool output = false;
        string lowerCaseTest = stringToTest.ToLower();

        if (lowerCaseTest.Contains("darn") || lowerCaseTest.Contains("heck"))
        {
            output = true;
        }

        return output;
    }
}