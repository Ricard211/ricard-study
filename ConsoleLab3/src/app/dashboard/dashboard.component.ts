import { Component, OnInit } from '@angular/core';
import { Console } from '../console';
import { ConsoleService } from '../console.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  consoles: Console[] = [];

  constructor(private consoleService: ConsoleService) {}

  ngOnInit(): void {
    this.getConsoles();
  }

  getConsoles(): void {
    this.consoleService
      .getConsoles()
      .subscribe((consoles) => (this.consoles = consoles.slice(1, 5)));
  }
}
