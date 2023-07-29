import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HElpComponent } from './h-elp.component';

describe('HElpComponent', () => {
  let component: HElpComponent;
  let fixture: ComponentFixture<HElpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HElpComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HElpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
