using System;
using Newtonsoft.Json;
using SharpYaml.Serialization;
using WorkflowCore.Models.DefinitionStorage.v1;

namespace WorkflowCore.Services.DefinitionStorage
{
    public static class Deserializers
    {
        private static Serializer yamlSerializer = new Serializer();
        private static XmlSerializer xmlSerializer = new XmlSerializer();

        public static Func<string, DefinitionSourceV1> Json = (source) => JsonConvert.DeserializeObject<DefinitionSourceV1>(source);

        public static Func<string, DefinitionSourceV1> Yaml = (source) => yamlSerializer.DeserializeInto(source, new DefinitionSourceV1());

        public static Func<string, DefinitionSourceV1> Xml = (source) => xmlSerializer.DeserializeInto(source, new DefinitionSourceV1());
    }
}
