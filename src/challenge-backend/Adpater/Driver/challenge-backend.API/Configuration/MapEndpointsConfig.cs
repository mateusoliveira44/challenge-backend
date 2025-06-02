using challenge_backend.API.Endpoints;

namespace challenge_backend.API.Configuration
{
    public static class MapEndpointsConfig
    {
        public static void MapAllEndpoints(this WebApplication app)
        {
            app.MapUserEndpoints();
            app.MapAuthEndpoints();
            app.MapWalletEndpoints();
        }
    }
}
