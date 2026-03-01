using System.Net;

namespace WarehouseManagementWeb.Api.Middlewares
{
    /// <summary>
    /// Класс перехвата и обработки исключений.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="next">Запрос.</param>
        /// <param name="logger">Логгер.</param>
        /// <param name="env">Окружение.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Метод перехвата запроса.
        /// </summary>
        /// <param name="context">Контекст запроса.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Метод обрабатывает исключение и формирует HTTP ответ.
        /// </summary>
        /// <param name="context">Контекст запроса.</param>
        /// <param name="exception">Исключение.</param>
        /// <returns>Обработанное исключение.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = ((int)HttpStatusCode.BadRequest, exception.Message);

            //int statusCode = exception switch
            //{
            //    UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            //    InvalidOperationException => (int)HttpStatusCode.BadRequest,

            //    _ => (int)HttpStatusCode.InternalServerError
            //};

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = statusCode,
                Message = exception.Message,
                Details = _env.IsDevelopment()
                    ? exception.ToString()
                    : null
            });
        }
    }
}