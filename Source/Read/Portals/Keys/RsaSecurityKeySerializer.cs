/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Security.Cryptography;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Read.Portals.Keys
{
    public class RsaSecurityKeySerializer : StructSerializerBase<RSAParameters>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, RSAParameters value)
        {
            var writer = context.Writer;
            writer.WriteStartDocument();
            writer.WriteName("D");
            writer.WriteBytes(value.D);
            writer.WriteName("DP");
            writer.WriteBytes(value.DP);
            writer.WriteName("DQ");
            writer.WriteBytes(value.DQ);
            writer.WriteName("Exponent");
            writer.WriteBytes(value.Exponent);
            writer.WriteName("InverseQ");
            writer.WriteBytes(value.InverseQ);
            writer.WriteName("Modulus");
            writer.WriteBytes(value.Modulus);
            writer.WriteName("P");
            writer.WriteBytes(value.P);
            writer.WriteName("Q");
            writer.WriteBytes(value.Q);
            writer.WriteEndDocument();
        }
        
        public override RSAParameters Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var parameters = new RSAParameters();
            var reader = context.Reader;
            reader.ReadStartDocument();
            while (reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var name = reader.ReadName();
                var bytes = reader.ReadBinaryData().AsByteArray;
                switch (name)
                {
                    case "D":
                    parameters.D = bytes;
                    break;
                    case "DP":
                    parameters.DP = bytes;
                    break;
                    case "DQ":
                    parameters.DQ = bytes;
                    break;
                    case "Exponent":
                    parameters.Exponent = bytes;
                    break;
                    case "InverseQ":
                    parameters.InverseQ = bytes;
                    break;
                    case "Modulus":
                    parameters.Modulus = bytes;
                    break;
                    case "P":
                    parameters.P = bytes;
                    break;
                    case "Q":
                    parameters.Q = bytes;
                    break;
                }
            }
            reader.ReadEndDocument();
            return parameters;
        }
    }
}