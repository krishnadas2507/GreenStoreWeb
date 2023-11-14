using GreenStoreWeb.Services.Contract;
using GreenStoreWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Security.Claims;

namespace GreenStoreWeb.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]

        public IActionResult postCustomerDetails(RequiredData requiredData)
        {
            ResponseApi<RequiredData> response = new ResponseApi<RequiredData>();

            try
            {
                RequiredData newdetail = _userServices.AddData(requiredData);
                response = new ResponseApi<RequiredData>
                {
                    Status = true,
                    Msg = "Added",
                    Value = newdetail
                };
                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                response = new ResponseApi<RequiredData>(); response.Msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);

            }
        }

        [HttpPost("verify")]
        public ActionResult<RequiredData> CheckUser([FromBody] RequiredData Request)
        {
            try
            {
                var responses = this._userServices.Verify(Request);

                if (string.IsNullOrWhiteSpace(Request.Email) || string.IsNullOrWhiteSpace(Request.Password))
                {
                    return BadRequest(responses);
                }
                if (responses == "Not Found" || responses == "Wrong Password")
                {
                    return BadRequest(responses);
                }
                return Ok(responses);
            }
            catch (Exception ex)
            {
                var response = new ResponseApi<RequiredData>();
                response.Msg = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("vegetables")]
        public IActionResult GetVegetables()
        {
            var vegetables = _userServices.GetVegetables();
            return Ok(vegetables);
        }
        private int GetUserIdFromToken()
        {
            // Get the token from the Authorization header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
          
            Console.WriteLine($"Received token: {token}");

            // Decode the token to extract user information
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            // Extract user ID from the token claims
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            // Check if user ID claim exists and parse it to an integer
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }

            // If unable to get the user ID, you may throw an exception or return a default value
            throw new ApplicationException("Unable to retrieve user ID from token.");
        }

        [HttpGet("getPrices")]
        public IActionResult GetPrices()
        {
            try
            {
                int userId = GetUserIdFromToken(); // Implement this method

                var prices = _userServices.GetVegetablePrices(userId);

                return Ok(prices);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error fetching prices: {ex.Message}" });
            }
        }
        [HttpPost("updatePrice")]
        public IActionResult UpdatePrice([FromBody] UpdatePriceRequest updateData)
        {
            try
            {
                int userId = GetUserIdFromToken(); // Implement this method

                _userServices.UpdateOrCreatePrice(userId, updateData.Vegetable, updateData.Price);

                return Ok(new { Message = "Price updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Error updating price: {ex.Message}" });
            }
        }

    }
}
