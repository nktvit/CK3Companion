using CompanionDomain.Enums;
using CompanionDomain.Utilities;
using SQLite;

namespace CompanionDomain.Models;

[Table("Characters")]
public class Character
{
    [PrimaryKey]
    [Column("id")]
    public string Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string? Name { get; set; }

    [MaxLength(50)]
    [Column("nickname")]
    public string? Nickname { get; set; }

    [Column("sex")]
    public Sex Sex { get; set; }

    [Column("gender")]
    public Gender? Gender { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("weight")]
    public float Weight { get; set; }

    [Column("dynasty_id")]
    public string? DynastyId { get; set; }

    [Ignore]
    public Dynasty? Dynasty { get; set; }

    [Column("house_id")]
    public string? HouseId { get; set; }

    [Ignore]
    public DynastyHouse? House { get; set; }

    [Column("realm_id")]
    public string? RealmId { get; set; }

    [Ignore]
    public Realm? Realm { get; set; }

    [Column("faith_id")]
    public string? FaithId { get; set; }

    [Ignore]
    public Faith? Faith { get; set; }

    [Column("culture_id")]
    public string? CultureId { get; set; }

    [Ignore]
    public Culture? Culture { get; set; }

    [Column("customization_points")]
    public int CustomizationPoints { get; set; }

    [Ignore]
    private SkillSet Skills { get; set; } = new();

    [Ignore]
    public List<Trait> Traits { get; set; } = new();

    public Character()
    {
        Id = URandom.GenerateUniqueId();
    }

    public Character(string? name, Sex sex = Sex.Male, Gender? gender = null, float weight = 0)
    {
        Id = URandom.GenerateUniqueId();
        Name = name;
        Sex = sex;
        Gender = gender ?? RandomGender();
        Weight = weight;
    }

    private Gender RandomGender()
    {
        var random = new Random();

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
    public int GetSkillValue(Skill skill)
    {
        return Skills.GetSkillValue(skill);
    }
    public void AddTrait(Trait trait)
    {
        if (Traits.Contains(trait))
        {
            throw new ArgumentException("Trait already exists in the list.", nameof(trait)); 
        }

        Traits.Add(trait);
        Skills.AddTraitModifiers(trait.SkillModifiers);
        
    }

    public void AddTraits(IEnumerable<Trait> traits)
    {
        if (traits.Any(x => Traits.Contains(x)))
        {
            var duplicateTraits = traits.Where(x => Traits.Contains(x)).Select(x => x.Name);
            throw new ArgumentException("One or more traits already exist in the list.", nameof(duplicateTraits));
        }
        Traits.AddRange(traits);
        Skills.AddTraitModifiers(traits.SelectMany(x => x.SkillModifiers));
    }
    
    public void RemoveTraitById(int traidId)
    {
        // Remove trait from list
        Traits.RemoveAll(x => x.Id == traidId);
        // Remove trait modifiers from skills
        Skills.RemoveAllModifiersForTrait(traidId);
    }
    public void RemoveTrait(Trait trait)
    {
        Traits.Remove(trait);
        Skills.RemoveAllModifiersForTrait(trait.Id);
    }
    public void RemoveAllTraits()
    {
        Traits.Clear();
        Skills.RemoveAllTraitModifiers();
    }
}