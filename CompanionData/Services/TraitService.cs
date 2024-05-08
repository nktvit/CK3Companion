using CompanionData.Repositories;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Services;

public class TraitService
{
    private readonly TraitRepository _traitRepository;
    private readonly SkillModifierRepository _skillModifierRepository;
    private readonly NonApplicableTraitRepository _nonApplicableTraitRepository;
    private readonly Logger _logger;

    public TraitService(TraitRepository traitRepository, SkillModifierRepository skillModifierRepository,
        NonApplicableTraitRepository nonApplicableTraitRepository, Logger logger)
    {
        _traitRepository = traitRepository;
        _skillModifierRepository = skillModifierRepository;
        _nonApplicableTraitRepository = nonApplicableTraitRepository;
        _logger = logger;
    }

    public IEnumerable<Trait> GetAllTraits()
    {
        try
        {
            _logger.Info("Starting to fetch traits...");
            var _traits = _traitRepository.GetTraits();
            _logger.Info("Finished fetching traits.");

            foreach (var trait in _traits)
            {
                _logger.Info($"Fetching skill modifiers for trait: {trait.Name}");
                trait.SkillModifiers = _skillModifierRepository.GetTraitSkillModifiers(trait.Id);

                _logger.Info($"Fetching non-applicable traits for trait: {trait.Name}");
                trait.NonApplicableTraits = _nonApplicableTraitRepository.GetTraitNonApplicableTraits(trait);
            }

            _logger.Info("Finished fetching all data.");
            return _traits;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching data: {ex.Message}");
            return Enumerable.Empty<Trait>();
        }
    }

    public Trait GetTraitById(int id)
    {
        return _traitRepository.GetTraitById(id);
    }
}