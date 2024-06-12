import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthentictedLayoutComponent } from './authenticted-layout.component';

describe('AuthentictedLayoutComponent', () => {
  let component: AuthentictedLayoutComponent;
  let fixture: ComponentFixture<AuthentictedLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthentictedLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuthentictedLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
