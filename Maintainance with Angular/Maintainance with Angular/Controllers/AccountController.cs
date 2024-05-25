using Azure.Core;
using Maintainance_with_Angular.DTOs;
using Maintainance_with_Angular.Extensions;
using Maintainance_with_Angular.Models.Identity;
using Maintainance_with_Angular.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Maintainance_with_Angular.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		public AccountController(ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_tokenService = tokenService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("Login")]
		public async Task<Result<UserDetailsDto>> Login(LoginDto loginQuery) 
		{
			var user = await _userManager.Users
							    .AsQueryable()
								.AsSplitQuery()
							    .FirstOrDefaultAsync(x => x.Email == loginQuery.Email && !x.IsDeleted);

			if (user == null) return Result<UserDetailsDto>.Failure("User not found!");
			var result = await _signInManager
						  .CheckPasswordSignInAsync(user, loginQuery.Password, false);

			if (!result.Succeeded) return Result<UserDetailsDto>.Failure("Password invalid!");

			var userDetails = new UserDetailsDto
			{
				Username = user.UserName,
				Token = await _tokenService.CreateToken(user),
				Email = user.Email,
			};

			return Result<UserDetailsDto>.Success(userDetails);
		}

		[HttpPost("Register")]
		public async Task<Result<UserDetailsDto>> Register([FromBody] RegisterDto registerDto)
		{
			if (_userManager.Users.Any(x => x.Email == registerDto.Email && !x.IsDeleted))
				Result<UserDetailsDto>.Failure("User exist before!");



			var user =new AppUser
			{
				Email = registerDto.Email,
				UserName = registerDto.UserName,
				PhoneNumber = registerDto.PhoneNumber,
			};

			var result = await _userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded) return Result<UserDetailsDto>.Failure(result.Errors.ToErrorString());

			var userDetails = new UserDetailsDto
			{
				Username = user.UserName,
				Email = user.Email,
				Token = await _tokenService.CreateToken(user),
			};

			return Result<UserDetailsDto>.Success(userDetails);
		}
	}
}
