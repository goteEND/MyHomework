using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHomework.API.Dtos;
using MyHomework.API.Entities;
using MyHomework.API.Services;

namespace MyHomework.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register(UserForRegistrationDto registerDto)
        {
            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.FirstName.ToLower() + registerDto.LastName.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Student");

            if (!roleResult.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Registration successful!");
        }


        [HttpPost("authentication")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(usr => usr.UserName == loginDto.Username.ToLower());

            if (user == null)
                return Unauthorized("Invalid username.");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid password.");

            var userForReturn = new UserForReturnDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

            return Ok(userForReturn);
        }
    }
}
