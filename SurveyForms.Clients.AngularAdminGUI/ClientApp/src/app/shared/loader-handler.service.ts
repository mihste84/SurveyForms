import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderHandlerService {
  private loaderSubject = new Subject<boolean>();
  public loaderState = this.loaderSubject.asObservable();

  public start() {
    this.loaderSubject.next(true);
  }

  public stop() {
    this.loaderSubject.next(false);
  }
}
