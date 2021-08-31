import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PhotosRoutingModule } from './photos-routing.module';
import { PhotosComponent } from './photos.component';
import { PhotoGridComponent } from './photo-grid/photo-grid.component';
import { PhotoComponent } from './photo/photo.component';


@NgModule({
  declarations: [
    PhotosComponent,
    PhotoGridComponent,
    PhotoComponent
  ],
  imports: [
    CommonModule,
    PhotosRoutingModule
  ]
})
export class PhotosModule { }
