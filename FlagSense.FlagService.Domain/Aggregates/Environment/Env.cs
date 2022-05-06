using FlagSense.FlagService.Domain.Entities;
using FlagService.Domain.Interfaces;
using FlagService.Infra.Data.Abstracts;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FlagService.Domain.Aggregates.Environment
{
    public class Env : FsDataObject, IColourCoding, IAggregateRoot
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
