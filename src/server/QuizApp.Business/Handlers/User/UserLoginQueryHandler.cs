using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Core;
using QuizApp.Data;
using QuizApp.Models;

namespace QuizApp.Business;

public class UserLoginQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
SignInManager<User> signInManager, UserManager<User> userManager, 
IConfiguration configuration)
    : IRequestHandler<UserLoginQuery, LoginResultViewModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IConfiguration _configuration = configuration;

    private readonly SignInManager<User> _signInManager = signInManager;

    private readonly UserManager<User> _userManager = userManager;

    public async Task<LoginResultViewModel> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        // Check user exists or not
        var existingUser = await _userManager.FindByNameAsync(request.UserName);

        if (existingUser == null)
        {
            throw new ArgumentException("User with username not found");
        }

        // If user exists, check password is correct or not
        var result = await _signInManager.CheckPasswordSignInAsync(existingUser, request.Password, false);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Password is incorrect");
        }

        // Generate Token
        // Get User Information and transform to json string
        var userInformationViewModel = _mapper.Map<UserInformationViewModel>(existingUser);

        var userRoles = await _userManager.GetRolesAsync(existingUser);
        userInformationViewModel.Roles = userRoles;

        var userInformation = GetSerializeObject(userInformationViewModel); // string json

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
            new(ClaimTypes.Name, existingUser.UserName ?? string.Empty),
            new(ClaimTypes.Email, existingUser.Email ?? string.Empty),
            new(ClaimTypes.GivenName, existingUser.FirstName),
            new(ClaimTypes.Surname, existingUser.LastName),
            new(ClaimTypes.Role, string.Join(",", userRoles))
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.AppSetting.Jwt.Secret] ?? "congdinh2012@hotmail.com"));

        var token = new JwtSecurityToken(
            issuer: _configuration[Constants.AppSetting.Jwt.ValidIssuer],
            audience: _configuration[Constants.AppSetting.Jwt.ValidAudience],
            claims: claims,
            expires: _configuration[Constants.AppSetting.Jwt.ExpirationInMinutes] == null ? DateTime.Now.AddDays(1) :
                DateTime.Now.AddMinutes(Convert.ToDouble(_configuration[Constants.AppSetting.Jwt.ExpirationInMinutes])),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        var loginResultViewModel = new LoginResultViewModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = existingUser.Id.ToString(),
            UserInformation = userInformation,
            ExpiresAt = token.ValidTo,
            IssuedAt = token.ValidFrom,
        };

        return loginResultViewModel;

        // Return LoginResultViewModel
    }

    private string GetSerializeObject(object value)
    {
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        return value != null ? JsonSerializer.Serialize(value, serializeOptions) : string.Empty;
    }
}