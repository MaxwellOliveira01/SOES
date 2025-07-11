using Microsoft.EntityFrameworkCore;
using SOE.Api;
using SOE.Entities;

namespace SOE.Services;

public interface IAuthenticationService {
     Task<AuthenticationResponse> AuthenticateAsync(Voter voter, string givenOtp);
}

public class AuthenticationService(AppDbContext appDbContext, ITokenService tokenService) : IAuthenticationService {

     public async Task<AuthenticationResponse> AuthenticateAsync(Voter voter, string givenOtp) {

          if (!await tokenService.ValidateTokenAsync(voter.Id, givenOtp)) {
               return new() {
                    Success = false,
                    ErrorMessage = "Invalid or expired token."
               };
          }
          
          return new() {
               Success = true,
               Elections = await GetElectionsAsync(voter),
          };
          
     }
     
     private async Task<List<ElectionVoterModel>> GetElectionsAsync(Voter voter) {

          var elections = await appDbContext.Elections
               .Include(e => e.Voters)
               .ToListAsync();
          
          return elections.Select(e => new ElectionVoterModel {
               Id = e.Id,
               Name = e.Name,
               HasVoted = e.Voters.Any(v => v.VoterId == voter.Id)
          }).ToList();

     }
     
}
