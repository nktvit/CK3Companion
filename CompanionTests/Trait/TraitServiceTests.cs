using CompanionData;
using CompanionData.Repositories;
using CompanionData.Services;
using CompanionDomain.Utilities;
using NLog;

namespace CompanionTests.Trait;

public class TraitServiceTests
{
    private NonApplicableTraitRepository _nonApplicableTraitRepository;
    private SkillModifierRepository _skillModifierRepository;
    private TraitRepository _traitRepository;
    private TraitService _traitService;
    private Logger _logger;
    private string _databasePath;

    [SetUp]
    public void Setup()
    {
        const string path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");

        _traitRepository =
            new TraitRepository(new DatabaseConnection(_databasePath));
        _skillModifierRepository =
            new SkillModifierRepository(new DatabaseConnection(_databasePath));
        _nonApplicableTraitRepository =
            new NonApplicableTraitRepository(new DatabaseConnection(_databasePath));
        _logger = ULogging.ConfigureLogging();
        _traitService = new TraitService(_traitRepository, _skillModifierRepository,
            _nonApplicableTraitRepository, _logger);
    }

    [Test]
    public void TraitService_GetTraits()
    {
        // Arrange

        // Act
        var traits = _traitService.GetAllTraits();
        // Assert
        Assert.That(traits, Is.Not.Null);
        Assert.That(traits.Count(), Is.EqualTo(61));
    }
}