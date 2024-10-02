using Bogus;
using CriticalSectionTask;
using System.Text.Json;

class Program
{
    static List<User> users = new List<User>();
    static object lockObject = new object();

    public static WaitCallback ReadJsonFile { get; private set; }

    static void ReadJsonFile(object filePath)
    {
        string path = (string)filePath;
        string jsonString = File.ReadAllText(path);
        var userList = JsonSerializer.Deserialize<List<User>>(jsonString);
        if (userList != null)
        {
            lock (lockObject) // Eş zamanlı erişim için kilitle
            {
                users.AddRange(userList);
            }
        }
        static void Main(string[] args)
        {
            var faker = new Faker<User>();
            var users = faker.RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Surname, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .Generate(50);

            //var jsonFile =  JsonSerializer.Serialize(users);
            //File.WriteAllText("5.json",jsonFile);




            Console.WriteLine("1.Single Thread\n2.Multiple Thread");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    string[] jsonFilePaths = { "1.json", "2.json", "3.json", "4.json", "5.json" };

                    List<User> user = new List<User>();

                    foreach (var path in jsonFilePaths)
                    {
                        string jsonString = File.ReadAllText(path);
                        var userList = JsonSerializer.Deserialize<List<User>>(jsonString);
                        if (userList != null)
                        {
                            users.AddRange(userList);
                        }


                    }
                    foreach (var user1 in users)
                    {
                        Console.WriteLine($"Name: {user1.Name}, Surname: {user1.Surname}, Email: {user1.Email}, DateOfBirth: {user1.DateOfBirth}");
                    }
                    break;
                case "2":
                    string[] jsonFilePaths1 = { "1.json", "2.json", "3.json", "4.json", "5.json" };

                    foreach (var filePath in jsonFilePaths1)
                    {
                        ThreadPool.QueueUserWorkItem(ReadJsonFile, filePath);
                    }
                    Thread.Sleep(2000);

                    break;
            }

        }


    }
}