using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace boulderlog.Domain.Email
{
    public class AppEmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public AppEmailConfirmationTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<EmailConfirmationTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
        {

        }
    }
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            Name = "EmailDataProtectorTokenProvider";
            TokenLifespan = TimeSpan.FromHours(4);
        }
    }
}
