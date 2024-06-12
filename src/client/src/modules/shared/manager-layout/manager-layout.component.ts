import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { AsideMenuComponent } from '../aside-menu/aside-menu.component';

@Component({
  selector: 'app-manager-layout',
  standalone: true,
  imports: [RouterModule, HeaderComponent, AsideMenuComponent],
  templateUrl: './manager-layout.component.html',
  styleUrl: './manager-layout.component.scss'
})
export class ManagerLayoutComponent {

}
