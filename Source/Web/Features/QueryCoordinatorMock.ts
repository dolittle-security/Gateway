/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

import { IQueryCoordinator, IQuery, QueryResponse, Query } from '@dolittle/queries';
import { IReadModel, ReadModel } from '@dolittle/readmodels';

export class QueryCoordinatorMock implements IQueryCoordinator
{
    private _handlers : Map<string, any> = new Map<string, any>();

    registerQueryHandler<Q extends Query, T extends ReadModel>(nameOfQuery: string, handler: (query: Q) => T[])
    {
        this._handlers.set(nameOfQuery, handler);
    }

    async execute<T extends IReadModel>(query: IQuery<T>): Promise<QueryResponse<T>>
    {
        if (this._handlers.has(query.nameOfQuery))
        {
            return this.newQueryResponse(query, this._handlers.get(query.nameOfQuery)());
        }
        else
        {
            return this.newQueryResponse(query, []);
        }
    }

    private newQueryResponse<Q extends Query, T extends IReadModel>(query: Q, items: T[])
    {
        return {
            queryName: query.nameOfQuery,
            totalItems: items.length,
            exception: null,
            securityMessages: [],
            brokenRules: [],
            passedSecurity: true,
            success: true,
            invalid: false,
            items: items,
        } as QueryResponse<T>;
    }
}