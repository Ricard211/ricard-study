import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Console } from '../console';
import { ConsoleService } from '../console.service';

@Component({
  selector: 'app-console-detail',
  templateUrl: './console-detail.component.html',
  styleUrls: ['./console-detail.component.css'],
})
export class ConsoleDetailComponent implements OnInit {
  console: Console | undefined;

  constructor(
    private route: ActivatedRoute,
    private consoleService: ConsoleService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getConsole();
  }

  getConsole(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.consoleService
      .getConsole(id)
      .subscribe((console) => (this.console = console));
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.console) {
      this.consoleService
        .updateConsole(this.console)
        .subscribe(() => this.goBack());
    }
  }
}
