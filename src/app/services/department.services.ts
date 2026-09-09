import { Observable } from "rxjs";
import { department } from "../models/department.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn:'root'
})
export class departmentService{
  private apiUrl = "http://localhost:5100/api/departments";
  constructor(public http:HttpClient){}
  getDepartments():Observable<department[]>{
    return this.http.get<department[]>(this.apiUrl);
  }
}
