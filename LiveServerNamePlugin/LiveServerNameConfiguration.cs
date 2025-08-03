using AssettoServer.Server.Configuration;

namespace LiveServerNamePlugin;

public class LiveServerNameConfiguration : IValidateConfiguration<LiveServerNameConfigurationValidator>
{
    public int UpdateInterval { get; init; } = 10;
    public bool Randomize { get; init; } = false;
    public List<string> ListOfNames { get; init; } = new();
}
