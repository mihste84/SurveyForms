import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NotFoundComponent } from './not-found/not-found.component';
import { ErrorComponent } from './error/error.component';


@NgModule({
  declarations: [NotFoundComponent, ErrorComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports: [NotFoundComponent]
})
export class SharedModule { }
