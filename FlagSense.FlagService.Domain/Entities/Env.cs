using Common.Domain.Core.Extensions;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FlagSense.FlagService.Domain.Entities
{
    public class Env : FsEntity, IColourCoding
    {
        public static int DefaultColour = Color.OrangeRed.ToArgb();
        public int ColourCoding { get; set; } = DefaultColour;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Flag> Flags { get; set; } = new();
        public List<Segment> Segments { get; set; } = new();
        public List<User> Users { get; set; } = new();

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<Env>();
            entity
                .Tap(x => x.HasMany(e => e.Flags))
                .Tap(x => x.HasMany(e => e.Segments))
                .Tap(x => x.HasMany(e => e.Users));
        }
    }
}
