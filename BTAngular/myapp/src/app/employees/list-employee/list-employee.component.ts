import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeService, Employee } from 'src/app/services/employee.service';
import { ToastrService } from 'ngx-toastr';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
  selector: 'app-list-employee',
  templateUrl: './list-employee.component.html',
  // styleUrls: ['./list-employee.component.css']
  styles: []
})
export class ListEmployeeComponent implements OnInit {
  @ViewChild('modalcomfirm') modalcomfirm: ModalDirective;
  idemp: number = 0;
  constructor(private service: EmployeeService, private toastr: ToastrService) { }

  ngOnInit() {
    this.service.getAllEmployee();
  }

  populateForm(row: Employee) {
    this.service.formData = Object.assign({}, row);
  }

  delConfirm(event, id) {
    event.preventDefault();
    this.idemp = id;
    this.modalcomfirm.show();
  }

  delete() {
    this.service.delete(this.idemp).subscribe(res =>{
      this.service.getAllEmployee();
    });
    this.modalcomfirm.hide();
  }
}
