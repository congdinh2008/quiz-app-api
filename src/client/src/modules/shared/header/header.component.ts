import { CommonModule, DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AUTH_SERVICE_INJECTOR } from '../../../constants/injector.constant';
import { IAuthService } from '../../../services/interfaces/auth-service.interface';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit{
  public brand: string = 'ViVu Store';
  public isShowProfile: boolean = false;
  private _localStorage: Storage | undefined;
  public isManager: boolean = false;

  public user: any = {};

  public toggleProfile(): void {
    this.isShowProfile = !this.isShowProfile;
  }

  /**
   *
   */
  constructor(
    @Inject(DOCUMENT) private document: Document,
    @Inject(AUTH_SERVICE_INJECTOR) private authService: IAuthService
  ) {
    this._localStorage = document.defaultView?.localStorage;
  }
  
  ngOnInit(): void {
    this.user = this.authService.getCurrentUser();
    this.isManager = this.user && this.authService.isManager();
  }

  public logout(): void {
    this.authService.logout();
  }


}
