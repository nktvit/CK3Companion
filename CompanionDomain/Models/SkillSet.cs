using CompanionDomain.Enums;
using CompanionDomain.Models.Records;
using SQLite;
using System.Collections.Specialized;

namespace CompanionDomain.Models;

public class SkillSet
{
    private readonly Dictionary<Skill, int> _skills = Enum.GetValues(typeof(Skill)).Cast<Skill>()
        .ToDictionary(skill => skill, _ => 0); // Initialize all skills to 0
    public IReadOnlyDictionary<Skill, int> Skills => _skills; // Expose as read-only

    public List<TraitModifierRecord> TraitModifiers { get; set; } = new List<TraitModifierRecord>();

    public List<BasicModifierRecord> BasicModifiers { get; set; } = new List<BasicModifierRecord>();

    // == Get methods ==
    public int GetSkillValue(Skill skill)
        => _skills[skill];
    
    // TraitModifiers
    public List<TraitModifierRecord> GetTraitModifiers()
        => TraitModifiers;
    
    // BasicModifiers
    public List<BasicModifierRecord> GetBasicModifiers()
        => BasicModifiers;
    
    // == Add methods ==
    
    // TraitModifiers
    public void AddTraitModifier(SkillModifier skillModifier)
    {
        TraitModifiers.Add(new TraitModifierRecord(skillModifier.TraitId, skillModifier.Modifier, skillModifier.Skill));
        RecalculateSkills();
    }
    public void AddTraitModifiers(IEnumerable<SkillModifier> skillModifiers)
    {
        TraitModifiers.AddRange(
            TraitModifiers
                .Concat(skillModifiers
                    .Select(sm
                        => new TraitModifierRecord(sm.TraitId, sm.Modifier, sm.Skill))));
        RecalculateSkills();
    }
    public void AddTraitModifierRecords(List<TraitModifierRecord> traitModifiers)
    {
        TraitModifiers.AddRange(traitModifiers);
        RecalculateSkills();
    }

    // BasicModifiers
    public void AddBasicModifier(int modifier, Skill skill)
    {
        BasicModifiers.Add(new BasicModifierRecord { Modifier = modifier, Skill = skill });
        RecalculateSkills();
    }

    // == Remove methods ==
    
    // TraitModifiers
    public void RemoveTraitModifier(SkillModifier skillModifier)
    {
        var traitId = skillModifier.TraitId;
        var skill = skillModifier.Skill;
        TraitModifiers.RemoveAll(tm => tm.TraitId == traitId && tm.Skill == skill);
        RecalculateSkills();

    }

    public void RemoveAllModifiersForTrait(int traitId)
    {
        TraitModifiers.RemoveAll(tm => tm.TraitId == traitId);
        RecalculateSkills();
    }

    public void RemoveAllTraitModifiers()
    {
        TraitModifiers.Clear();
        RecalculateSkills();
    } 
    
    // BasicModifiers
    public void RemoveBasicModifier(Skill skill)
    {
        BasicModifiers.RemoveAll(bm => bm.Skill == skill);
        RecalculateSkills();
    } 

    // == Update methods ==
    
    // TraitModifiers
    public void UpdateTraitModifiers(List<Trait> traits)
    {
        TraitModifiers.Clear();
        foreach (var trait in traits)
        {
            foreach (var modifier in trait.GetTraitModifiersRecords())
            {
                AddTraitModifier(new SkillModifier(trait.Id, modifier.Skill, modifier.Modifier));
            }
        }
    }
    
    // BasicModifiers
    public void UpdateBasicModifiers(List<BasicModifierRecord> basicModifiers)
    {
        BasicModifiers.Clear();
        BasicModifiers.AddRange(basicModifiers);
    }
    
    // == Recalculate methods ==
    
    private void RecalculateSkills()
    {
        foreach (var skill in _skills.Keys.ToList()) // Make a copy of keys to modify values
        {
            _skills[skill] = 0; // Reset skill value

            _skills[skill] += TraitModifiers.Where(tm => tm.Skill == skill).Sum(tm => tm.Modifier);
            _skills[skill] += BasicModifiers.Where(bm => bm.Skill == skill).Sum(bm => bm.Modifier);
        }
    }
    
}