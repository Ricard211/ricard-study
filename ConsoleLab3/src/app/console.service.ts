import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Console } from './console';
import { MessageService } from './message.service';

@Injectable({ providedIn: 'root' })
export class ConsoleService {
  private consolesUrl = 'api/consoles'; // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {}

  /** GET consoles from the server */
  getConsoles(): Observable<Console[]> {
    return this.http.get<Console[]>(this.consolesUrl).pipe(
      tap((_) => this.log('fetched console')),
      catchError(this.handleError<Console[]>('getConsoles', []))
    );
  }

  /** GET console by id. Return `undefined` when id not found */
  getConsoleNo404<Data>(id: number): Observable<Console> {
    const url = `${this.consolesUrl}/?id=${id}`;
    return this.http.get<Console[]>(url).pipe(
      map((consoles) => consoles[0]), // returns a {0|1} element array
      tap((h) => {
        const outcome = h ? 'fetched' : 'did not find';
        this.log(`${outcome} console id=${id}`);
      }),
      catchError(this.handleError<Console>(`getConsole id=${id}`))
    );
  }

  /** GET console by id. Will 404 if id not found */
  getConsole(id: number): Observable<Console> {
    const url = `${this.consolesUrl}/${id}`;
    return this.http.get<Console>(url).pipe(
      tap((_) => this.log(`fetched console id=${id}`)),
      catchError(this.handleError<Console>(`getConsole id=${id}`))
    );
  }

  /* GET consoles whose name contains search term */
  searchConsoles(term: string): Observable<Console[]> {
    if (!term.trim()) {
      // if not search term, return empty console array.
      return of([]);
    }
    return this.http.get<Console[]>(`${this.consolesUrl}/?name=${term}`).pipe(
      tap((x) =>
        x.length
          ? this.log(`found consoles matching "${term}"`)
          : this.log(`no consoles matching "${term}"`)
      ),
      catchError(this.handleError<Console[]>('searchConsoles', []))
    );
  }

  //////// Save methods //////////

  /** POST: add a new console to the server */
  addConsole(console: Console): Observable<Console> {
    return this.http
      .post<Console>(this.consolesUrl, console, this.httpOptions)
      .pipe(
        tap((newHero: Console) =>
          this.log(`added console w/ id=${newHero.id}`)
        ),
        catchError(this.handleError<Console>('addConsole'))
      );
  }

  /** DELETE: delete the console from the server */
  deleteConsole(id: number): Observable<Console> {
    const url = `${this.consolesUrl}/${id}`;

    return this.http.delete<Console>(url, this.httpOptions).pipe(
      tap((_) => this.log(`deleted console id=${id}`)),
      catchError(this.handleError<Console>('deleteConsole'))
    );
  }

  /** PUT: update the console on the server */
  updateConsole(console: Console): Observable<any> {
    return this.http.put(this.consolesUrl, console, this.httpOptions).pipe(
      tap((_) => this.log(`updated console id=${console.id}`)),
      catchError(this.handleError<any>('updateConsole'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    this.messageService.add(`ConsoleService: ${message}`);
  }
}
