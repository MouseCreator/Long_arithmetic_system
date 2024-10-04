namespace API.Endpoints
{
    public static class GeneralEndpoints
    {
        public static void MapGeneral_Get(this WebApplication app)
        {
            app.MapGet("/test", () =>
            {
                string response = "test";
                return Results.Json(response);
            })
            .WithName("Test")
            .WithOpenApi();

            // Add more here
        }
    }
}
