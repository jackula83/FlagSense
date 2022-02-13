using FlagSense.FlagService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Models
{
    public class FlagValue : FsModel
    {
        public bool State { get; set; } = false;
    }
}
