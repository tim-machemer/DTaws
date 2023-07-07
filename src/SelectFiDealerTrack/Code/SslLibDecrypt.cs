using System;
using System.Security.Cryptography;
using System.Text;

namespace DealerTrackJsonTranslator.Code;

public class SslLibDecrypt
{
    private string _target;
    private string _privkey;
    
    public SslLibDecrypt(string target, string key)
    {
        _target = target;
        _privkey = key;

    }

    public string ReturnValue()
    {
        RSA privKey = RSA.Create();
        privKey.ImportFromPem(_privkey);
        
        byte[] data = Convert.FromBase64String(_target);
        byte[] decryptedData = privKey.Decrypt(data, RSAEncryptionPadding.Pkcs1);

        return Encoding.UTF8.GetString(decryptedData);
        
    }
}

public class SslLibDecryptBytes
{
    private byte[] _target;
    private string _privkey;
    
    public SslLibDecryptBytes(byte[] target, string key)
    {
        _target = target;
        _privkey = key;

    }

    public string ReturnValue()
    {
        RSA privKey = RSA.Create();
        privKey.ImportFromPem(_privkey);
        
        byte[] decryptedData = privKey.Decrypt(_target, RSAEncryptionPadding.Pkcs1);

        return Encoding.UTF8.GetString(decryptedData);
        
    }
}