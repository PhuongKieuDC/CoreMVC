import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/services/employee.service';
import { NgForm } from '@angular/forms';
import { formatDate } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detail-employee',
  templateUrl: './detail-employee.component.html',
  // styleUrls: ['./detail-employee.component.css']
  styles: []
})
export class DetailEmployeeComponent implements OnInit {

  constructor(private service: EmployeeService, private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();
    this.getAllDepartment();
  }

  resetForm(form?: NgForm) {
    if (form != null) {
      form.form.reset();
    }
    this.service.formData = {
      id: 0,
      name: '',
      email: '',
      phoneNumber: '',
      dateOfBirth: null,
      salary: 0,
      departmentId: 1,
    };
  }

  getAllDepartment() {
    this.service.getAllDepartment();
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id === 0) {
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }
  }

  insertRecord(form: NgForm) {
    this.service.post().subscribe(
      res => {
        this.resetForm();
        this.toastr.success('Submitted successfully', 'Add new Employee');
        this.service.getAllEmployee();
      }
    );
  }

  updateRecord(form: NgForm) {
    this.service.edit().subscribe(
      res => {
        this.resetForm();
        this.toastr.success('Submitted successfully', 'Edit employee');
        this.service.getAllEmployee();
      }
    );
  }

  reset(form: NgForm) {
    this.resetForm(form);
  }
}
