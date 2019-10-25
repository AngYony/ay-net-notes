using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Safety_Sample
{
    /*
     * 使用EC Diffie-Hellman算法交换一个对称密钥，以进行安全的传输
     */

    internal class SecureTransferDemo
    {
        private CngKey _aliceKey;
        private CngKey _bobKey;

        private byte[] _alicePubKeyBlob;
        private byte[] _bobPubKeyBlob;

        public static void Run()
        {
            SecureTransferDemo std = new SecureTransferDemo();
            std.RunAsync().Wait();
        }

        public async Task RunAsync()
        {
            try
            {
                CreateKeys();
                byte[] encrytpedData = await AliceSendsDataAsync("This is a secret message for Bob");
                await BobReceivesDataAsync(encrytpedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 使用EC Diffie-Hellman512 算法创建密钥对
        /// </summary>
        private void CreateKeys()
        {
            _aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP521);
            _bobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP521);
            _alicePubKeyBlob = _aliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
            _bobPubKeyBlob = _bobKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task<byte[]> AliceSendsDataAsync(string message)
        {
            Console.WriteLine("Alice sends message:" + message);
            byte[] rawData = Encoding.UTF8.GetBytes(message);
            byte[] encryptedData = null;
            //创建ECDiffieHellmanCng对象，并使用Alice的密钥对初始化它
            using (ECDiffieHellmanCng aliceAlgorithm = new ECDiffieHellmanCng(_aliceKey))
            using (CngKey bobPubKey = CngKey.Import(_bobPubKeyBlob, CngKeyBlobFormat.EccPublicBlob))
            {
                //使用Alice的密钥对和Bob的公钥创建一个对称密钥，返回的对称密钥使用对称算法AES加密数据
                byte[] symmKey = aliceAlgorithm.DeriveKeyMaterial(bobPubKey);
                Console.WriteLine("Alice creates thsi symmetric key with Bobs public key Information:" + Convert.ToBase64String(symmKey));
                //
                using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                {
                    aes.Key = symmKey;
                    aes.GenerateIV();
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            await ms.WriteAsync(aes.IV, 0, aes.IV.Length);
                            cs.Write(rawData, 0, rawData.Length);
                        }
                        encryptedData = ms.ToArray();
                    }
                    //在访问内存流中的加密数据之前，必须关闭加密流，否则加密数据就会丢失最后的位
                    aes.Clear();
                }
            }

            Console.WriteLine("Alice:message is encrypted:" + Convert.ToBase64String(encryptedData));
            Console.WriteLine();
            return encryptedData;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrytpedData"></param>
        /// <returns></returns>

        private async Task BobReceivesDataAsync(byte[] encrytpedData)
        {
            Console.WriteLine("Bob receives encrypted data");
            byte[] rawData = null;
            //读取未加密的初始化矢量
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            //BlockSize属性返回块的位数，除以8就可以计算出字节数。
            int nBytes = aes.BlockSize / 8;
            byte[] iv = new byte[nBytes];
            for (int i = 0; i < iv.Length; i++)
            {
                iv[i] = encrytpedData[i];
            }
            //实例化一个ECDiffieHellmanCng对象，使用Alice的公钥，从DeriveKeyMaterial()方法中返回对称密钥
            using (ECDiffieHellmanCng bobAlgorithm = new ECDiffieHellmanCng(_bobKey))
            using (CngKey alicePubKey = CngKey.Import(_alicePubKeyBlob, CngKeyBlobFormat.EccPublicBlob))
            {
                byte[] symmKey = bobAlgorithm.DeriveKeyMaterial(alicePubKey);
                Console.WriteLine("Bob creates this symmetric key with Alices public key information:" + Convert.ToBase64String(symmKey));

                aes.Key = symmKey;
                aes.IV = iv;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        await cs.WriteAsync(encrytpedData, nBytes, encrytpedData.Length - nBytes);
                    }
                    rawData = ms.ToArray();
                    Console.WriteLine("Bob decrypts mesage to:" + Encoding.UTF8.GetString(rawData));
                }
                aes.Clear();
            }
        }
    }
}