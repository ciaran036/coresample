using System;

namespace Common.Encryption
{
    public class EncryptAttribute : Attribute
    {
        public string Key { get; set; }

        public EncryptAttribute(string key)
        {
            this.Key = key;
        }
    }
}
