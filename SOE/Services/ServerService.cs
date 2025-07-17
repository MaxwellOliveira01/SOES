using SOE.Entities;
using System.Security.Cryptography;
using System.Text;

namespace SOE.Services;

public class ServerService() {
    
    public Server Server { get; private set; }

    private RSA rsa { get; set; }

    public async Task Init(AppDbContext appDbContext) {

        rsa = RSA.Create(2048);

        Server = new Server(){
            Starts = DateTimeOffset.Now,
            PublicKey = rsa.ExportSubjectPublicKeyInfo()
        };

        await appDbContext.Servers.AddAsync(Server);
        await appDbContext.SaveChangesAsync();
    }

    public byte[] Sign(byte[] data) {
        return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
    }

    public bool Verify(byte[] data, byte[] signature) {
        return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
    }
    
}