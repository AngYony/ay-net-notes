using System;
using System.Security.Cryptography;
using System.Text;

namespace Safety_Sample
{
    /*
     * 使用ECDSA算法进行签名。Alice创建一个签名，它用Alice的私钥加密，可以使用Alice的公钥访问
     */
    internal class SigningDemo
    {
        private CngKey _aliceKeySignature;
        private byte[] _alicePubKeyBlob;
        public void Run()
        {
            //创建Alice的密钥
            InitAliceKeys();
            byte[] aliceData = Encoding.UTF8.GetBytes("Alice");
            //给字符串签名
            byte[] aliceSignature = CreateSignature(aliceData, _aliceKeySignature);
            //将加密的签名写入控制台
            Console.WriteLine("Alice created signature:" + Convert.ToBase64String(aliceSignature));
            //使用公钥验证该签名是否真的来自于Alice
            if (VerifySignature(aliceData, aliceSignature, _alicePubKeyBlob))
            {
                Console.WriteLine("Alice signature verified successfully");
            }
        }
        /// <summary>
        /// 验证签名是否正确，使用公钥检查签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature">签名后的数据</param>
        /// <param name="pubKey">公钥字节数组</param>
        /// <returns></returns>
        private bool VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
        {
            bool retValue = false;
            //导入CngKey对象
            using(CngKey key = CngKey.Import(pubKey, CngKeyBlobFormat.GenericPublicBlob))
            using (var signingAlg=new ECDsaCng(key))
            {
#if NET46
                retValue = signingAlg.VerifyData(data, signature);
                signingAlg.Clear();
#else
                //验证签名
                retValue = signingAlg.VerifyData(data, signature, HashAlgorithmName.SHA512);
#endif
            }
            return retValue;
        }

        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] CreateSignature(byte[] data, CngKey key)
        {
            byte[] signature;
            //使用ECDsaCng创建签名,ECDsaCng的构造函数接收包含公钥和私钥的CngKey类对象
            using (ECDsaCng signingAlg = new ECDsaCng(key))
            {
#if NET46
                signature = signingAlg.SignData(data);
                signingAlg.Clear();
#else
                //对数据进行签名（加密）
                signature = signingAlg.SignData(data, HashAlgorithmName.SHA512);
#endif
            }
            return signature;
        }

        /// <summary>
        /// 创建新的密钥对
        /// </summary>
        private void InitAliceKeys()
        {
            //创建密钥对，CngKey包含公钥和私钥数据
            _aliceKeySignature = CngKey.Create(CngAlgorithm.ECDsaP521);
            
            //导出密钥对中的公钥，后面将要使用它来验证签名
            _alicePubKeyBlob = _aliceKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
        }
    }
}
