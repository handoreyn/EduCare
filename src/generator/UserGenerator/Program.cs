using System.Text;
using Bogus;
using BuildingBlocks.Enums;
using EduCare.User.Core.Entities;

var faker = new Faker<User>();


var generator = faker.RuleFor(l => l.Id, f => f.IndexFaker)
    .RuleFor(l => l.Email, f => f.Person.Email.ToLower())
    .RuleFor(l => l.Password, f => f.Internet.Password(10))
    .RuleFor(l => l.FullName, f => f.Person.FullName)
    .RuleFor(l => l.BirthDate, f => f.Person.DateOfBirth)
    .RuleFor(l => l.CreateDate, f => f.Date.Past(1))
    .RuleFor(l => l.UpdateDate, f => f.Date.Past(1))
    .RuleFor(l => l.Status, f => f.Random.Enum<StatusEnumType>());

var users = generator.Generate(1_000_000);

var userCsvData = new StringBuilder();
var fileChunks = users.Count / 100_000;
var lines = new List<string>();
// userCsvData.AppendLine($"1,fatihgencaslan@yahoo.com,g9f11e12,Fatih Gençaslan,{DateTime.Now},{DateTime.Now},{DateTime.Now},1");
lines.Add($"1,fatihgencaslan@yahoo.com,g9f11e12,Fatih Gençaslan,{DateTime.Now},{DateTime.Now},{DateTime.Now},1");
for (var i = 0; i < users.Count; ++i)
{
    var u = users[i];
    lines.Add($"{i + 1},{u.Email},{u.Password},{u.FullName},{u.BirthDate},{u.CreateDate},{u.UpdateDate},2");
    //     userCsvData.AppendLine($"{users.IndexOf(u) + 2},{u.Email},{u.Password},{u.FullName},{u.BirthDate},{u.CreateDate},{u.UpdateDate},2");
    Console.WriteLine(i);
}
await File.AppendAllLinesAsync("users.csv", lines);
// foreach (var u in users)
// {
//     userCsvData.AppendLine($"{users.IndexOf(u) + 2},{u.Email},{u.Password},{u.FullName},{u.BirthDate},{u.CreateDate},{u.UpdateDate},2");
// }

// await File.WriteAllTextAsync($"users{DateTime.Now.ToString("yyyyMMdd")}.csv", userCsvData.ToString());