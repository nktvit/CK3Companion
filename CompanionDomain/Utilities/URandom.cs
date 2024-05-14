using CompanionDomain.Enums;

namespace CompanionDomain.Utilities;

public static class URandom
{
    public static string GenerateUniqueId()
    {
        // Generate a random UUID (version 4) as the ID
        return Guid.NewGuid().ToString();
    }

}