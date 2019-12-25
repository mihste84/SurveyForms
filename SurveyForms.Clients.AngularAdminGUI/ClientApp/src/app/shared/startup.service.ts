import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StartupService {
  public referenceData: any;
  public appInfo: any;

  constructor(private http: HttpClient) { }

  public async getReferenceData() {
    const data = await this.http.get('/api/referenceData').toPromise();
    return this.referenceData = data;
  }

  public async getAppInfo() {
    const data = await this.http.get('/api/appInfo').toPromise();
    return this.appInfo = data;
  }
}
