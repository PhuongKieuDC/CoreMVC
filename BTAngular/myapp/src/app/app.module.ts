import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { EmployeesComponent } from './employees/employees.component';
import { DetailEmployeeComponent } from './employees/detail-employee/detail-employee.component';
import { ListEmployeeComponent } from './employees/list-employee/list-employee.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeesComponent,
    DetailEmployeeComponent,
    ListEmployeeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ModalModule.forRoot(),
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
