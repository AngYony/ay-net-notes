using System.Security.Cryptography;

namespace OpenIddictServer;

public class AesProtector
{
    private readonly SymmetricAlgorithm _aes;

    public AesProtector()
    {
        _aes = Aes.Create();
    }

    public AesProtector(byte[] key, byte[] iv, int blockSize, int keySize, int feedbackSize,
        PaddingMode paddingMode, CipherMode mode)
    {
        _aes = Aes.Create();
        _aes.BlockSize = blockSize;
        _aes.KeySize = keySize;
        _aes.FeedbackSize = feedbackSize;
        _aes.Padding = paddingMode;
        _aes.Mode = mode;

        //Key和IV要在上面的参数设置完成完成后再设置，否则可能会被内部重新生成的值覆盖
        _aes.Key = key;
        _aes.IV = iv;
    }

    public byte[] Protect(byte[] normal)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            CryptoStream cs = new CryptoStream(ms, _aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(normal, 0, normal.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
    }

    public byte[] Unprotect(byte[] secret)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            CryptoStream cs = new CryptoStream(ms, _aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(secret, 0, secret.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
    }

    public byte[] GenerateKey()
    {
        //备份信息
        var key = _aes.Key;
        var iv = _aes.IV;
        //生成
        _aes.GenerateKey();
        //获取结果
        var res = _aes.Key;
        //还原信息
        _aes.Key = key;
        _aes.IV = iv;

        return res;
    }

    public byte[] GenerateIV()
    {
        //备份信息
        var key = _aes.Key;
        var iv = _aes.IV;
        //生成
        _aes.GenerateIV();
        //获取结果
        var res = _aes.IV;
        //还原信息
        _aes.Key = key;
        _aes.IV = iv;

        return res;
    }
}
