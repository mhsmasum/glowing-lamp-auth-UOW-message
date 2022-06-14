using AutoMapper;
using DotNetSecurity.DTO;
using DotNetSecurity.Extensions;
using DotNetSecurity.Models;
using DotNetSecurity.Publisher;
using DotNetSecurity.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork , ILogger<OrderController> logger , IMapper mapper , UserManager<ApplicationUser> userManager )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post( OrderCreateDto model)
        {


            try
            {
                var order = _mapper.Map<Order>(model);
                var userid = HttpContext.GetUserId();
                order.UserId = userid;

                await _unitOfWork.Orders.CreateAsync(order);
                await _unitOfWork.CompleteAsync();
                var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                DirectExchangePublisher.Publish(channel,order);
                var data = _mapper.Map<OrderCreateDto>(order);
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok("Something Wrong");

        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
