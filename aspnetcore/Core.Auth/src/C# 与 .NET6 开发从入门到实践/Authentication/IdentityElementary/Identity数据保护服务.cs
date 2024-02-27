﻿using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace OpenIddictServer;

/// <summary>
/// AES数据加密器
/// </summary>
public class AesLookupProtector : ILookupProtector
{
    private readonly object _locker;

    private readonly Dictionary<string, AesProtector> _protectors;

    private readonly DirectoryInfo _dirInfo;

    public AesLookupProtector(ProtectorOptions options)
    {
        _locker = new object();

        _protectors = new Dictionary<string, AesProtector>();

        _dirInfo = new DirectoryInfo(options.KeyPath);
    }

    public string Protect(string keyId, string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return data;
        }

        CheckOrCreateProtector(keyId);

        return _protectors[keyId].Protect(Encoding.UTF8.GetBytes(data)).ToBase64String();
    }

    public string Unprotect(string keyId, string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return data;
        }

        CheckOrCreateProtector(keyId);

        return Encoding.UTF8.GetString(_protectors[keyId].Unprotect(data.ToBytesFromBase64String()));
    }

    private void CheckOrCreateProtector(string keyId)
    {
        if (!_protectors.ContainsKey(keyId))
        {
            lock (_locker)
            {
                if (!_protectors.ContainsKey(keyId))
                {
                    var fileInfo = _dirInfo.GetFiles().FirstOrDefault(d => d.Name == $@"key-{keyId}.xml") ??
                                    throw new FileNotFoundException();
                    using (var stream = fileInfo.OpenRead())
                    {
                        XDocument xmlDoc = XDocument.Load(stream);
                        _protectors.Add(keyId,
                            new AesProtector(xmlDoc.Element("key")?.Element("encryption")?.Element("masterKey")?.Value.ToBytesFromBase64String()
                                , xmlDoc.Element("key")?.Element("encryption")?.Element("iv")?.Value.ToBytesFromBase64String()
                                , int.Parse(xmlDoc.Element("key")?.Element("encryption")?.Attribute("BlockSize")?.Value)
                                , int.Parse(xmlDoc.Element("key")?.Element("encryption")?.Attribute("KeySize")?.Value)
                                , int.Parse(xmlDoc.Element("key")?.Element("encryption")?.Attribute("FeedbackSize")?.Value)
                                , Enum.Parse<PaddingMode>(xmlDoc.Element("key")?.Element("encryption")?.Attribute("Padding")?.Value)
                                , Enum.Parse<CipherMode>(xmlDoc.Element("key")?.Element("encryption")?.Attribute("Mode")?.Value)));
                    }
                }
            }
        }
    }
}

/// <summary>
/// AES加密器密钥管理器
/// </summary>
public class AesProtectorKeyRing : ILookupProtectorKeyRing
{
    private readonly object _locker;
    private readonly Dictionary<string, XDocument> _keyRings;
    private readonly DirectoryInfo _dirInfo;

    public AesProtectorKeyRing(ProtectorOptions options)
    {
        _locker = new object();
        _keyRings = new Dictionary<string, XDocument>();
        _dirInfo = new DirectoryInfo(options.KeyPath);

        ReadKeys(_dirInfo);
    }

    public IEnumerable<string> GetAllKeyIds()
    {
        return _keyRings.Keys;
    }

    public string CurrentKeyId => NewestActivationKey(DateTimeOffset.Now)?.Element("key")?.Attribute("id")?.Value ?? GenerateKey(_dirInfo)?.Element("key")?.Attribute("id")?.Value;

    public string this[string keyId] =>
        GetAllKeyIds().FirstOrDefault(id => id == keyId) ?? throw new KeyNotFoundException();

    private void ReadKeys(DirectoryInfo dirInfo)
    {
        foreach (var fileInfo in dirInfo.GetFiles().Where(f => f.Extension == ".xml"))
        {
            using (var stream = fileInfo.OpenRead())
            {
                XDocument xmlDoc = XDocument.Load(stream);

                _keyRings.TryAdd(xmlDoc.Element("key")?.Attribute("id")?.Value, xmlDoc);
            }
        }
    }

    private XDocument GenerateKey(DirectoryInfo dirInfo)
    {
        var now = DateTimeOffset.Now;
        if (!_keyRings.Any(item =>
            DateTimeOffset.Parse(item.Value.Element("key")?.Element("activationDate")?.Value) <= now
            && DateTimeOffset.Parse(item.Value.Element("key")?.Element("expirationDate")?.Value) > now))
        {
            lock (_locker)
            {
                if (!_keyRings.Any(item =>
                    DateTimeOffset.Parse(item.Value.Element("key")?.Element("activationDate")?.Value) <= now
                    && DateTimeOffset.Parse(item.Value.Element("key")?.Element("expirationDate")?.Value) > now))
                {
                    var masterKeyId = Guid.NewGuid().ToString();

                    XDocument xmlDoc = new XDocument();
                    xmlDoc.Declaration = new XDeclaration("1.0", "utf-8", "yes");

                    XElement key = new XElement("key");
                    key.SetAttributeValue("id", masterKeyId);
                    key.SetAttributeValue("version", 1);

                    XElement creationDate = new XElement("creationDate");
                    creationDate.SetValue(now);

                    XElement activationDate = new XElement("activationDate");
                    activationDate.SetValue(now);

                    XElement expirationDate = new XElement("expirationDate");
                    expirationDate.SetValue(now.AddDays(90));

                    XElement encryption = new XElement("encryption");
                    encryption.SetAttributeValue("BlockSize", 128);
                    encryption.SetAttributeValue("KeySize", 256);
                    encryption.SetAttributeValue("FeedbackSize", 128);
                    encryption.SetAttributeValue("Padding", PaddingMode.PKCS7);
                    encryption.SetAttributeValue("Mode", CipherMode.CBC);

                    AesProtector protector = new AesProtector();
                    XElement masterKey = new XElement("masterKey");
                    masterKey.SetValue(protector.GenerateKey().ToBase64String());

                    XElement iv = new XElement("iv");
                    iv.SetValue(protector.GenerateIV().ToBase64String());

                    xmlDoc.Add(key);
                    key.Add(creationDate);
                    key.Add(activationDate);
                    key.Add(expirationDate);
                    key.Add(encryption);
                    encryption.Add(masterKey);
                    encryption.Add(iv);

                    xmlDoc.Save(
                        $@"{dirInfo.FullName}\key-{masterKeyId}.xml");

                    _keyRings.Add(masterKeyId, xmlDoc);

                    return xmlDoc;
                }

                return NewestActivationKey(now);
            }
        }

        return NewestActivationKey(now);
    }

    private XDocument NewestActivationKey(DateTimeOffset now)
    {
        return _keyRings.Where(item =>
                DateTimeOffset.Parse(item.Value.Element("key")?.Element("activationDate")?.Value) <= now
                && DateTimeOffset.Parse(item.Value.Element("key")?.Element("expirationDate")?.Value) > now)
            .OrderByDescending(item =>
                DateTimeOffset.Parse(item.Value.Element("key")?.Element("expirationDate")?.Value)).FirstOrDefault().Value;
    }
}

public class ProtectorOptions
{
    public string KeyPath { get; set; }
}
