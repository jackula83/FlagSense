using FlagSense.FlagService.Domain.Models.Abstracts;
using System.Drawing;

namespace FlagSense.FlagService.Domain.Models
{
    public class FsEnvironment : FsEntity
    {
        public static int DefaultColour = Color.DarkRed.ToArgb();

        public int Colour { get; set; } = DefaultColour;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Flag> Flags { get; set; } = new();
    }
}
