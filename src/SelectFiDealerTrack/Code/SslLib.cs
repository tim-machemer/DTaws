using System;
using System.Security.Cryptography;
using System.Text;

namespace DealerTrackJsonTranslator.Code;

public class SslLib
{
    private string _target;
    private string _pubkey;
    
    public SslLib(string target, string key)
    {
        _target = target;
        _pubkey = key;

    }

    public string ReturnValue()
    {
        RSA publicKey = RSA.Create();
        publicKey.ImportFromPem(_pubkey);
        
        byte[] data = Encoding.UTF8.GetBytes(_target);
        byte[] encryptedData = publicKey.Encrypt(data, RSAEncryptionPadding.Pkcs1);

        return Encoding.UTF8.GetString(encryptedData);
        
    }
    public string ReturnValueBase64()
    {
        RSA publicKey = RSA.Create();
        publicKey.ImportFromPem(_pubkey);
        
        byte[] data = Encoding.UTF8.GetBytes(_target);
        byte[] encryptedData = publicKey.Encrypt(data, RSAEncryptionPadding.Pkcs1);

        return Convert.ToBase64String(encryptedData);
        
    }

    public byte[] ReturnValueBytes()
    {
        RSA publicKey = RSA.Create();
        publicKey.ImportFromPem(_pubkey);
        
        byte[] data = Encoding.UTF8.GetBytes(_target);
        byte[] encryptedData = publicKey.Encrypt(data, RSAEncryptionPadding.Pkcs1);

        return encryptedData;
        
    }

}