using FlagSense.FlagService.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlagSense.FlagService.Domain.Models
{
    public class FlagValue : FsModel
    {
        public bool State { get; set; } = false;
    }
}
