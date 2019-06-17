import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  baseUrl = 'https://localhost:44323/api/';
  apiUrl = {
    employee: this.baseUrl + 'employee',
    department: this.baseUrl + 'department'
  };
  constructor() { }
}
