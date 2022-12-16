import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';

import { ConsoleSearchComponent } from '../console-search/console-search.component';
import { ConsoleService } from '../console.service';
import { CONSOLES } from '../mock-consoles';

import { DashboardComponent } from './dashboard.component';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let consoleService;
  let getConsolesSpy: jasmine.Spy;

  beforeEach(waitForAsync(() => {
    consoleService = jasmine.createSpyObj('ConsoleService', ['getConsoles']);
    getConsolesSpy = consoleService.getConsoles.and.returnValue(of(CONSOLES));
    TestBed.configureTestingModule({
      declarations: [DashboardComponent, ConsoleSearchComponent],
      imports: [RouterTestingModule.withRoutes([])],
      providers: [{ provide: ConsoleService, useValue: ConsoleService }],
    }).compileComponents();

    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should display "Top Consoles" as headline', () => {
    expect(fixture.nativeElement.querySelector('h2').textContent).toEqual(
      'Top Consoles'
    );
  });

  it('should call consoleService', waitForAsync(() => {
    expect(getConsolesSpy.calls.any()).toBe(true);
  }));

  it('should display 4 links', waitForAsync(() => {
    expect(fixture.nativeElement.querySelectorAll('a').length).toEqual(4);
  }));
});
