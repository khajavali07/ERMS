import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm =new FormGroup({
    empId: new FormControl(''),
    password: new FormControl('')
  })
  loginFormSubmmision(){
    console.log(this.loginForm.value.empId,this.loginForm.value.password)
  }
  loginFormReset(){
    this.loginForm.setValue({
      empId: '',
      password: ''
  })
  }
}
