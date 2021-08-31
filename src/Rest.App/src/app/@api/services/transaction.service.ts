import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Transaction } from '@api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { baseUrl, EntityPage, IPagableService } from '@core';

@Injectable({
  providedIn: 'root'
})
export class TransactionService implements IPagableService<Transaction> {

  uniqueIdentifierName: string = "transactionId";

  constructor(
    @Inject(baseUrl) private readonly _baseUrl: string,
    private readonly _client: HttpClient
  ) { }

  getPage(options: { pageIndex: number; pageSize: number; }): Observable<EntityPage<Transaction>> {
    return this._client.get<EntityPage<Transaction>>(`${this._baseUrl}api/transaction/page/${options.pageSize}/${options.pageIndex}`)
  }

  public get(): Observable<Transaction[]> {
    return this._client.get<{ transactions: Transaction[] }>(`${this._baseUrl}api/transaction`)
      .pipe(
        map(x => x.transactions)
      );
  }

  public getById(options: { transactionId: string }): Observable<Transaction> {
    return this._client.get<{ transaction: Transaction }>(`${this._baseUrl}api/transaction/${options.transactionId}`)
      .pipe(
        map(x => x.transaction)
      );
  }

  public remove(options: { transaction: Transaction }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/transaction/${options.transaction.transactionId}`);
  }

  public create(options: { transaction: Transaction }): Observable<{ transaction: Transaction }> {
    return this._client.post<{ transaction: Transaction }>(`${this._baseUrl}api/transaction`, { transaction: options.transaction });
  }
  
  public update(options: { transaction: Transaction }): Observable<{ transaction: Transaction }> {
    return this._client.put<{ transaction: Transaction }>(`${this._baseUrl}api/transaction`, { transaction: options.transaction });
  }
}
