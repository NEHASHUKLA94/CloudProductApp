import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; // Import this
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CloudProductService {
  private apiUrl = 'https://localhost:7100/api/Product';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<any> {
    return this.http.get(`${this.apiUrl}/products`);
  }
}
