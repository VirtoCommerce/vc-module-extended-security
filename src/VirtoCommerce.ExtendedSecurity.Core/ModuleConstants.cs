using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.ExtendedSecurity.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "ExtendedSecurity:access";
            public const string Create = "ExtendedSecurity:create";
            public const string Read = "ExtendedSecurity:read";
            public const string Update = "ExtendedSecurity:update";
            public const string Delete = "ExtendedSecurity:delete";

            public static string[] AllPermissions { get; } =
            [
                Access,
                Create,
                Read,
                Update,
                Delete,
            ];
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor ExtendedSecurityEnabled { get; } = new()
            {
                Name = "ExtendedSecurity.ExtendedSecurityEnabled",
                GroupName = "ExtendedSecurity|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static SettingDescriptor ExtendedSecurityPassword { get; } = new()
            {
                Name = "ExtendedSecurity.ExtendedSecurityPassword",
                GroupName = "ExtendedSecurity|Advanced",
                ValueType = SettingValueType.SecureString,
                DefaultValue = "qwerty",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return ExtendedSecurityEnabled;
                    yield return ExtendedSecurityPassword;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
