using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FlagSense.FlagService.Domain.Entities
{
    public class Env : FsEntity, IColourCoding, IAuditable
    {
        public static int DefaultColour = Color.OrangeRed.ToArgb();
        public int ColourCoding { get; set; } = DefaultColour;

        [StringLength(0x200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(0x10000)]
        public string Description { get; set; } = string.Empty;

        public List<Segment> Segments { get; set; } = new();

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Env>();
            entity.HasMany(e => e.Segments);
        }
    }
}
