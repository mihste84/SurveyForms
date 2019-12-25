import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  private errorSubject = new Subject<HttpErrorResponse>();
  public errorObject = this.errorSubject.asObservable();

  public setError(error: HttpErrorResponse) {
    this.errorSubject.next(error);
  }
}
