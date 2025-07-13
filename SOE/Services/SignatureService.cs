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

    public static bool Verify(byte[] publicKey, int optionId, byte[] signature){
        using RSA rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(publicKey, out var _);
        byte[] dataBytes = Encoding.UTF8.GetBytes(optionId.ToString());
        return rsa.VerifyData(dataBytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

}
