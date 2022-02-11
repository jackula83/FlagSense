using FlagSense.FlagService.Domain.Models.Abstracts;

namespace FlagSense.FlagService.Domain.Models
{
    public class FlagValue : FsEntity
    {
        public bool State { get; set; } = false;
    }
}
