import { Component } from '@angular/core';
import { Employees } from '../employees/employees';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [Employees],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {}
