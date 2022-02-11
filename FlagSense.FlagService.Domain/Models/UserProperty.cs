using FlagSense.FlagService.Domain.Models.Abstracts;

namespace FlagSense.FlagService.Domain.Models
{
    public class UserProperty : FsEntity
    {
        public int UserId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public UserProperty() { }

        public UserProperty(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
