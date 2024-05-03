using CompanionDomain.Enums;

namespace CompanionDomain.Models;

public class Character
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public Sex Sex { get; set; }
    public Gender? Gender { get; set; }
    public int Age { get; set; }
    public float Weight { get; set; }
    public Dynasty? Dynasty { get; set; }
    public Realm? Realm { get; set; }
    public Faith? Faith { get; set; }
    public Culture? Culture { get; set; }
    public int CustomizationPoints { get; set; }

    // Skill properties
    public SkillSet Skills { get; set; } = new ();

    // Trait collection
    public List<Trait> Traits { get; set; } = new ();
    
    
    public Character(string? name, Sex sex = Sex.Male, Gender? gender = null, float weight = 0)
    {
        Id = GenerateUniqueId();
        Name = name;
        Sex = sex;
        Gender = gender ?? RandomGender();
        Weight = weight;
    }
    
    private string GenerateUniqueId()
    {
        // Generate a random UUID (version 4) as the ID
        return Guid.NewGuid().ToString();
    }
    private Gender RandomGender()
    {
        Random random = new Random();
    
        // Generate a random number between 1 and 100
        int randomNumber = random.Next(1, 101);
    
        // 75% of the time, return Heterosexual (1)
        if (randomNumber <= 75)
        {
            return Enums.Gender.Heterosexual;
        }
        else
        {
            // 25% of the time, return a random value from the rest of the options
            // Assuming Gender enum has 3 values (Heterosexual, Homosexual, Bisexual, Asexual)
            return (Gender)random.Next(2, 5); // Randomly selects from Homosexual (2), Bisexual (3) and Asexual (4)
        }
    }

}