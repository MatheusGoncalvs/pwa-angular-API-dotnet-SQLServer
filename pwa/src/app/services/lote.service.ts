import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, retry, take } from 'rxjs/operators';
import { Lote } from '../Models/Lote';
import { LoteTrackerError } from '../Models/LoteTrackerError';

@Injectable({
  providedIn: 'root'
})
export class LoteService {

  private _deleteOperationSuccessfulEvent$: Subject<boolean> = new Subject();

  get deleteOperationSuccessfulEvent$(): Observable<boolean> {
    return this._deleteOperationSuccessfulEvent$.asObservable();
  }

  constructor(
    private http: HttpClient
  ) { }

  private lotesUrl = 'http://localhost:5001/api/produto/importacoes';
  private removeUrl = 'http://localhost:5001/api/produto/importacao';


  obterLotes(): Observable<Lote[]> {
    return this.http.get<Lote[]>(this.lotesUrl)
      .pipe(
        retry(1),
        catchError(error => this.handleHttpError(error))
      );
  }

  removerLote(id: number): Observable<{} | LoteTrackerError> {
    return this.http.delete(`${this.removeUrl}/${id}`)
      .pipe(
        retry(1),
        take(1),
        catchError(error => this.handleHttpErrorTracker(error))
      );
  }

  private handleHttpError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      let dataError = new LoteTrackerError();
      dataError.errorNumber = 100;
      dataError.message = error.statusText;
      dataError.friendlyMessage = "An error occurred while trying to get the lotes.";
      return throwError(dataError);
    }
  }

  private handleHttpErrorTracker(error: HttpErrorResponse): Observable<LoteTrackerError> {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      let dataError = new LoteTrackerError();
      dataError.errorNumber = 100;
      dataError.message = error.statusText;
      dataError.friendlyMessage = "An error occurred while trying to remove lotes.";
      return throwError(dataError);
    }
  }

}
