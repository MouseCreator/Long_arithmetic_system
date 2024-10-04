namespace API.Service.Middlewares
{
    public class TimeoutMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);

        public TimeoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var cts = new CancellationTokenSource(_timeout))
            {
                var timeoutTask = Task.Delay(_timeout, cts.Token);

                var requestTask = _next(context);

                // wait for request proccessed, or timeout
                var completedTask = await Task.WhenAny(requestTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                    await context.Response.WriteAsJsonAsync(new { message = "Час на виконання операції вичерпано" });
                }
                else
                {
                    // if request proccessed before timeout, continue request progress
                    cts.Cancel();
                    await requestTask; // finish request proccess
                }
            }
        }
    }
}
