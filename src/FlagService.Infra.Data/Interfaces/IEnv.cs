using Framework2.Infra.Data.Entity;

namespace FlagService.Infra.Data.Interfaces
{
    public interface IEnv : IAggregateRoot
    {
        int ColourCoding { get; set; }

        string Name { get; set; }
        string Description { get; set; }

        List<ISegment> Segments { get; set; }
    }
}
