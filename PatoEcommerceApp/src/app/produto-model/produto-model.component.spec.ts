import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutoModelComponent } from './produto-model.component';

describe('ProdutoModelComponent', () => {
  let component: ProdutoModelComponent;
  let fixture: ComponentFixture<ProdutoModelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProdutoModelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProdutoModelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
