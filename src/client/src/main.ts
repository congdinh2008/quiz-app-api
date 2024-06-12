import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './modules/app.config';
import { AppComponent } from './modules/app.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
