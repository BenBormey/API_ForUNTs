using API_ForUNT.DTO;
using API_ForUNT.Models;
using API_ForUNT.Services;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_ForUNT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
       private readonly ServiceRepository _Servicerepository;
        public ServiceController(ServiceRepository servicerepository)
        {
            _Servicerepository = servicerepository;
        }
        [HttpGet]
        public async Task<APIResponse<List<Service>>> GetAllCustomers()
        {
            try
            {
                var Services = await _Servicerepository.GetAllService();
                return new APIResponse<List<Service>>(Services, "Retrieved all customers successfully.");
            }
            catch (Exception ex)
            {
                return new APIResponse<List<Service>>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<APIResponse<ServiceResponseDTO>> CreateService( [FromBody]  ServiceDTO serviceId)
        {
            if (!ModelState.IsValid)
            {
                return new APIResponse<ServiceResponseDTO>(HttpStatusCode.BadRequest, "Invalid data", ModelState);
            }
            try
            {
                var serviceIds = await _Servicerepository.InsertService(serviceId);
                var serviceDTO = new ServiceResponseDTO { serviceId = serviceIds };
                return new APIResponse<ServiceResponseDTO>(serviceDTO, "Customer Created Successfully.");
            }
            catch (Exception ex)
            {
                return new APIResponse<ServiceResponseDTO>(HttpStatusCode.InternalServerError, "Internal server error: " + ex.Message);
            }

        }
    }
}
