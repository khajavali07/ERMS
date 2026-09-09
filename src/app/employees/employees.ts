import { Component, OnInit, signal} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSlideToggleChange, MatSlideToggleModule} from '@angular/material/slide-toggle';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Employee } from '../models/employee.model';
import { EmployeeService } from '../services/employee.services';
import { departmentService } from '../services/department.services';
import { department } from '../models/department.model';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,MatSlideToggleModule],
  templateUrl: './employees.html',
  styleUrls: ['./employees.css'],
})
export class Employees implements OnInit{
  mode = signal<'add'|'edit'>('add');

  employees = signal<Employee[]>([]);

  departments = signal<department[]>([]);

  colorMode = signal<boolean>(false);

  showPopup = signal<boolean>(false);

  editingEmployee= signal<Employee | null>(null);

  employeeForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private departmentService: departmentService
  ){
    this.employeeForm = this.fb.group({
      //personal Information
      employeeCode: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth:[''],
      gender:[''],
      maritalStatus:[''],
      bloodGroup:[''],
      profilePhoto:[''],

      //contact information
      email: ['', [Validators.required, Validators.email]],
      phone:[''],
      emergencyContact:[''],
      address:[''],
      city:[''],
      state:[''],
      pincode:[''],

      //proffesional information
      departmentId:[''],
      designation:['',Validators.required],
      managerId:[''],
      joiningDate:[''],
      employmentType:[''],
      salary:[''],
      shift:[''],
      workLocation:['']
    });
  }
  ngOnInit():void{
    this.loadEmployees();
    this.loadDepartments();
  }

  loadEmployees():void{
    this.employeeService.getEmployees().subscribe({
      next:(data)=>{
        this.employees.set(data);
      },
      error:(error)=>{
        console.error("Error while loading Employees ",error);
      }
    })
  }

  loadDepartments():void{
    this.departmentService.getDepartments().subscribe({
      next:(data)=>{
        this.departments.set(data);
      },
      error:(error)=>{
        console.error("error while Loading departments ",error);
      }
    })
  }

  toggleChange(event:MatSlideToggleChange){
    this.colorMode.set(event.checked);
  }

  addEmployee(){
    this.mode.set('add');
    this.editingEmployee.set(null);
    this.employeeForm.reset();
    this.showPopup.set(true);
  }

  saveEmployee(){
    if(this.employeeForm.invalid){
      alert("Form is Incomplete");
      this.employeeForm.markAllAsTouched();
      return;
    }

    const employee = this.employeeForm.value;
    console.log('Employee being sent:', employee);

    if(this.mode()==='add'){
      this.employeeService.createEmployee(employee).subscribe({
        next:()=>{
          this.loadEmployees();
          this.employeeForm.reset();
          this.showPopup.set(false);
        },
        error:(error)=>{
          console.error("Error While creating Employee ",error);
        }
      })
    }
    else{
      const existingEmployee = this.editingEmployee();
      if(!existingEmployee){
        return;
      }
      this.employeeService.updateEmployee(existingEmployee.id,employee).subscribe({
        next:()=>{
          this.loadEmployees();
          this.employeeForm.reset();
          this.showPopup.set(false);
          this.editingEmployee.set(null);
        },
        error:(error)=>{
          console.error("Error while updating Employee");
        }
      });
    }
  }

  editEmployee(emp:Employee):void{
    this.mode.set('edit');
    this.editingEmployee.set(emp);
    this.employeeForm.patchValue({
      employeeCode:emp.employeeCode,
      firstName:emp.firstName,
      lastName:emp.lastName,
      email:emp.email,
      dateOfBirth:emp.dateOfBirth?.split('T')[0],
      gender:emp.gender,
      maritalStatus:emp.maritalStatus,
      bloodGroup:emp.bloodGroup,
      profilePhoto:emp.profilePhoto,
      phone:emp.phone,
      emergencyContact:emp.emergencyContact,
      address:emp.address,
      city:emp.city,
      state:emp.state,
      pincode:emp.pincode,
      departmentId:emp.departmentId,
      designation:emp.designation,
      managerId:emp.managerId,
      joiningDate:emp.joiningDate?.split('T')[0],
      employmentType:emp.employmentType,
      salary:emp.salary,
      workLocation:emp.workLocation,
      shift:emp.shift
    });
    this.showPopup.set(true);
  }
  deleteEmployee(emp: Employee):void{
    if(!confirm(`Are you sure you want to deactivate ${emp.firstName} ${emp.lastName}?`)){
      return;
    }
    this.employeeService.deleteEmployee(emp.id).subscribe({
      next:()=>{
        this.loadEmployees();
      },
      error:(error)=>{
        console.error("error while deleting Employee ",error);
        alert('Unable to Delete Employee');
      }
    });
  }
}
