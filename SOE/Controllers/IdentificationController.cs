using Microsoft.AspNetCore.Mvc;
using SOE.Api;
using SOE.Services;

namespace SOE.Controllers;

[ApiController]
[Route("api/identification")]
public class IdentificationController(
    AppDbContext appDbContext,
    IIdentificationService identificationService
) : ControllerBase {
    
    private readonly AppDbContext _appDbContext = appDbContext;

    [HttpPost]
    public async Task<IdentificationResponse> IdentifyAsync(IdentificationRequest request) { 
        return await identificationService.IdentifyAsync(request.Email);
    }
    
}