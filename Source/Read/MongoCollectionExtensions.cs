/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using Dolittle.ReadModels.MongoDB;
using MongoDB.Driver;

namespace Read
{
    public static class MongoCollectionExtensions
    {
        public static bool TryFindById<T>(this IMongoCollection<T> collection, object id, out T document)
        {
            var documentId = id.GetIdAsBsonValue();
            var results = collection.Find(Builders<T>.Filter.Eq("_id", documentId)).Limit(1);
            document = results.FirstOrDefault();
            return results.Any();
        }

        public static T FindById<T>(this IMongoCollection<T> collection, object id)
        {
            collection.TryFindById(id, out var document);
            return document;
        }
    }
}