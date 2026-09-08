import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Employee } from "../models/employee.model";

@Injectable({
  providedIn:'root'
})
export class EmployeeService{
  private apiUrl = "http://localhost:5100/api/employees";
  constructor(public http:HttpClient){}

  getEmployees(): Observable<Employee[]>{
    return this.http.get<Employee[]>(this.apiUrl);
  }

  getEmployee(id:number):Observable<Employee>{
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }

  createEmployee(employee:Employee) : Observable<Employee>{
    return this.http.post<Employee>(this.apiUrl,employee)
  }

  updateEmployee(id:number,employee:Employee|any) : Observable<void>{
    return this.http.put<void>(`${this.apiUrl}/${id}`,employee);
  }
  deleteEmployee(id:number) : Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

}
