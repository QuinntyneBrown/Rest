import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'photos', pathMatch: 'full' },
  { path: 'login', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },
  { path: 'photos', loadChildren: () => import('./photos/photos.module').then(m => m.PhotosModule) },
  { path: 'landing', loadChildren: () => import('./landing/landing.module').then(m => m.LandingModule) },
  { path: 'workspace', loadChildren: () => import('./workspace/workspace.module').then(m => m.WorkspaceModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
