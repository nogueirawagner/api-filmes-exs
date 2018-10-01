using Exs.Domain.Interfaces;
using Exs.Infra.Identity.Authorization;
using Exs.Infra.Identity.Models;
using Exs.Infra.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exs.Api.Controllers
{
  public class ContaController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly TokenDescriptor _tokenDescriptor;

    public ContaController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        TokenDescriptor tokenDescriptor,
        IUser user) : base(user)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _tokenDescriptor = tokenDescriptor;
    }

    private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    [HttpPost]
    [AllowAnonymous]
    [Route("nova-conta")]
    public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest();

      var user = await _userManager.FindByNameAsync(model.Cpf);
      if (user == null)
      {
        user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = model.Cpf };
        var result = await _userManager.CreateAsync(user, model.Senha);

        if (!result.Succeeded)
        {
          StringBuilder erros = new StringBuilder();
          foreach (var error in result.Errors)
            erros.Append(error.Description);

          return BadRequest(erros.ToString());
        }
      }

      var response = await GerarToken(model.Cpf);
      return Ok(response);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("conta")]
    public async Task<IActionResult> Login([FromBody]LoginViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest(model);

      var user = await _userManager.FindByNameAsync(model.Cpf);
      if (user == null)
      {
        return BadRequest("Favor realizar o cadastro");
      }

      var result = await _signInManager.PasswordSignInAsync(model.Cpf, model.Senha, false, lockoutOnFailure: true);

      if (result.Succeeded)
      {
        var response = await GerarToken(model.Cpf);
        return Ok(response);
      }

      return BadRequest("Dados incorretos");
    }

    private async Task<object> GerarToken(string cpf)
    {
      var user = await _userManager.FindByNameAsync(cpf);
      var userClaims = await _userManager.GetClaimsAsync(user);
      var userRoles = await _userManager.GetRolesAsync(user);

      userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

      // Necessário converter para IdentityClaims
      var identityClaims = new ClaimsIdentity();
      identityClaims.AddClaims(userClaims);
      identityClaims.AddClaims(userRoles.Select(p => new Claim("role", p)));

      var handler = new JwtSecurityTokenHandler();
      var signingConf = new SigningCredentialsConfiguration();
      var securityToken = handler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = _tokenDescriptor.Issuer,
        Audience = _tokenDescriptor.Audience,
        SigningCredentials = signingConf.SigningCredentials,
        Subject = identityClaims,
        NotBefore = DateTime.Now,
        Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
      });

      var encodedJwt = handler.WriteToken(securityToken);

      return new
      {
        result = new
        {
          access_token = encodedJwt,
          expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
          user = new
          {
            id = user.Id,
            cpf = user.UserName,
            claims = userClaims.Select(c => new { c.Type, c.Value }),
            roles = userRoles
          }
        }
      };
    }

  }
}