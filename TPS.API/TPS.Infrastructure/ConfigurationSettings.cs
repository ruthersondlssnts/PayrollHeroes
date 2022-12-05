using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string FileUploadLocation { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public string BaseUrl { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }

        public string initVector { get; set; }
        public string passPhrase { get; set; }
        public int keySize { get; set; }


        public string RabbitHost { get; set; }
        public string RabbitUsername { get; set; }
        public string RabbitPassword { get; set; }
        public string VirtualHost { get; set; }
        public string ParentExchange { get; set; }
        public string Exchange { get; set; }
    }

    public interface IConfigurationSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string Key { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        string Subject { get; set; }
        string FileUploadLocation { get; set; }

        string Email { get; set; }
        string Password { get; set; }
        string Host { get; set; }
        string Port { get; set; }
        string initVector { get; set; }
        string passPhrase { get; set; }
        int keySize { get; set; }

        string RabbitHost { get; set; }

        string RabbitUsername { get; set; }

        string RabbitPassword { get; set; }

        string VirtualHost { get; set; }
        string ParentExchange { get; set; }
        string Exchange { get; set; }

    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
