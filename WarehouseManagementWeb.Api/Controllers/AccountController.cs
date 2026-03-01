using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementWeb.Infrastructure.Identity.Models;
using WarehouseManagementWeb.Infrastructure.Interfaces;

namespace WarehouseManagementWeb.Api.Controllers
{
    /// <summary>
    /// Контроллер авторизации регистрации пользователей.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Контроллер.
        /// </summary>
        /// <param name="authService">Сервис аутентификации пользователей.</param>
        /// <param name="tokenService">Сервис токенов пользователей.</param>
        public AccountController(IAuthService authService,
            ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Метод регистрирует нового пользователя.
        /// </summary>
        /// <param name="userSignUpInput">Входная модель.</param>
        /// <returns>Выходная модель.</returns>
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserSignUpInput userSignUpInput)
        {
            UserSignUpOutput result = await _authService.SignUpAsync(userSignUpInput);

            return Ok(result);
        }

        /// <summary>
        /// Метод аутентифицирует пользователя.
        /// </summary>
        /// <param name="userSignInInput">Входная модель.</param>
        /// <returns>Выходная модель.</returns>
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] UserSignInInput userSignInInput)
        {
            UserSignInOutput result = await _authService.SignInAsync(userSignInInput);

            return Ok(result);
        }

        /// <summary>
        /// Метод обновляет токены пользователя (accessToken и refreshToken).
        /// </summary>
        /// <param name="tokenInput">Входная модель.</param>
        /// <returns>Выходная модель.</returns>        
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenInput tokenInput)
        {
            TokenOutput result = await _tokenService.RefreshTokenAsync(tokenInput);

            return Ok(result);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        [Route("test")]
        public IActionResult TestAuthorize()
        {
            List<string> r = ["string", "string21"];

            return Ok(r);
        }

        /// <summary>
        /// Метод выходит из аккаунта пользователя.
        /// </summary>
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task LogoutAsync()
        {
            string? userEmail = User.Identity?.Name;

            await _authService.LogoutAsync(userEmail!);
        }
    }
}