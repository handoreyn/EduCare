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

var users = generator.Generate(3_000_000);

var userCsvData = new StringBuilder();
var fileChunks = users.Count / 100_000;
var lines = new Dictionary<string, string>();
var emails = new HashSet<string>();
var count = 0;
for (var i = 0; i < users.Count; ++i)
{
    var u = users[i];
    if (emails.Contains(u.Email)) continue;

    if (lines.TryAdd(u.Email, $",{u.Email},{u.Password},{u.FullName},{u.BirthDate.ToString("MM/dd/yyyy")},{u.CreateDate.ToString("MM/dd/yyyy")},{u.UpdateDate.ToString("MM/dd/yyyy")},2"))
    {
        ++count;
        emails.Add(u.Email);
    }
    if (count % 1_000_000 == 0)
    {
        count = 0;
        await File.AppendAllLinesAsync($"{Guid.NewGuid()}.csv", lines.Values);
        lines = new Dictionary<string, string>();
    }
}

await File.AppendAllLinesAsync($"{Guid.NewGuid()}.csv", lines.Values);
