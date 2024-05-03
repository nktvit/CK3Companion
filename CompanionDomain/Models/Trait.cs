using System.ComponentModel.DataAnnotations.Schema;
using CompanionDomain.Enums;
using CompanionDomain.Interfaces;

namespace CompanionDomain.Models;

public class Trait : ITrait
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TraitType Type { get; set; }
    public int DesignerCost { get; set; } // positive, negative, or 0
    public int MinimumAge { get; set; }
    public int MaximumAge { get; set; }
    
    public Trait(int id, string name, TraitType type, int designerCost, int minimumAge, int maximumAge)
    {
        Id = id;
        Name = name;
        Type = type;
        DesignerCost = designerCost;
        MinimumAge = minimumAge;
        MaximumAge = maximumAge;
    }
}