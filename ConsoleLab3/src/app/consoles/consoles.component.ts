import { Component, OnInit } from '@angular/core';

import { Console } from '../console';
import { ConsoleService } from '../console.service';

@Component({
  selector: 'app-consoles',
  templateUrl: './consoles.component.html',
  styleUrls: ['./consoles.component.css'],
})
export class ConsolesComponent implements OnInit {
  consoles: Console[] = [];

  constructor(private consoleService: ConsoleService) {}

  ngOnInit(): void {
    this.getConsoles();
  }

  getConsoles(): void {
    this.consoleService
      .getConsoles()
      .subscribe((consoles) => (this.consoles = consoles));
  }

  add(name: string): void {
    name = name.trim();
    if (!name) {
      return;
    }
    this.consoleService.addConsole({ name } as Console).subscribe((console) => {
      this.consoles.push(console);
    });
  }

  delete(console: Console): void {
    this.consoles = this.consoles.filter((h) => h !== console);
    this.consoleService.deleteConsole(console.id).subscribe();
  }
}
