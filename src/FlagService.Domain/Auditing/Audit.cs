using FlagService.Core.Models;

namespace FlagService.Domain.Auditing
{
    public class Audit : FsModel
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public int RefId { get; set; }
        public string? Old { get; set; }
        public string New { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
    }
}
