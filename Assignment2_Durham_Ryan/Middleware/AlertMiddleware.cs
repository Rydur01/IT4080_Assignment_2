namespace Assignment2_Durham_Ryan.Middleware
{
    public class AlertMiddleware
    {
        RequestDelegate next;

        public AlertMiddleware(RequestDelegate Next)
        {
            next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Do something

            await next(context);
        }
    }
}
