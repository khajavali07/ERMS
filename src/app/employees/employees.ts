import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSlideToggleChange, MatSlideToggleModule} from '@angular/material/slide-toggle';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Employee } from '../models/employee.model';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,MatSlideToggleModule],
  templateUrl: './employees.html',
  styleUrls: ['./employees.css'],
})
export class Employees {
  mode = signal<'add'|'edit'>('add');
  employees = signal<Employee[]>([
  {
    id:1,
    name:"Rahul",
    role:"Developer",
    email:"rahul@gmail.com"
  },
  {
    id:2,
    name:"Aman",
    role:"Tester",
    email:"aman@gmail.com"
  }])
  colorMode = signal<boolean>(false);

  toggleChange(event: MatSlideToggleChange) {
    this.colorMode.set(event.checked);
  }

  employeeForm: FormGroup;
  constructor(private fb: FormBuilder) {
    this.employeeForm = this.fb.group({
      id: [0, Validators.required],
      name: ['', Validators.required],
      role: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }
  showPopUp = signal(false);
  addEmployee(){
    this.mode.set('add');
    this.employeeForm.reset();
    this.showPopUp.set(true);
  }
  saveEmployee(){
    if(this.employeeForm.invalid){
      alert("Form is Incomplete");
      this.employeeForm.markAllAsTouched();
      return;
    }
    const employee = this.employeeForm.value;
    this.employees.update((emp) => [...emp,employee]);
    this.employeeForm.reset();
    this.showPopUp.set(false);
  }
  editingEmployee = signal<Employee|null>(null);
  EditEmployee(emp:Employee){
    this.mode.set('edit');
    this.employeeForm.patchValue(emp);
    this.editingEmployee.set(emp);
    this.showPopUp.set(true);
  }
  deleteEmployee(emp: Employee){
    this.employees.update((values) => values.filter((item) => item.id !== emp.id));
  }
}
