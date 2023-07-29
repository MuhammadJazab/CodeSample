import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinalStatementComponent } from './final-statement.component';

describe('FinalStatementComponent', () => {
  let component: FinalStatementComponent;
  let fixture: ComponentFixture<FinalStatementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinalStatementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FinalStatementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
