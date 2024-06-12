import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  public brand: string = 'ViVu Store';
  public isShowProfile: boolean = false;

  public toggleProfile():void{
    this.isShowProfile = !this.isShowProfile;
  }
}
