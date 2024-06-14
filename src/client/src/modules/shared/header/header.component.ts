import { CommonModule, DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

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

  public user: any = {};

  public toggleProfile(): void {
    this.isShowProfile = !this.isShowProfile;
  }

  /**
   *
   */
  constructor(@Inject(DOCUMENT) private document: Document) {
    this._localStorage = document.defaultView?.localStorage;
    
  }

  ngOnInit(): void {
    this.user = JSON.parse(
      this._localStorage?.getItem('userInformation') || '{}'
    );
  }
}
