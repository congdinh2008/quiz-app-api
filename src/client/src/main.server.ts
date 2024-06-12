import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './modules/app.component';
import { config } from './modules/app.config.server';

const bootstrap = () => bootstrapApplication(AppComponent, config);

export default bootstrap;
