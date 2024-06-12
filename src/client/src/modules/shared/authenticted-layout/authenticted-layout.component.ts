import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-authenticted-layout',
  standalone: true,
  imports: [HeaderComponent, RouterModule],
  templateUrl: './authenticted-layout.component.html',
  styleUrl: './authenticted-layout.component.scss'
})
export class AuthentictedLayoutComponent {

}
