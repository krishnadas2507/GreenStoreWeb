using GreenStoreWeb.Models;
using GreenStoreWeb.Services.Contract;
using GreenStoreWeb.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenStoreWeb.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly UserDetailsContext _companyContext;
        private readonly IConfiguration _configuration;

        public UserServices(UserDetailsContext companyContext, IConfiguration configuration)
        {
            _companyContext = companyContext;
            _configuration = configuration;
        }
        public RequiredData AddData(RequiredData RequiredData)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(RequiredData.Password);

                var newCustomer = new Authentication
                {
                    Password = hashedPassword,
                    Email = RequiredData.Email,


                };
                _companyContext.Authentications.Add(newCustomer);
                _companyContext.SaveChanges();
                RequiredData.Password = hashedPassword;
                return RequiredData;

            }
            catch (Exception EX)
            {

                throw EX;
            }
        }

        public string Verify(RequiredData requiredData)
        {
            var storedCustomer = _companyContext.Authentications.FirstOrDefault(c => c.Email == requiredData.Email);

            if (storedCustomer == null)
            {
                return "Not Found"; // Email not found in the database
            }

            if (!BCrypt.Net.BCrypt.Verify(requiredData.Password, storedCustomer.Password))
            {
                return "Wrong Password"; // Wrong password
            }

            // If both email and password are correct, create and return the token
            var token = CreateToken(storedCustomer);
            return token;
        }

        public string CreateToken(Authentication login)
        {
            List<Claim> claims = new List<Claim>
         {
              new Claim(ClaimTypes.Name,login.Email)
         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public List<string> GetVegetables()
        {

            var vegetables = _companyContext.Vegelists.Select(v => v.Vegname).ToList();
            return vegetables;
        }
        public List<decimal?> GetVegetablePrices(int userId)
        {
            var prices = _companyContext.Vegetableprices
                .Where(vp => vp.Userid == userId)
                .Select(vp => vp.Price)
                .ToList();

            return prices;
        }
        public void UpdateOrCreatePrice(int userId, string vegetable, decimal price)
        {
            var existingPrice = _companyContext.Vegetableprices
                .FirstOrDefault(vp => vp.Userid == userId && vp.Veg.Vegname == vegetable);

            if (existingPrice != null)
            {
                // Update existing price
                existingPrice.Price = price;
            }
            else
            {
                // Create a new price entry
                var newPrice = new Vegetableprice
                {
                    Userid = userId,
                    Vegid = _companyContext.Vegelists.FirstOrDefault(v => v.Vegname == vegetable)?.Vegid,
                    Price = price
                };

                _companyContext.Vegetableprices.Add(newPrice);
            }

            _companyContext.SaveChanges();
        }
    }
}

