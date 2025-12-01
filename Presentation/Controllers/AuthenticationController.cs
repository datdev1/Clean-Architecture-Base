using Application.DTOs.Customer;
using Application.Interface.Service;
using AutoMapper;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extension;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDTO dto)
        {
            var result = await _authService.Login(dto);
            return result.ToActionResult();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerRegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return result.ToActionResult();
        }
    }
}
