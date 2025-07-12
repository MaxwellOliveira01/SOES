namespace SOE.Services;

using System.Security.Cryptography;
using System.Text;

public class SignatureService {

    public static bool Verify(string publicKeyPem, int optionId, string base64Signature) {
        byte[] dataBytes = Encoding.UTF8.GetBytes(optionId.ToString());
        byte[] signatureBytes = Convert.FromBase64String(base64Signature);
        using RSA rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem);
        return rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

}
