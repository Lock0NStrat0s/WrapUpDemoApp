namespace WrapUpDemo;

class Program
{
    static void Main(string[] args)
    {
        List<PersonModel> people = new List<PersonModel>
        {
            new PersonModel {FirstName = "Tim", LastName = "Corey", Email = "timc@gmail.com"},
            new PersonModel {FirstName = "Bob", LastName = "Jackson", Email = "bobj@gmail.com"},
            new PersonModel {FirstName = "Quack", LastName = "Malone", Email = "quackm@gmail.com"}
        };

        List<CarModel> cars = new List<CarModel>
        {
            new CarModel {Manufacturer = "Toyota", Model = "Corolla"},
            new CarModel {Manufacturer = "Toyota", Model = "Highlandeer"},
            new CarModel {Manufacturer = "Ford", Model = "Mustang"}
        };

        Console.ReadLine();
    }
}

public static class DataAccess
{
    public static void SaveToCSV<T> (this List<T> items) where T : new()
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
    }
}