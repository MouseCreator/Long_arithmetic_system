using API.Service;

namespace API.Endpoints
{
    public static class GeneralEndpoints
    {
        public static void MapGeneral_Get(this WebApplication app)
        {
            app.MapPost("S/test", () =>
            {
                var data = "Тестовий запит пройшов успішно";
                return Results.Json(ApiResponse<string>.SuccessResponse(data));
            })
            .WithName("Test")
            .WithOpenApi();

            app.MapPost("S/error", () =>
            {
                return Results.Json(ApiResponse<string>.ErrorResponse("Щось пішло не так"));
            })
            .WithName("Error")
            .WithOpenApi();

            //no difference if used SuccessResponse or ErrorResponse when time is exceeded, TimeoutMiddleware handles it as ErrorResponse
            app.MapGet("S/testTimeout", async () =>
            {
                var data = "Тестовий запит пройшов успішно";
                await Task.Delay(5000);

                return Results.Json(ApiResponse<string>.SuccessResponse(data));
            })
            .WithName("TestTimeout")
            .WithOpenApi();
        }
    }
}
