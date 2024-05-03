using CompanionDomain.Enums;

namespace CompanionDomain.Interfaces;

public interface ITrait
{
    int Id { get; set; }
    string Name { get; set; }
    TraitType Type { get; set; }
    int DesignerCost { get; set; } // positive, negative, or 0
    int MinimumAge { get; set; }
    int MaximumAge { get; set; }
}