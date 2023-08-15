namespace Havillah_SchoolManagement_System.ServiceCollections
{
    public static class AuthenticationService
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetSection("IdentityOptions").GetSection("IdentityUrl").Value;
                options.Audience = "travisterApiLoanApi";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    NameClaimType = "given_name",
                    RoleClaimType = "role",
                    ValidTypes = new[] { "at+jwt" }
                };

            });*/
            return services;
        }
    }
}
