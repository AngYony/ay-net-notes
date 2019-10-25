using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Safety_Sample
{
    /*
     * 该示例中，Alice创建一个文档，散列它，以确保它不会改变，给它加上签名，保证是Alice生成了文档。Bob接收文件，并检查Alice的担保，以确保文件没有被篡改
     */

    internal class RSADemo
    {
        private CngKey _aliceKey;
        private byte[] _alicePubKeyBlob;

        public void Run()
        {
            //创建一个文档、散列码、签名
            AliceTasks(out byte[] document, out byte[] hash, out byte[] signature);
            BobTasks(document, hash, signature);
        }

        private void AliceTasks(out byte[] data, out byte[] hash, out byte[] signature)
        {
            //创建Alice所需的密钥
            InitAliceKeys();
            //将消息转换为一个字节数组
            data = Encoding.UTF8.GetBytes("Best greetings from Alice");
            //散列字节数组
            hash = HashDocument(data);
            //添加一个签名
            signature = AddSignatureToHash(hash, _aliceKey);
        }

        /// <summary>
        /// 使用RSA算法创建密钥
        /// </summary>
        private void InitAliceKeys()
        {
            //创建公钥和私钥
            _aliceKey = CngKey.Create(CngAlgorithm.Rsa);
            //公钥只提供给Bob，所以公钥使用Export方法提取
            _alicePubKeyBlob = _aliceKey.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        /// <summary>
        /// 创建散列码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] HashDocument(byte[] data)
        {
            /*
             * 散列码使用一个散列算法SHA384类创建
             * 不管文档存在多久，散列码的长度总是相同
             * 再次为相同的文档创建散列码，会得到相同的散列码
             * Bob需要在文档上使用相同的算法，如果返回相同的散列码，就说明文档没有改变
             */

            using (SHA384 hashAlg = SHA384.Create())
            {
                return hashAlg.ComputeHash(data);
            }
        }

        /// <summary>
        /// 添加签名
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private byte[] AddSignatureToHash(byte[] hash, CngKey key)
        {
            /*
             * 添加签名，可以保证文档来自Alice
             *
             */
            //使用RSACng给散列签名
            using (RSACng signingAlg = new RSACng(key))
            {
                //给散列签名时，SignHash方法需要了解散列算法,此处基于HashAlgorithmName.SHA384算法传入
                byte[] signed = signingAlg.SignHash(hash, HashAlgorithmName.SHA384, RSASignaturePadding.Pss);
                return signed;
            }
        }

        /// <summary>
        /// 接收文档数据、散列码和签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hash"></param>
        /// <param name="signature"></param>
        private void BobTasks(byte[] data, byte[] hash, byte[] signature)
        {
            //导入Alice的公钥
            CngKey aliceKey = CngKey.Import(_alicePubKeyBlob, CngKeyBlobFormat.GenericPublicBlob);
            //验证签名是否有效
            if (!IsSignatureValid(hash, signature, aliceKey))
            {
                Console.WriteLine("signature not valid");
                return;
            }
            //验证文档是否不变
            if (!IsDocumentUnchanged(hash, data))
            {
                Console.WriteLine("doucment was changed");
                return;
            }
            Console.WriteLine("signature valid,doucment unchanged");
            Console.WriteLine("document from Alice:" + Encoding.UTF8.GetString(data));
        }

        /// <summary>
        /// 验证签名是否有效
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="signature"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsSignatureValid(byte[] hash, byte[] signature, CngKey key)
        {
            using (RSACng signingAlg = new RSACng(key))
            {
                return signingAlg.VerifyHash(hash, signature, HashAlgorithmName.SHA384, RSASignaturePadding.Pss);
            }
        }

        /// <summary>
        /// 验证文档数据是否发生了改变
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool IsDocumentUnchanged(byte[] hash, byte[] data)
        {
            byte[] newHash = HashDocument(data);
            //验证散列码是否相同
            return newHash.SequenceEqual(hash);
        }
    }
}