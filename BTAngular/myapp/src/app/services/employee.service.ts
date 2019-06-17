import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
export interface Employee {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: Date;
  salary: number;
  departmentId: number;
}
export interface Department {
  id: number;
  name: string;
  departmentLead: number;
}
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  formData: Employee;
  formDataDe: Department;
  listEmployee: Employee[];
  listDepartment: Department[];
  constructor(private http: HttpClient, private api: ApiService) { }

  public getAllEmployee() {
    this.http.get(this.api.apiUrl.employee)
    .toPromise()
    .then(res =>  this.listEmployee = res as Employee[]);
  }
  public getAllDepartment() {
    this.http.get(this.api.apiUrl.department)
    .toPromise()
    .then(res1 => this.listDepartment = res1 as Department[]);
  }
  public post() {
    return this.http.post(this.api.apiUrl.employee, this.formData);
  }
  public edit() {
    return this.http.put(this.api.apiUrl.employee + '/' + this.formData.id, this.formData);
  }
  public delete(id) {
    return this.http.delete(this.api.apiUrl.employee + '/' + id);
  }
}
